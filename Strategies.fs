module ShutTheBox.Strategies

// The signature for a strategy function.
// Takes a target number and the list of numbers still in play.
// Returns the numbers to shut, if possible, otherwise None.
type Strategy = int -> int List -> int list option

// The strategy of always choosing the lowest possible number or pair of numbers.
let rec lowestFirst (target: int) (box: int list) =
    match box with
    | [] -> None
    | hd::tl ->
        if hd = target then Some [hd]
        else if hd > target then lowestFirst target tl
        else
            let pair = tl |> List.tryFind (fun n -> hd + n = target)
            match pair with
            | Some n -> Some [hd; n]
            | None -> lowestFirst target tl

// The strategy of always choosing the highest possible number or pair of numbers.
let highestFirst (target: int) (box: int list) =
    box |> List.rev |> lowestFirst target