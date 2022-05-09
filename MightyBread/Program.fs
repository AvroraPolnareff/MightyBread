open System
open System.Runtime.InteropServices
open System.Drawing.Imaging
open System.Drawing
open System.Text
open Tesseract


// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"



//let fullScreenshot (filepath: string) (filename: string) (format ImageFormat) =
//    let bounds =

//type EnumWindowsProc = delegate of (IntPtr * int) -> bool






let windowStrings =
    getOpenWindows()
        |> Map.toList
        |> List.map snd

for title in windowStrings do
    printfn $"{title}"

