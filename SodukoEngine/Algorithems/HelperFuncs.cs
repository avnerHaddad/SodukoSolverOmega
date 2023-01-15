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
        public static bool ExsistInBoxPeers(Board board, ValueTuple<int, int> cell, uint possibility)
        {
            foreach (ValueTuple<int, int> peer in Board.rowPeers[cell])
            {
                if (BitContains(board[peer].Possibilities, possibility) && !board[peer].Isfilled)
                {
                    return true;
                }
            }
            return false;

        }

        //checks if a possibility exsists in colPeers of cell
        public static bool BitContains(uint number, uint contains)
        {
            return ((number & contains) > 0 || contains == 0) ;
        }
        public static bool BitContains(uint number, char contains)
        {
            return BitContains(number, ValueToPossibility(contains));
        }

        public static uint RemovePossibility(uint possibility, uint ToRemove)
        {
            if ((possibility & ToRemove) > 0)
            {
                possibility ^= ToRemove;
                return possibility;
            }

            return possibility;

        }
        public static uint RemoveValue(uint possibility, char remove)
        {
            uint ToRemove = ValueToPossibility(remove);
            if ((possibility & ToRemove) > 0)
            {
                possibility ^= ToRemove;
                return possibility;
            }

            return possibility;

        }

        //converts a value of a cell to a possibility format aka a bitMask
        public static uint ValueToPossibility(char val)
        {
            if (val == '0')
            {
                return 0;
            }
            return (uint)(00000000000000000000000000000000 | (1 << val - 49));
        }
        
        public static int CountOfSetBits(uint number)
        {
            return BitConverter.GetBytes(number)
                .Count(b => b == 1);
        }

        public static List<uint> ListPossibilities(uint Possibilities)
        {
            List<uint> PossibilityCombinations = new List<uint>();
            for (int i = 0; i < 9; i++) {
                if ((Possibilities & (1 << i)) != 0) {
                    PossibilityCombinations.Add((uint)(1 << i));
                }
            }

            return PossibilityCombinations;
        }
        
        public static int FindPosition(uint n)
        {
            int i = 1, pos = 1;
 
            // Iterate through bits of n till we find a set bit
            // i&n will be non-zero only when 'i' and 'n' have a set bit
            // at same position
            while ((i & n) == 0) {
                // Unset current bit and set the next bit in 'i'
                i = i << 1;
 
                // increment position
                ++pos;
            }
 
            return pos;
        }
        public static bool ExsistInColPeers(Board board, ValueTuple<int, int> cell, uint possibility)
        {
            foreach (ValueTuple<int, int> peer in Board.colPeers[cell])
            {
                if (BitContains(board[peer].Possibilities, possibility) && !board[peer].Isfilled)
                {
                    return true;
                }
            }
            return false;

        }

        //checks if a possibility exsists in rowPeers of cell
        public static bool ExsistInRowPeers(Board board, ValueTuple<int, int> cell, uint possibility)
        {
            foreach (ValueTuple<int, int> peer in Board.rowPeers[cell])
            {
                if (BitContains(board[peer].Possibilities, possibility) && !board[peer].Isfilled)
                {
                    return true;
                }
            }
            return false;

        }

        //return all peers that thier Possibilities are a sublist of the possibiltes of curent cell
        public static List<ValueTuple<int, int>> SubPossibilities(Board board, List<ValueTuple<int, int>> peers, uint Possibilities)
        {
            List <ValueTuple<int, int>> SubLists = new();
            foreach (ValueTuple<int,int> peer in peers)
            {
                //check if Possibilities of peer are a sublist of Possibilities param
                //subList bitwise
                if ((board[peer].Possibilities | Possibilities) > board[peer].Possibilities  && !board[peer].Isfilled)
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
                board[peer].Possibilities = (board[peer].Possibilities ^ possibility);
            }
        }

        //return a list of all the cells with the possibility param in the list param
        public static List<ValueTuple<int,int>> AllCellsWithPossibility(Board board, List<ValueTuple<int, int>> peers, char val)
        {
            List<ValueTuple<int,int>> cells = new List<ValueTuple<int,int>>();
            foreach (ValueTuple<int, int> peer in peers)
            {
                
                if ((board[peer].Possibilities & val) > 0)
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
            foreach (ValueTuple<int, int> cell in cells)
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
