module NativeMethods
    open System
    open System.Runtime.InteropServices
    open System.Text

    [<DllImport("user32.dll")>]
    extern IntPtr GetWindowDC(IntPtr)
    
    [<DllImport("user32.dll")>]
    extern bool EnumWindows(Helpers.Native.EnumWindowsFunc, int)
    
    [<DllImport("user32.dll")>]
    extern int GetWindowText(IntPtr, StringBuilder, int)
    
    [<DllImport("user32.dll")>]
    extern int GetWindowTextLength(IntPtr)
    
    [<DllImport("user32.dll")>]
    extern bool IsWindowVisible(IntPtr)
    
    [<DllImport("user32.dll")>]
    extern IntPtr GetShellWindow()