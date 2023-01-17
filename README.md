
Omega E

# Sudoku Solver Using Backtracking and Constraint Propagation



# Sudoku Defined:
Sudoku is a logic-based number-placement puzzle. The objective is to fill a 9x9 grid with digits so that each column, each row, and each of the nine 3x3 sub-grids that compose the grid contains all of the digits from 1 to 9.

This Sudoku solver is capable of solving grids of various sizes, not just the traditional 9x9 grid.

### Rules of the puzzle

These rules will represent the constraints in the algorithm.

* Each cell can only contain a number between 1 to BoardSize.
* Each row must contain all of the digits from 1 to BoardSize.
* Each column must contain all of the digits from 1 to BoardSize.
* Each of the sub-grids must contain all of the digits from 1 to BoardSize.

Since this project is capable of solving different sizes of sudoku grids
the number that can be filled in the cells and the size of the subgrids
will be different in every size of a sudoku grid.


###  Backtracking Defined
As the term backtracking suggests, the algorithm checks if the current solution is not suitable, then backtracks and tries other solutions. Thus, recursion is used in this approach. This approach is used to solve problems that have multiple solutions. in our case, we use it for sudoku 

### Constraint Propagation
Constraint propagation is a technique that is used to eliminate possibilities for a given cell based on the current state of the puzzle. 
This technique is very efficient, especially when combined with backtracking, as it helps to reduce the search space and can often lead to a solution with fewer backtracks.

## Optimisations Used
To be able to solve hard/big boards with Backtracking, optimizations for the algorithm are not only needed but are a necessity


- BitWise Possibilities - instead of representing the possible value of each cell as a list. we use a binary number with the set bits indexes being the legal candidates. this makes functions like HasPossibility() run a lot faster o(n) - > o(1). this saves a lot of time when you consider the fact that possibility manipulation is one of the core functionalities of solving. and these funcs are constantly invoked
- Constraints - implemented several "Constraint Strategies" that try to generate clues based on solving logic(implemented: NakedSingles, HiddenSinlges, hiddenPairs, NakedPairs, Naked Triples, Hidden triples)
- EffectedQueue - EffectedQueue is a queue where all affected cells of the previous set are placed(its peers) we then use this affected queue for the NakedSinle constraint, shortening its runtime by not needing to check the entire board each time and only a select few
- ConstraintPrioritisation - each constraint strategy is prioritized by its efficiency, we run the most efficient strategy until it can't generate any more answers. then we move on to the less efficient and so on. once an answer is found we move back to the first strategy. constraints are finished one last strategy is reached and no more answers are found  
- Backtracking Heuristics - instead of iterating over the matrix normally when backtracking, we use a func to get the best next cell to guess on(cell with the least amount of possibilities and the highest amount of empty peers)

## Class Explanations

* IO:
    * I_Input_output - IO interface.
    * FileIO - reading and writing to a file.
    * ConsoleIo - reading and writing to console.
* SodukoEngine:
    * Algorithms:
        * Backtracking Heuristics - Logics to help determine the best next cell to guess on
        * Constraints
            * NakedSingles
            * HiddenSinlges
            * NakedPairs
            * HiddenPairs
            * NakedTriples
            * HiddenTriples
            * IConstrint - interface for constraints
            * HiddenTupple - Parent class for hiddenPairs/triples. contains generic logic to find x HiddenTupple
            * NakedTuple - Parent class for NakedPairs/triples contains generic logic to find x NakedTuple
        
    * Objects:
        * Board - contains a matrix of cells, and several funcs to manipulate them
        * Cell - contains a value, cords, and Possibilities var that represents the possible values to put in the cell
        * Possibilities - holds a binary number that each set bit in it represents a possible possibility, class is filled with funcs to abstract this concept
    * SodukoSolver - Gets a board and Solves it using the data structures and algorithms in the engine
    
* Configuration:
    * Exceptions:
        * BoardSizeMisMatchException - the board is bigger than 25 board or is not an integer sqrt
        * InvalidCharacterException - invalid character in the grid.
        * UnsolvableSudokuException - unsolvable board, either invalid or no possible solution.
    * Consts - File containing all static variables the solver needs to use 

* Program - main class, calls the UserInterface.
* UserInterface - creates a small and compact interface to use the solver.


## Why Not Dancing Links?
the Dancing Links algorithm, also known as DLX is an efficient algorithm that has been proven to be faster than the backtracking approach for sudoku but still, I chose to stick to backtracking for a few reasons

* Most of my class peers have gone with the DLX approach and I didn't want my project to be "just another DLX solver"

* The backtracking approach allows for a lot more customization and personal choice on how to implement it, you choose the data structures for the board, you choose the constraints to implement, and you choose the heuristics to add. On dancing links though it felt like the algorithm is already clearly defined. and all you have to do is follow the step and implement it. and it seemed far less interesting to me

* I simply wanted more of a challenge by trying to implement a well-optimized backtracking algorithm