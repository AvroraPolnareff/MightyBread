namespace MightyBread

open MightyBread.Parsing
open Tesseract

type ChatMessageItem =
    { Rect: Rect
      Name: string
      Description: string option }

type ChatMessage =
    { Items: ChatMessageItem list
      User: string
      Text: string }

[<RequireQualifiedAccess>]
module ChatMessage =

    let parseMessages (rectChars: RectCharString) : RectCharString seq =
        RectCharString.matches @"(?:.\n?)+" rectChars

    let parseUser (message: RectCharString) : string option =
        RectCharString.matches @"^.+(?=\:)" message
        |> Seq.tryItem 0
        |> Option.map RectCharString.toString
        
    let parseItem (rectChars: RectChar list) : ChatMessageItem option =
        let name =
            rectChars
            |> RectCharString.matches @"(?<=\[).+?(?=\])" 
            |> Seq.tryItem 0

        let description =
            rectChars
            |> RectCharString.matches @"(?<=\]).+"
            |> Seq.tryItem 0
            |> Option.map RectCharString.toString

        let rect = name |> Option.bind RectCharString.findCommonRect

        (name, rect)
        ||> Option.map2 ^fun name rect ->
            { Name = name |> RectCharString.toString
              Description = description
              Rect = rect }

    let parseItems (message: RectCharString) : ChatMessageItem list =
        message
        |> RectCharString.matches @"(?:\[)[^\[]+"
        |> Seq.choose parseItem
        |> Seq.toList

    let parseMessage (message: RectCharString) : ChatMessage option =
        let user = parseUser message
        let items = parseItems message
        user
        |> Option.map ^fun user ->
            { User = user; Items = items; Text = RectCharString.toString message }
