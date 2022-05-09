module OCR

open Tesseract

let example = 
    let textImagePath = "./phototest.tif"
    
    let engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default)
    let image = Pix.LoadFromFile(textImagePath)
    let page = engine.Process(image)
    
    printfn "%s" (page.GetBoxText(0))