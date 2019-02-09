module ShutTheBox.Strategies

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

// The strategy of always including the lowest possible number
let rec lowestFirst target box =
    legalCombinations target box
    |> List.sortBy List.min
    |> List.tryHead
    
// The strategy of always including the highest possible number
let highestFirst target box =
    legalCombinations target box
    |> List.sortByDescending List.max
    |> List.tryHead
