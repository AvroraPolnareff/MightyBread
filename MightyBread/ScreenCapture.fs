module ScreenCapture

open System
open System.Text

let getOpenWindows () =
    let shellWindow = NativeMethods.GetShellWindow()
    let mutable windows: Map<IntPtr, string> = Map []
    
    NativeMethods.EnumWindows(Helpers.Native.EnumWindowsFunc(
        fun windowPointer _ -> (
            let isVisible = NativeMethods.IsWindowVisible(windowPointer)
            let isShellWindow = windowPointer = shellWindow

            if not isVisible || isShellWindow then true
            else
                let titleLength = NativeMethods.GetWindowTextLength(windowPointer)

                if titleLength = 0 then true
                else
                    let builder = StringBuilder(titleLength)
                    NativeMethods.GetWindowText(windowPointer, builder, titleLength + 1) |> ignore
                    windows <- Map.add windowPointer (builder.ToString()) windows
                    true
        )
    ), 0) |> ignore
    windows