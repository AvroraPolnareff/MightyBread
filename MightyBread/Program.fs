module MightyBread.Program

open System.IO
open System.Text
open Microsoft.FSharp.Collections
open Microsoft.FSharp.Core
open MightyBread.Parsing
open Tesseract
open Utils



[<EntryPoint>]
let main args =
    let textImagePath = "./screenshot.png"

    let engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default)
    let image = Pix.LoadFromFile(textImagePath)
    let page = engine.Process(image)

    let text = page.GetText()
    let boxText = page.GetBoxText(0)
    let result =
        RectCharString.parseBoxText boxText text
        |> ChatMessage.parseMessages
        |> Seq.map ChatMessage.parseMessage

    let rivenPrefixes =
        ["Crita"; "Laci"; "Pura"]



    let example = $"""
<<<
%A{result}
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