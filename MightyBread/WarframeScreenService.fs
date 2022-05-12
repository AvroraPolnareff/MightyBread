[<RequireQualifiedAccess>]
module WarframeScreenService

open System

let scaleFactor = 1.2

[<Literal>]
let WINDOW_TITLE = "Warframe"

let LAUNCHER_WIDTH = 640
[<Literal>]
let LAUNCHER_HEIGHT = 400
[<Literal>]
let CHAT_START_Y = 530
[<Literal>]
let CHAT_START_X = 0
[<Literal>]
let CHAT_END_Y = 1000
[<Literal>]
let CHAT_END_X = 720
[<Literal>]
let WINDOW_X = 1287
[<Literal>]
let windowY = 750


let checkIsLauncher windowPointer =
    let dimensions = ScreenCapture.getWindowDimensions(windowPointer)
    (dimensions.Height = LAUNCHER_HEIGHT && dimensions.Width = LAUNCHER_WIDTH)
    
let getChatScreenshot () =
    let window = ScreenCapture.tryFindWindowByTitle(WINDOW_TITLE)
//    let rect = ScreenCapture.getWindowDimensions(window)
    ()