module ShutTheBox.Program

open Game
open Strategies

[<EntryPoint>]
let main argv =
    test1000 lowestFirst |> printfn "Lowest first: average %f turns"
    test1000 highestFirst |> printfn "Highest first: average %f turns"
    0 // return an integer exit code
