# MastermindGame
 A number guessing game, with guranteed success without randomness.

## Code Breakers
 - Random: randomly guesses the secret number each turn (complexity is 10^n, where n is the secret code digit count.)
 - Donald Knuth Algorithm: by process of elimination, on each turn, gradually eliminates unsuccessfull code) maximum 6 guesses to win.
 - Custom Code Breakers: Inheret from [Player Module Base]

## Bonus
 - Includes a standalone GUI panel for visualization of the algorithm in process, simply enable/disable the gameobject named **AlgorithmWalkthrough**

 [Player Module Base]: <https://github.com/DDR12/MastermindGame/blob/653d3269566ab97a0ad2d7c8c3ef855b5da72790/Assets/Scripts/CodeBreakers/BasePlayerModule.cs>
