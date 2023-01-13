using SodukoSolverOmega.SodukoEngine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolverOmega.SodukoEngine.Algorithems
{
    internal static class HelperFuncs
    {
        //checks if a possibility exsists in boxPeers of cell
        public static bool ExsistInBoxPeers(Board board, ValueTuple<int, int> cell, char possibility)
        {
            foreach (ValueTuple<int, int> peer in Board.rowPeers[cell])
            {
                if (board[peer].Possibilities.Contains(possibility) && !board[peer].Isfilled)
                {
                    return true;
                }
            }
            return false;

        }

        //checks if a possibility exsists in colPeers of cell
        public static bool ExsistInColPeers(Board board, ValueTuple<int, int> cell, char possibility)
        {
            foreach (ValueTuple<int, int> peer in Board.colPeers[cell])
            {
                if (board[peer].Possibilities.Contains(possibility) && !board[peer].Isfilled)
                {
                    return true;
                }
            }
            return false;

        }

        //checks if a possibility exsists in rowPeers of cell
        public static bool ExsistInRowPeers(Board board, ValueTuple<int, int> cell, char possibility)
        {
            foreach (ValueTuple<int, int> peer in Board.rowPeers[cell])
            {
                if (board[peer].Possibilities.Contains(possibility) && !board[peer].Isfilled)
                {
                    return true;
                }
            }
            return false;

        }

        //return all peers that thier Possibilities are a sublist of the possibiltes of curent cell
        public static List<ValueTuple<int, int>> SubPossibilities(Board board, List<ValueTuple<int, int>> peers, List<char> Possibilities)
        {
            List <ValueTuple<int, int>> SubLists = new();
            foreach (ValueTuple<int,int> peer in peers)
            {
                //check if Possibilities of peer are a sublist of Possibilities param
                if (!board[peer].Possibilities.Except(Possibilities).Any() && !board[peer].Isfilled)
                {
                    //is sub list
                    SubLists.Add(peer);

                }
            }
            return SubLists;
        }

        //removes a value from all the possibilites of the cells in the group param
        public static void RemovePossibilities(Board board, List<ValueTuple<int,int>> toRemove, char possibility)
        {
            foreach(var peer in toRemove)
            {
                board[peer].Possibilities.Remove(possibility);
            }
        }

        //return a list of all the cells with the possibility param in the list param
        public static List<ValueTuple<int,int>> AllCellsWithPossibility(Board board, List<ValueTuple<int, int>> peers, char val)
        {
            List<ValueTuple<int,int>> cells = new List<ValueTuple<int,int>>();
            foreach (ValueTuple<int, int> peer in peers)
            {
                if (board[peer].Possibilities.Contains(val))
                {
                    cells.Add(peer);
                }
            }
            return cells;
        }

        //return  a list of all unfilled cells in the group param
        public static List<ValueTuple<int, int>> GetUnfilledCells(Board board, List<ValueTuple<int, int>> cells)
        {
            List<ValueTuple<int, int>> Unfilled = new(cells);
            foreach (ValueTuple<int, int> cell in Unfilled)
            {
                if (board[cell].Isfilled)
                {
                    Unfilled.Remove(cell);
                }
            }
            return Unfilled;

        }

        //places a value in a cell with only one possibility,
        //removes its value from its peers possibilities and adds them to effected queue
        public static void FixCellHidden(Board board, ValueTuple<int, int> cell)
        {
            if (!board.cells[cell.Item1, cell.Item2].Isfilled)
            {
                board.cells[cell.Item1, cell.Item2].HiddenSet();
                board.RemoveFromPossibilities(board.cells[cell.Item1, cell.Item2]);
            }
            

        }

        //places a value in a cell,
        //removes its value from its peers possibilities and adds them to effected queue
        public static void FixCell(Board board, ValueTuple<int, int> cell, char val)
        {
            if (!board.cells[cell.Item1, cell.Item2].Isfilled)
            {
                board.cells[cell.Item1, cell.Item2].SetVal(val);
                board.RemoveFromPossibilities(board.cells[cell.Item1, cell.Item2]);
            }
        }

        //return the amount of time a possibility occcurs in the cells from the list param
        public static int PossibilityCountInPeers(Board board, List<ValueTuple<int,int>> peers, char val)
        {
            int count = 0;
            foreach(ValueTuple<int, int> peer in peers)
            {
                if (board[peer].Possibilities.Contains(val))
                {
                    count++;
                }
            }
            return count;
        }

    }
}
