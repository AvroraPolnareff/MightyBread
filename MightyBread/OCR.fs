module OCR

open System
open FSharp.Control.Reactive
open Tesseract

let example () = 
    let textImagePath = "./phototest.tif"
    
    let engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default)
    let image = Pix.LoadFromFile(textImagePath)
    let page = engine.Process(image)
    
    let subj = Subject.async

    
    let counter =
        Observable.interval (TimeSpan.FromSeconds(1))
        |> Observable.take 10
    
    use subscription = 
        counter
        |> Observable.map ^fun a -> a * 100L
        |> Observable.subscribe ^fun a -> printfn $"{a}"
    ()
    