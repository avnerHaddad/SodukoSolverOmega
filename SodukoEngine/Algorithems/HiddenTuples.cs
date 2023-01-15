using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Algorithems;

internal class HiddenTuples : IConstraint
{
    
     public bool Solve(Board board)
     {
         return FindHiddenTuples(board, 2);
     }

     bool FindHiddenTuples(Board board, int tupleSize)
     {
         bool FoundTuple = false;
         // Iterate through each group (row, column, or box) on the board
         foreach (List<ValueTuple<int, int>> group in Board.Groups)
         {
             // Create a list to hold the candidate sets for each cell in the group
             List<uint> candidateSets = new List<uint>();
             foreach (ValueTuple<int, int> cell in group)
             {
                 candidateSets.Add(board[cell].Possibilities);
             }

             // Iterate through all possible combinations of tupleSize cells in the group
             for (int i = 0; i < group.Count - tupleSize + 1; i++)
             {
                 for (int j = i + 1; j < group.Count; j++)
                 {
                     // Get the intersection of the candidate sets for the current combination of cells
                     uint intersection = candidateSets[i] & candidateSets[j];
                     for (int k = 2; k < tupleSize; k++)
                     {
                         intersection &= candidateSets[j + k];
                     }

                     // If the intersection has the same number of set bits as the tuple size, it's a hidden tuple
                     if (BitUtils.CountOfSetBits(intersection) == tupleSize)
                     {
                         // Iterate through the cells in the group again to find the cells that are part of the hidden tuple
                         for (int k = 0; k < group.Count; k++)
                         {
                             // If the cell is part of the hidden tuple, remove the candidates from its possibilities
                             if ((candidateSets[k] & intersection) > 0)
                             {
                                 board[group[k]].Possibilities &= ~intersection;
                                 FoundTuple = true;

                             }
                         }
                     }
                 }
             }
         }

         return FoundTuple;
     }
     /*
     public static bool FindTuples(Board board, ValueTuple<int, int> cords, int amount)
         {
             int rowCount = 0, colCount = 0, boxCount = 0;
             uint FoundInRow = 0;
             uint FoundInCol = 0;
             uint FoundInBox = 0;
             foreach (char possibility in BitUtils.ListPossibilities(board.cells[cords.Item1, cords.Item2].Possibilities))
             {
                 if (HelperFuncs.PossibilityCountInPeers(board, Board.rowPeers[cords], possibility) == amount - 1)
                 {
                     //found!
                     rowCount++;
                     BitUtils.addPossibility(FoundInRow, possibility);
                 }
                 if (HelperFuncs.PossibilityCountInPeers(board, Board.colPeers[cords], possibility) == amount - 1)
                 {
                     //hiddden amount found!
                     colCount++;
                     BitUtils.addPossibility(FoundInCol, possibility);
                 }
                 if (HelperFuncs.PossibilityCountInPeers(board, Board.boxPeers[cords], possibility) == amount - 1)
                 {
                     //hiddden amount found!
                     boxCount++;
                     BitUtils.addPossibility(FoundInBox, possibility);
                 }
             }
             if (rowCount == amount)
             {
                 return ConfirmHidden(board, FoundInRow,cords);
                 
             }
             if (colCount == amount)
             {
                 return ConfirmHidden(board, FoundInCol,cords);
  
             }
             if (boxCount == amount)
             {
                 return ConfirmHidden(board, FoundInBox,cords);
  
             }
             return false;
         }
  
     public static bool ConfirmHidden(Board board, uint FoundInGroup, ValueTuple<int, int> cords)
     {
         List<ValueTuple<int, int>> hiddenCells = HelperFuncs.AllCellsWithPossibility(board, Board.rowPeers[cords], (FoundInGroup));
         bool isInTheSameCell = true;
         //check if all peers connecting are in the same cell
         foreach (ValueTuple<int, int> cell in hiddenCells)
         {
             foreach (var possibility in BitUtils.ListPossibilities(FoundInGroup))
             {
                 if (!BitUtils.BitContains(board.cells[cell.Item1, cell.Item2].Possibilities, possibility))
                 {
                     isInTheSameCell = false; break;
                 }
             }
  
         }
         if (isInTheSameCell)
         {
             //do the stuff
             //remove all other Possibilities from all of them
             //ie set thier Possibilities to - found in row!
             //enter all of these cells to the effectedQueue?
             foreach (ValueTuple<int, int> cell in hiddenCells)
             {
                 board[cell].Possibilities = FoundInGroup;
                 //board.EffectedQueue.Remove(cell);
             }
             return true;
         }
         return false;
     }
  }
  */
    
    }
