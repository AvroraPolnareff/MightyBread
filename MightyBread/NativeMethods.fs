namespace NativeMethods
open System
open System.Drawing
open System.Runtime.InteropServices
open System.Text
    
    
module User32 =
    type EnumWindowsFunc = delegate of IntPtr * int -> bool

    [<DllImport("user32.dll")>]
    extern IntPtr GetWindowDC(IntPtr)
    
    [<DllImport("user32.dll")>]
    extern IntPtr ReleaseDC(IntPtr, IntPtr)

    [<DllImport("user32.dll")>]
    extern IntPtr GetWindowRect(IntPtr, Rectangle)

    [<DllImport("user32.dll")>]
    extern bool EnumWindows(EnumWindowsFunc, int)
    
    [<DllImport("user32.dll")>]
    extern int GetWindowText(IntPtr, StringBuilder, int)
    
    [<DllImport("user32.dll")>]
    extern int GetWindowTextLength(IntPtr)
    
    [<DllImport("user32.dll")>]
    extern bool IsWindowVisible(IntPtr)
    
    [<DllImport("user32.dll")>]
    extern IntPtr GetShellWindow()