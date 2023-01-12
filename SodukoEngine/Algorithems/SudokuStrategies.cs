using SodukoSolverOmega.SodukoEngine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolverOmega.SodukoEngine.Algorithems
{
    internal static class SudokuStrategies
    {
        public static bool HiddenSingles(Board board, ValueTuple<int, int> cell)
        {
            //check the cell row
            //check the cell box
            //check the cell col
            //for each group count the number of cells with the possibiity from cell possibilities?
            //if missiing llok for him?
            //worst case 3n^2
            foreach (char possibility in board.cells[cell.Item1, cell.Item2].Possibilities)
            {
                if (!HelperFuncs.ExsistInRowPeers(board, cell, possibility))
                {
                    HelperFuncs.FixCell(board,cell, possibility);
                    return true; ;
                }
                if (!HelperFuncs.ExsistInColPeers(board, cell, possibility))
                {
                    HelperFuncs.FixCell(board,cell, possibility);
                    return true; ;
                }
                if (!HelperFuncs.ExsistInBoxPeers(board, cell, possibility))
                {
                    HelperFuncs.FixCell(board, cell, possibility);
                    return true; ;
                }
            }
            return false;
        }
        public static bool NakedSingles(Board board, ValueTuple<int, int> cords)
        {
            //check for hidden singles and set them vals
            if (board.cells[cords.Item1, cords.Item2].Possibilities.Count == 1 && !board.cells[cords.Item1, cords.Item2].Isfilled)
            {
                HelperFuncs.FixCellHidden(board, cords);
                return true;
            }
            return false;

        }


       //confirms the hidden suspects are all in the same cells, if yes than call HiddenTupples to eliminate possibilites
       //if they are also aligned in another group than func will call intersection removal to remove more possibilities

        public static void ConfirmHidden(Board board, List<char> FoundInGroup, ValueTuple<int, int> cords)
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
                //remove all other possibilities from all of them
                //ie set thier possibilities to - found in row!
                //enter all of these cells to the effectedQueue?
                foreach (ValueTuple<int, int> cell in hiddenCells)
                {
                    board.cells[cell.Item1, cell.Item2].Possibilities = FoundInGroup;
                    //board.EffectedQueue.Remove(cell);
                }
            }
        }

        //function check to see if cell might be a part of a HiddenPair.triple etc, if suspect calls the confoirm hidden
        public static void HiddenTuples(Board board, ValueTuple<int, int> cords, int amount)
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
                ConfirmHidden(board, FoundInRow,cords);
                
            }
            if (colCount == amount)
            {
                ConfirmHidden(board, FoundInCol,cords);

            }
            if (boxCount == amount)
            {
                ConfirmHidden(board, FoundInBox,cords);

            }
        }


        public static bool NakedCells(Board board, ValueTuple<int, int> cords)
        {

            if (!board.Cells[cords.Item1, cords.Item2].Isfilled)
            {
                return NakedCanidates(board, cords, 2);
              
            }
            return false;
            //NakedCanidates(board, cords, 3);
            //NakedCanidates(board, cords, 4);
            //NakedCanidates(board, cords, 5);

        }

        public static bool NakedCanidates(Board board, ValueTuple<int, int> cords, int amount)
        {
            if (board.Cells[cords.Item1,cords.Item2].Possibilities.Count != amount)
            {
                return false;
                //dont bother checking, dont waste time
            }
            //go over all peers, if 'amount' peers are a subList of this cells possibilities
            //this is a naked'amount'
            bool Success = false;
            List<List<ValueTuple<int, int>>> peerGroups = new();
            peerGroups.Add(Board.rowPeers[cords]);
            peerGroups.Add(Board.colPeers[cords]);
            peerGroups.Add(Board.boxPeers[cords]);
            foreach(List<ValueTuple<int, int>> peerGroup in peerGroups)
            {
                List<ValueTuple<int, int>> subPossibilities = HelperFuncs.SubPossibilities(board, peerGroup, board.cells[cords.Item1, cords.Item2].Possibilities);
                if (subPossibilities.Count == amount - 1)
                {
                    List<ValueTuple<int, int>> NonTupled = peerGroup;
                    //get all cells that are not the naked tuple
                    NonTupled = NonTupled.Except(subPossibilities).ToList();
                    foreach (ValueTuple<int, int> valueTuple in NonTupled)
                    {
                        if (board.Cells[valueTuple.Item1, valueTuple.Item2].Possibilities.Intersect(board.cells[cords.Item1, cords.Item2].Possibilities).Count() > 0 &&
                            !board.Cells[valueTuple.Item1, valueTuple.Item2].Isfilled)
                        {
                            Success = true;
                            board.cells[valueTuple.Item1, valueTuple.Item2].Possibilities = board.cells[valueTuple.Item1, valueTuple.Item2].Possibilities.Except(board.cells[cords.Item1, cords.Item2].Possibilities).ToList();
                            if (board.cells[valueTuple.Item1,valueTuple.Item2].Possibilities.Count == 1)
                            {
                                HelperFuncs.FixCellHidden(board, valueTuple);
                            }
                            else
                            {
                                board.EffectedQueue.Enqueue(valueTuple);

                            }
                        }

                    }

                    
                }
            }
            return Success;
            

        }
        public static void InterSectionRemoval()
        {
            return;
        }

    }
}
