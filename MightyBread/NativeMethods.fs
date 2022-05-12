namespace NativeMethods
open System
open System.Runtime.InteropServices
open System.Text

[<RequireQualifiedAccess>]
module User32 =
    [<Struct; StructLayout(LayoutKind.Sequential)>]
    type Rect =
        val Left: int
        val Top: int
        val Right: int
        val Bottom: int
        member this.Width with get () = this.Right - this.Left
        member this.Height with get () = this.Bottom - this.Top

    type EnumWindowsFunc = delegate of IntPtr * int -> bool

    [<DllImport("user32.dll")>]
    extern IntPtr GetWindowDC(IntPtr windowPointer)
    
    [<DllImport("user32.dll")>]
    extern IntPtr ReleaseDC(IntPtr windowPointer, IntPtr hDC)

    [<DllImport("user32.dll")>]
    extern IntPtr GetWindowRect(IntPtr windowPointer, Rect& rect)

    [<DllImport("user32.dll")>]
    extern bool EnumWindows(EnumWindowsFunc, int lParam)
    
    [<DllImport("user32.dll")>]
    extern int GetWindowText(IntPtr windowPointer, StringBuilder, int lParam)
    
    [<DllImport("user32.dll")>]
    extern int GetWindowTextLength(IntPtr windowPointer)
    
    [<DllImport("user32.dll")>]
    extern bool IsWindowVisible(IntPtr windowPointer)
    
    [<DllImport("user32.dll")>]
    extern IntPtr GetShellWindow()
    
    [<DllImport("user32.dll")>]
    extern IntPtr GetDesktopWindow()
    
    [<DllImport("user32.dll", SetLastError = true)>]
    extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow)
    
    [<DllImport("user32.dll", CharSet = CharSet.Auto)>]
    extern IntPtr SendMessage(IntPtr windowPointer, UInt32 Msg, IntPtr wParam, IntPtr lParam);

[<RequireQualifiedAccess>]
module GDI32 =
    let [<Literal>] SRCCOPY = 0x00CC0020
    
    [<DllImport("gdi32.dll")>]
    extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hObjectSource, int nXSrc, int nYSrc, int dwRop)
    
    [<DllImport("gdi32.dll")>]
    extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth, int nHeight)
    
    [<DllImport("gdi32.dll")>]
    extern IntPtr CreateCompatibleDC(IntPtr hDC)
    
    [<DllImport("gdi32.dll")>]
    extern IntPtr DeleteDC(IntPtr hDC)
    
    [<DllImport("gdi32.dll")>]
    extern IntPtr DeleteObject(IntPtr hObject)
    
    [<DllImport("gdi32.dll")>]
    extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject)
