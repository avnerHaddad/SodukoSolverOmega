using SodukoSolverOmega.Configuration.Consts;
using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Algorithems;

internal class BitUtils
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
            for (int i = 0; i < Consts.BOARD_SIZE; i++) {
                if ((Possibilities & (1 << i)) != 0) {
                    PossibilityCombinations.Add((uint)(1 << i));
                }
            }

            return PossibilityCombinations;
        }

        public static uint UniqueBits(uint valA, uint valB)
        {
            uint mask = valA & valB;
            valA ^= mask;
            return valA;
        }
        public static int CommonSetBits(uint possibilitiesA, uint posssibilitiesB)
        {
            possibilitiesA &= posssibilitiesB;
            return CountOfSetBits(possibilitiesA);
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

        public static void addPossibility(uint foundInRow, char possibility)
        {
            foundInRow |= possibility;
        }
        
    
}