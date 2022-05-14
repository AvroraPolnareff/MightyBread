namespace MightyBread.Parsing

open System
open System.Text
open System.Text.RegularExpressions
open Tesseract

type RectChar =
    { Char: char
      Rect: Rect option }

type RectCharString = RectChar list

module RectCharString =

    let private parseBoxChars (boxText: string) : (char * Rect) list =
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

    let private parseZip (text: string) (boxChars: (char * Rect) list) : RectCharString =
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

    let parseBoxText (boxText: string) (text: string) : RectCharString =
        boxText
        |> parseBoxChars
        |> parseZip text

    let toString (str: RectCharString) : string =
        str
        |> List.map ^fun rectChar -> rectChar.Char
        |> fun chars -> String.Join("", chars)

    let matches (pattern: string) (input: RectCharString) : RectCharString seq =
        let matches = Regex.Matches(toString input, pattern)
        matches
        |> Seq.map ^fun m ->
            input
            |> List.skip m.Index
            |> List.take m.Length

    let formatDebug (str: RectCharString) : string =
        let sb = StringBuilder()
        for rectChar in str do
            match rectChar.Rect with
            | None -> sb.AppendLine(string rectChar.Char) |> ignore
            | Some rect -> sb.Append(rectChar.Char).Append(" ").AppendLine(string rect) |> ignore
        sb.ToString()

    let findCommonRect (rectChars: RectChar list) =
        let filteredChars =
            rectChars
            |> List.filter ^fun rectChar ->
                Option.isSome rectChar.Rect

        let firstChar = filteredChars.[0]
        let lastChar = List.last filteredChars

        (firstChar.Rect, lastChar.Rect)
        ||> Option.map2 ^fun firstRect lastRect ->
            Rect.FromCoords(firstRect.X1, firstRect.Y1, lastRect.X2, lastRect.Y2)