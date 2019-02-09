module ShutTheBox.Game

open System
open Strategies

let random = Random()
let rollDie () = random.NextDouble() * 6.0 |> int |> (+) 1
let roll2Dice () = rollDie() + rollDie()

let closeNumbers (strategy: Strategy) target box =
    // Find the numbers that can be closed
    let toClose = strategy target box
    // Return the remaining numbers, if we can play a turn; otherwise None.
    toClose |> Option.map(fun nums -> box |> List.except nums)


let doThrow strategy box =
    let target = roll2Dice()
    let result = closeNumbers strategy target box
    // printfn "%i -> %A" target result
    result

// Plays one round of Shut The Box. Returns a boolean indicating whether all the numbers were closed.
let play strategy =
    let rec play' box =
        // Evaluate the result of one throw of the dice
        match box with
        | Some [] -> true // All the numbers were closed
        | None -> false // The target couldn't be matched
        | Some xs -> xs |> doThrow strategy |> play' // Some numbers were closed, so throw again
    play' (Some [1..9]) // Kick off play with a full complement of numbers

// Plays using the specified strategy until a successful outcome is reached.
// Returns the number of rounds played.
let playUntilOut strategy =
    Seq.initInfinite (id >> (+) 1) // An infinite sequence of numbers, starting at 1
    |> Seq.map (fun i -> (i, play strategy)) // For each number, generate a tuple comprising the number and whether the game was successful
    |> Seq.find snd // Find the first tuple where the second element is true
    |> fst // Return the first element of the tuple

// Calculates the average number of rounds required to go out, for the given number of iterations
let test iterations strategy =
    Seq.init iterations id
    |> Seq.map (fun _ -> playUntilOut strategy)
    |> Seq.averageBy float

let test1000 = test 1000