#load "Strategies.fs"
#load "Game.fs"

Game.test1000 Strategies.highestFirst |> printfn "Highest first: %f turns"
Game.test1000 Strategies.lowestFirst |> printfn "Lowest first: %f turns"