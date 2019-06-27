#load "Strategies.fs"
#load "Game.fs"

open ShutTheBox

Game.test1000 (Strategies.highestFirst)

Game.test1000 (Strategies.furthestFrom 2)
Game.test1000 (Strategies.avoidAll [2])

Strategies.avoidAll [6;8;7] 7 [1;2;6;7;8;9]
