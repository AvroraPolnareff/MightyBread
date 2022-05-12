open System
open System.IO
open System.Text
open System.Text.RegularExpressions
open Microsoft.FSharp.Collections
open Tesseract
open Utils

//let windowStrings =
//    ScreenCapture.getOpenWindows()
//        |> List.map snd
//        
//ScreenCapture.captureWindowToFile (User32.GetDesktopWindow()) "./file.png" ImageFormat.Png 
//
//for title in windowStrings do
//    printfn $"{title}"
//

type RectChar =
    { Char: char
      Rect: Rect option }

[<RequireQualifiedAccess>]
module RectChars =
    let parseZip (text: string) (boxChars: (char * Rect) list) =
        let rectChars = []
        ((rectChars, boxChars), text)
        ||> Seq.fold ^fun (rectChars, boxChars) c ->
            let rect, chars =
                match boxChars with
                | (c', rect) :: charTail when c' = c ->
                    Some rect, charTail
                | _ -> None, boxChars
            { Char = c; Rect = rect } :: rectChars, chars
        |> fst
        |> List.rev
    
    let parseBoxText (boxText: string) : (char * Rect) list =
        boxText.Split('\010', StringSplitOptions.RemoveEmptyEntries)
        |> Seq.map ^fun line ->
            let tokens = line.Split(' ')
            let c = tokens.[0] |> char
            let rect =
                Rect.FromCoords(
                    int tokens.[1],
                    int tokens.[2],
                    int tokens.[3],
                    int tokens.[4]
                )
            c, rect
        |> Seq.toList

    let matches (pattern: string) (input: RectChar list) : RectChar list seq =
        let allText =
            input
            |> List.map ^fun rectChar -> rectChar.Char
            |> fun chars -> String.Join("", chars)
        
        let matches = Regex.Matches(allText, pattern)
        matches
        |> Seq.map ^fun m ->
            input
            |> List.skip m.Index
            |> List.take m.Length
        

type ChatMessage =
    { Chars: RectChar list }


[<RequireQualifiedAccess>]
module ChatMessage =
    let parseMessages (rectChars: RectChar list) : ChatMessage seq =
        let matches = RectChars.matches @"(?:.\n?)+" rectChars
        matches
        |> Seq.map ^fun m -> { Chars = m }
    
    let format (chatMessage: ChatMessage) : string =
        let sb = StringBuilder()
        for rectChar in chatMessage.Chars do
            match rectChar.Rect with
            | None -> sb.AppendLine(string rectChar.Char) |> ignore
            | Some rect -> sb.Append(rectChar.Char).Append(" ").AppendLine(string rect) |> ignore
        sb.ToString()



[<EntryPoint>]
let main args =
    let textImagePath = "./screenshot.png"

    let engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default)
    let image = Pix.LoadFromFile(textImagePath)
    let page = engine.Process(image)

    let text = page.GetText()
    let boxText = page.GetBoxText(0)
    let result =
        boxText
        |> RectChars.parseBoxText
        |> RectChars.parseZip text
        |> ChatMessage.parseMessages
        |> Seq.map ChatMessage.format
        |> fun x -> String.Join("", x)
    

    let rivenPrefixes =
        ["Crita"; "Laci"; "Pura"]



    let example = $"""
<<<
{result}
>>>

<<<
{page.GetText()}
>>>

<<<
{page.GetBoxText(0)}
>>>
"""

    File.WriteAllText("./text.txt", example)

    0


    //let subj = Subject.async
    //
    //
    //let counter =
    //    Observable.interval (TimeSpan.FromSeconds(1))
    //    |> Observable.take 10

    //use subscription = 
    //    counter
    //    |> Observable.map ^fun a -> a * 100L
    //    |> Observable.subscribe ^fun a -> printfn $"{a}"
    //    ()