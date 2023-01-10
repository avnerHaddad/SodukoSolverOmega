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
                if (board.cells[peer.Item1, peer.Item2].Possibilities.Contains(possibility) && !board.cells[peer.Item1, peer.Item2].isfilled)
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
                if (board.cells[peer.Item1, peer.Item2].Possibilities.Contains(possibility) && !board.cells[peer.Item1, peer.Item2].isfilled)
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
                if (board.cells[peer.Item1, peer.Item2].Possibilities.Contains(possibility) && !board.cells[peer.Item1, peer.Item2].isfilled)
                {
                    return true;
                }
            }
            return false;

        }
        public static void fixCellHidden(Board board, ValueTuple<int, int> cell)
        {
            board.cells[cell.Item1, cell.Item2].hiddenSet();
        }

        public static void FixCell(Board board, ValueTuple<int, int> cell, char val)
        {
            board.cells[cell.Item1, cell.Item2].setVal(val);
            board.RemoveFromPossibilities(board.cells[cell.Item1, cell.Item2]);
        }

    }
}
