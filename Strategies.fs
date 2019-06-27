module ShutTheBox.Strategies

open System

// The signature for a strategy function.
// Takes a target number and the list of numbers still in play.
// Returns the numbers to shut, if possible, otherwise None.
type Strategy = int -> int List -> int list option

// Gets a list of all possible combinations of numbers in the box that sum to the target.
let rec combinations (target: int) (box: int list) =
    let findForTarget = combinations target
    match box with
    | [] -> []
    | current::rest ->
        if current = target then [current]::(findForTarget rest)
        else if current > target then findForTarget rest
        else
            let combinationsWithCurrent =
                combinations (target - current) rest
                |> List.map (fun c -> current::c)
            combinationsWithCurrent |> List.append (findForTarget rest)

let legalCombinations target box =
    combinations target box
    |> List.filter (fun c -> c.Length < 4)

let applyStrategy fn target box =
    legalCombinations target box
    |> fn
    |> List.tryHead

// The strategy of always including the lowest possible number
let lowestFirst = applyStrategy (List.sortBy List.min)
    
// The strategy of always including the highest possible number
let highestFirst = applyStrategy (List.sortByDescending List.max)

let containsAny items list = list |> List.exists (fun x -> List.contains x items)

// TODO check priority
let avoidAll ns = applyStrategy (List.sortBy (containsAny ns))

let distanceFrom (target: int) x = (target - x) |> Math.Abs

let furthestFrom n = applyStrategy (List.sortByDescending (List.sumBy (distanceFrom n)))