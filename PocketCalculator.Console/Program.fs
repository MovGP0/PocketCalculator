open PocketCalculator.Parser
open System
open System.Text

let rec getInput () = 
    Console.Write("input formula: ")
    let input = Console.ReadLine()
    if(String.IsNullOrEmpty input)
    then
        ()
    else
        calculator input 
        |> printfn "%f" 
        getInput ()

[<EntryPoint>]
let main (args:string[]) = 
    getInput()
    0
