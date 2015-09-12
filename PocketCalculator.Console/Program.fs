open PocketCalculator.Parser
open System.Text

[<EntryPoint>]
let main (args:string[]) = 
    let combine (sb:StringBuilder) (s:string) =
        if sb.Length = 0 
        then sb.Append s
        else (sb.Append " ").Append s

    args
    |> Array.fold combine (new StringBuilder())
    |> string
    |> calculator 
    |> printfn "%f" 
    0
