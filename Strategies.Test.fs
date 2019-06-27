module ShutTheBox.StrategiesTests
open Xunit
open FsUnit
open ShutTheBox.Strategies

let lowestFirstTestData: obj[][] = [|
    [|[1..9]; 2; Some [2]|]
    [|[1..9]; 3; Some [1; 2]|]
    [|[1..9]; 4; Some [1; 3]|]
    [|[1..9]; 9; Some [1; 8]|]
    [|[1..9]; 10; Some [1; 9]|]
    [|[1..9]; 12; Some [1; 5; 6]|]
    [|[7; 8; 9]; 6; None|]
    [|[9]; 9; Some [9]|]
    [|[1; 2; 3]; 6; Some [1; 2; 3]|]
|]


[<Theory>]
[<MemberData("lowestFirstTestData")>]
let lowestFirstTest (box, target, expected) =
    let result = lowestFirst target box
    result |> should equal expected

let highestFirstTestData: obj[][] = [|
    [|[1..9]; 2; Some [2]|]
    [|[1..9]; 3; Some [3]|]
    [|[1..9]; 9; Some [9]|]
    [|[1..9]; 10; Some [1; 9]|]
    [|[1..9]; 12; Some [3; 9]|]
    [|[7; 8; 9]; 6; None|]
    [|[9]; 9; Some [9]|]
|]


[<Theory>]
[<MemberData("highestFirstTestData")>]
let highestFirstTest (box, target, expected) =
    let result = highestFirst target box
    result |> should equal expected

let avoidAllTestData: obj[][] = [|
    [|[1..9]; 7; Some [1, 6]|]
|]

[<Theory>]
[<MemberData("avoidAllTestData")>]
let avoidAllTest (box, target, expected) =
    let result = avoidAll target box
    result |> should equal expected