using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Algorithems;
/*
internal class HiddenTuples : Constraint
{
  
   public override bool Solve(Board board, (int, int) Cellcords)
   {
       return FindTuples(board, Cellcords, 2);
   }
   public static bool FindTuples(Board board, ValueTuple<int, int> cords, int amount)
       {
           int rowCount = 0, colCount = 0, boxCount = 0;
           List<char> FoundInRow = new List<char>();
           List<char> FoundInCol = new List<char>();
           List<char> FoundInBox = new List<char>();
           foreach (char possibility in board.cells[cords.Item1, cords.Item2].Possibilities)
           {
               if (HelperFuncs.PossibilityCountInPeers(board, Board.rowPeers[cords], possibility) == amount - 1)
               {
                   //found!
                   rowCount++;
                   FoundInRow.Add(possibility);
               }
               if (HelperFuncs.PossibilityCountInPeers(board, Board.colPeers[cords], possibility) == amount - 1)
               {
                   //hiddden amount found!
                   colCount++;
                   FoundInCol.Add(possibility);
               }
               if (HelperFuncs.PossibilityCountInPeers(board, Board.boxPeers[cords], possibility) == amount - 1)
               {
                   //hiddden amount found!
                   boxCount++;
                   FoundInBox.Add(possibility);
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

   public static bool ConfirmHidden(Board board, List<char> FoundInGroup, ValueTuple<int, int> cords)
   {
       List<ValueTuple<int, int>> hiddenCells = HelperFuncs.AllCellsWithPossibility(board, Board.rowPeers[cords], FoundInGroup[0]);
       bool isInTheSameCell = true;
       //check if all peers connecting are in the same cell
       foreach (ValueTuple<int, int> cell in hiddenCells)
       {
           for (int i = 1; i < FoundInGroup.Count; i++)
           {
               if (!board.cells[cell.Item1, cell.Item2].Possibilities.Contains(FoundInGroup[i]))
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
               board.cells[cell.Item1, cell.Item2].Possibilities = FoundInGroup;
               //board.EffectedQueue.Remove(cell);
           }
           return true;
       }
       return false;
   }
}
*/