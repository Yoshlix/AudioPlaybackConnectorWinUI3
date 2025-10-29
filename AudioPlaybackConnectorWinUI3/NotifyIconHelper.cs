using System;
using System.Runtime.InteropServices;
using Windows.Foundation;

namespace AudioPlaybackConnectorWinUI3;

public class NotifyIconHelper : IDisposable
{
    private const int WM_USER = 0x0400;
    private const int WM_NOTIFYICON = WM_USER + 1;
    private const int NIN_SELECT = WM_USER + 0;
    private const int NIN_KEYSELECT = WM_USER + 1;
    private const int WM_CONTEXTMENU = 0x007B;
    private const int WM_RBUTTONUP = 0x0205;

    private readonly IntPtr _hwnd;
    private bool _isAdded = false;
    private NOTIFYICONDATA _nid;

    public event EventHandler? IconClicked;
    public event EventHandler<Point>? IconRightClicked;

    public NotifyIconHelper(IntPtr hwnd)
    {
        _hwnd = hwnd;
    }

    public void Initialize()
    {
        _nid = new NOTIFYICONDATA
        {
            cbSize = Marshal.SizeOf<NOTIFYICONDATA>(),
            hWnd = _hwnd,
            uID = 1,
            uFlags = NIF_MESSAGE | NIF_ICON | NIF_TIP | NIF_SHOWTIP,
            uCallbackMessage = WM_NOTIFYICON,
            szTip = "AudioPlaybackConnector",
            uVersion = NOTIFYICON_VERSION_4
        };

        // Load icon from resources
        _nid.hIcon = LoadAppIcon();

        // Add the icon
        Shell_NotifyIcon(NIM_ADD, ref _nid);
        Shell_NotifyIcon(NIM_SETVERSION, ref _nid);
        _isAdded = true;

        // Hook into window messages
        HookWindowMessages();
    }

    private IntPtr LoadAppIcon()
    {
        // Try to load the application icon
        var hInstance = Marshal.GetHINSTANCE(typeof(NotifyIconHelper).Module);
        var hIcon = LoadIcon(hInstance, new IntPtr(1));
        
        if (hIcon == IntPtr.Zero)
        {
            // Fall back to default application icon
            hIcon = LoadIcon(IntPtr.Zero, new IntPtr(32512)); // IDI_APPLICATION
        }

        return hIcon;
    }

    private void HookWindowMessages()
    {
        // Note: In a real WinUI3 app, you would use a proper subclass or window procedure hook
        // For this implementation, we'll rely on the notification icon callback
    }

    public Rect GetIconRect()
    {
        var niid = new NOTIFYICONIDENTIFIER
        {
            cbSize = Marshal.SizeOf<NOTIFYICONIDENTIFIER>(),
            hWnd = _hwnd,
            uID = 1
        };

        RECT rect = new RECT();
        var hr = Shell_NotifyIconGetRect(ref niid, out rect);
        
        if (hr == 0) // S_OK
        {
            return new Rect(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
        }

        return new Rect(0, 0, 16, 16);
    }

    public void ProcessMessage(uint msg, IntPtr wParam, IntPtr lParam)
    {
        if (msg == WM_NOTIFYICON)
        {
            var loWord = (int)(lParam.ToInt64() & 0xFFFF);
            
            if (loWord == NIN_SELECT || loWord == NIN_KEYSELECT)
            {
                IconClicked?.Invoke(this, EventArgs.Empty);
            }
            else if (loWord == WM_CONTEXTMENU)
            {
                var x = (int)(wParam.ToInt64() & 0xFFFF);
                var y = (int)((wParam.ToInt64() >> 16) & 0xFFFF);
                IconRightClicked?.Invoke(this, new Point(x, y));
            }
        }
    }

    public void Dispose()
    {
        if (_isAdded)
        {
            Shell_NotifyIcon(NIM_DELETE, ref _nid);
            _isAdded = false;
        }

        if (_nid.hIcon != IntPtr.Zero)
        {
            DestroyIcon(_nid.hIcon);
            _nid.hIcon = IntPtr.Zero;
        }
    }

    // P/Invoke declarations
    private const int NIF_MESSAGE = 0x00000001;
    private const int NIF_ICON = 0x00000002;
    private const int NIF_TIP = 0x00000004;
    private const int NIF_SHOWTIP = 0x00000080;
    private const int NIM_ADD = 0x00000000;
    private const int NIM_MODIFY = 0x00000001;
    private const int NIM_DELETE = 0x00000002;
    private const int NIM_SETVERSION = 0x00000004;
    private const int NOTIFYICON_VERSION_4 = 4;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct NOTIFYICONDATA
    {
        public int cbSize;
        public IntPtr hWnd;
        public int uID;
        public int uFlags;
        public int uCallbackMessage;
        public IntPtr hIcon;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szTip;
        public int dwState;
        public int dwStateMask;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string szInfo;
        public int uVersion;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string szInfoTitle;
        public int dwInfoFlags;
        public Guid guidItem;
        public IntPtr hBalloonIcon;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct NOTIFYICONIDENTIFIER
    {
        public int cbSize;
        public IntPtr hWnd;
        public int uID;
        public Guid guidItem;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
    private static extern bool Shell_NotifyIcon(int dwMessage, ref NOTIFYICONDATA lpData);

    [DllImport("shell32.dll", SetLastError = true)]
    private static extern int Shell_NotifyIconGetRect(ref NOTIFYICONIDENTIFIER identifier, out RECT iconLocation);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr LoadIcon(IntPtr hInstance, IntPtr lpIconName);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool DestroyIcon(IntPtr hIcon);
}
