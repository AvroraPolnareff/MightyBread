open System.Drawing.Imaging
open NativeMethods
open ScreenCapture

let windowStrings =
    getOpenWindows()
        |> Map.toList
        |> List.map snd
        
captureWindowToFile (User32.GetDesktopWindow()) "./file.png" ImageFormat.Png 

for title in windowStrings do
    printfn $"{title}"

