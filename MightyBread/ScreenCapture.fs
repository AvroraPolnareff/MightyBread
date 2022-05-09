module ScreenCapture

open System
open System.Text
open NativeMethods

let getOpenWindows () =
    let shellWindow = User32.GetShellWindow()
    let mutable windows: Map<IntPtr, string> = Map []
    let myCapybara =
        fun windowPointer _ -> 
            let isVisible = User32.IsWindowVisible(windowPointer)
            let isShellWindow = windowPointer = shellWindow

            if not isVisible || isShellWindow then true
            else
                let titleLength = User32.GetWindowTextLength(windowPointer)

                if titleLength = 0 then true
                else
                    // TODO проверить на проблемы с ссылками при передаче стрингбилдера в нейтив
                    let builder = StringBuilder(titleLength)
                    User32.GetWindowText(windowPointer, builder, titleLength + 1) |> ignore
                    windows <- Map.add windowPointer (builder.ToString()) windows
                    true
    
    User32.EnumWindows(User32.EnumWindowsFunc(myCapybara), 0) |> ignore
    windows