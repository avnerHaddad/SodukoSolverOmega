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
       


        //return all peers that thier Possibilities are a sublist of the possibiltes of curent cell
        public static List<ValueTuple<int, int>> SubPossibilities(Board board, List<ValueTuple<int, int>> peers, uint Possibilities)
        {
            List <ValueTuple<int, int>> SubLists = new();
            foreach (ValueTuple<int,int> peer in peers)
            {
                //check if Possibilities of peer are a sublist of Possibilities param
                //subList bitwise
                if (!((board[peer].Possibilities | Possibilities) > board[peer].Possibilities)  && !board[peer].Isfilled)
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
                BitUtils.RemovePossibility(board[peer].Possibilities, possibility);
            }
        }

        //return a list of all the cells with the possibility param in the list param
        public static List<ValueTuple<int,int>> AllCellsWithPossibility(Board board, List<ValueTuple<int, int>> peers, char val)
        {
            List<ValueTuple<int,int>> cells = new List<ValueTuple<int,int>>();
            foreach (ValueTuple<int, int> peer in peers)
            {
                
                if (BitUtils.BitContains(board[peer].Possibilities,val))
                {
                    cells.Add(peer);
                }
            }
            return cells;
        }
        public static List<ValueTuple<int,int>> AllCellsWithPossibility(Board board, List<ValueTuple<int, int>> peers, uint val)
        {
            List<ValueTuple<int,int>> cells = new List<ValueTuple<int,int>>();
            foreach (ValueTuple<int, int> peer in peers)
            {
                
                if (BitUtils.BitContains(board[peer].Possibilities,val))
                {
                    cells.Add(peer);
                }
            }
            return cells;
        }

        //return  a list of all unfilled cells in the group param
        public static List<ValueTuple<int, int>> GetUnfilledCells(Board board, List<ValueTuple<int, int>> cells)
        {
            List<ValueTuple<int, int>> Unfilled = new();
            foreach (ValueTuple<int, int> cell in cells)
            {
                if (!board[cell].Isfilled)
                {
                    Unfilled.Add(cell);
                }
            }
            return Unfilled;

        }

        //places a value in a cell with only one possibility,
        //removes its value from its peers possibilities and adds them to effected queue
        public static void FixCellHidden(Board board, ValueTuple<int, int> cell)
        {
            if (board.cells[cell.Item1, cell.Item2].Isfilled) return;
            board.cells[cell.Item1, cell.Item2].HiddenSet();
            board.RemoveFromPossibilities(board.cells[cell.Item1, cell.Item2]);


        }

        //places a value in a cell,
        //removes its value from its peers possibilities and adds them to effected queue
        public static void FixCell(Board board, ValueTuple<int, int> cell, uint val)
        {
            if (board.cells[cell.Item1, cell.Item2].Isfilled) return;
            board.cells[cell.Item1, cell.Item2].SetVal(val);
            board.RemoveFromPossibilities(board[cell]);
        }

        //return the amount of time a possibility occcurs in the cells from the list param
        public static int PossibilityCountInPeers(Board board, List<ValueTuple<int,int>> peers, char val)
        {
            int count = 0;
            foreach(ValueTuple<int, int> peer in peers)
            {
                if ((board[peer].Possibilities & val) > 0)
                {
                    count++;
                }
            }
            return count;
        }

    }
}
