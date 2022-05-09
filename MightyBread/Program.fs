open ScreenCapture

let windowStrings =
    getOpenWindows()
        |> Map.toList
        |> List.map snd

for title in windowStrings do
    printfn $"{title}"
