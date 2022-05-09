module ScreenCapture

open System
open System.Drawing
open System.Drawing.Imaging
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
    
let captureWindow (windowPointer: IntPtr) =
    // get te hDC of the target window
    let hdcSrc = User32.GetWindowDC(windowPointer)
    // get the size
    let windowRect = Rectangle()
    User32.GetWindowRect(windowPointer, windowRect) |> ignore
    // create a device context we can copy to
    let hdcDest = GDI32.CreateCompatibleDC(hdcSrc)
    // create a bitmap we can copy it to,
    // using GetDeviceCaps to get the width/height
    let hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, windowRect.Width, windowRect.Height)
    // select the bitmap object
    let hOld = GDI32.SelectObject(hdcDest, hBitmap)
    // bitblt over
    GDI32.BitBlt(hdcDest, 0, 0, windowRect.Width, windowRect.Height, hdcSrc, 0, 0, GDI32.SRCCOPY) |> ignore
    // restore selection
    GDI32.SelectObject(hdcDest, hOld) |> ignore
    // clean up 
    GDI32.DeleteDC(hdcDest) |> ignore
    User32.ReleaseDC(windowPointer, hdcSrc) |> ignore
    let img = Image.FromHbitmap(hBitmap)
    GDI32.DeleteObject(hBitmap) |> ignore
    img
    
let captureScreen () =
    captureWindow(User32.GetDesktopWindow())
    
let captureWindowToFile (windowPointer: IntPtr) (filename: string) (format: ImageFormat) =
    let img = captureWindow(windowPointer)
    img.Save(filename, format)