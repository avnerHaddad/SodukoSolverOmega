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
        public static bool ExsistInBoxPeers(Board board, ValueTuple<int, int> cell, char possibility)
        {
            foreach (ValueTuple<int, int> peer in Board.rowPeers[cell])
            {
                if (board.cells[peer.Item1, peer.Item2].Possibilities.Contains(possibility) && !board.cells[peer.Item1, peer.Item2].Isfilled)
                {
                    return true;
                }
            }
            return false;

        }
        public static bool ExsistInColPeers(Board board, ValueTuple<int, int> cell, char possibility)
        {
            foreach (ValueTuple<int, int> peer in Board.colPeers[cell])
            {
                if (board.cells[peer.Item1, peer.Item2].Possibilities.Contains(possibility) && !board.cells[peer.Item1, peer.Item2].Isfilled)
                {
                    return true;
                }
            }
            return false;

        }
        public static bool ExsistInRowPeers(Board board, ValueTuple<int, int> cell, char possibility)
        {
            foreach (ValueTuple<int, int> peer in Board.rowPeers[cell])
            {
                if (board.cells[peer.Item1, peer.Item2].Possibilities.Contains(possibility) && !board.cells[peer.Item1, peer.Item2].Isfilled)
                {
                    return true;
                }
            }
            return false;

        }
        public static ValueTuple<int,int> FirstCellWithPossibility(Board board, List<ValueTuple<int,int>> peers,char val)
        {
            foreach(ValueTuple<int,int> peer in peers)
            {
                if (board.cells[peer.Item1, peer.Item2].Possibilities.Contains(val)){
                    return peer;
                }
            }
            return new ValueTuple<int,int> (-1,-1);
        }

        //return all peers that thier possibilities are a sublist of the possibiltes of curent cell
        public static List<ValueTuple<int, int>> SubPossibilities(Board board, List<ValueTuple<int, int>> peers, List<char> possibilities)
        {
            List <ValueTuple<int, int>> SubLists = new();
            foreach (ValueTuple<int,int> peer in peers)
            {
                //check if possibilities of peer are a sublist of possibilities param
                if (!board.Cells[peer.Item1, peer.Item2].Possibilities.Except(possibilities).Any() && !board.Cells[peer.Item1,peer.Item2].Isfilled)
                {
                    //is sub list
                    SubLists.Add(peer);

                }
            }
            return SubLists;
        }

        public static List<ValueTuple<int,int>> AllCellsWithPossibility(Board board, List<ValueTuple<int, int>> peers, char val)
        {
            List<ValueTuple<int,int>> cells = new List<ValueTuple<int,int>>();
            foreach (ValueTuple<int, int> peer in peers)
            {
                if (board.cells[peer.Item1, peer.Item2].Possibilities.Contains(val))
                {
                    cells.Add(peer);
                }
            }
            return cells;
        }
        public static List<ValueTuple<int, int>> GetUnfilledCells(Board board, List<ValueTuple<int, int>> cells)
        {
            List<ValueTuple<int, int>> Unfilled = new(cells);
            foreach (ValueTuple<int, int> cell in Unfilled)
            {
                if (board.Cells[cell.Item1, cell.Item2].Isfilled)
                {
                    Unfilled.Remove(cell);
                }
            }
            return Unfilled;

        }

        public static void FixCellHidden(Board board, ValueTuple<int, int> cell)
        {
            if (!board.cells[cell.Item1, cell.Item2].Isfilled)
            {
                board.cells[cell.Item1, cell.Item2].HiddenSet();
                board.RemoveFromPossibilities(board.cells[cell.Item1, cell.Item2]);
            }
            

        }

        public static void FixCell(Board board, ValueTuple<int, int> cell, char val)
        {
            if (!board.cells[cell.Item1, cell.Item2].Isfilled)
            {
                board.cells[cell.Item1, cell.Item2].SetVal(val);
                board.RemoveFromPossibilities(board.cells[cell.Item1, cell.Item2]);
            }
        }
        public static int PossibilityCountInPeers(Board board, List<ValueTuple<int,int>> peers, char val)
        {
            int count = 0;
            foreach(ValueTuple<int, int> peer in peers)
            {
                if (board.cells[peer.Item1, peer.Item2].Possibilities.Contains(val))
                {
                    count++;
                }
            }
            return count;
        }

    }
}
