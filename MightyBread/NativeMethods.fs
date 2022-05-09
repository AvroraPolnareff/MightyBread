namespace NativeMethods
open System
open System.Drawing
open System.Runtime.InteropServices
open System.Text
    
    
module User32 =
    type EnumWindowsFunc = delegate of IntPtr * int -> bool

    [<DllImport("user32.dll")>]
    extern IntPtr GetWindowDC(IntPtr windowPointer)
    
    [<DllImport("user32.dll")>]
    extern IntPtr ReleaseDC(IntPtr windowPointer, IntPtr hDC)

    [<DllImport("user32.dll")>]
    extern IntPtr GetWindowRect(IntPtr windowPointer, Rectangle rect)

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
    
module GDI32 =
    [<Literal>]
    let SRCCOPY = 0x00CC0020
    
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
