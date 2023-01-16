using SodukoSolverOmega.Configuration.Consts;

namespace SodukoSolverOmega.SodukoEngine.Objects;

internal class Possibilities
{
    public uint val { get; set;}

    public Possibilities(uint value)
    {
        val = value;
    }
    public static uint ValueToPossibility(char val)
    {
        if (val == '0') return 0;
        return (uint)(00000000000000000000000000000000 | (1 << (val - 49)));
    }

    public char PossibilityValue()
    {
        return (char)(FindPosition(val) + 48);
    }
    
    public  int CountOfSetBits()
    {
        int count = 0;
        while (val> 0) {
            if ((val & 1) == 1) {
                count++;
            }
            val >>= 1;
        }
        return count;
    }
    public void  RemoveValue(char remove)
    {
        var ToRemove = ValueToPossibility(remove);
        if ((val & ToRemove) > 0)
        {
            ToRemove &= val;
            val ^= ToRemove;
        }
    }
    public  void RemovePossibility(uint ToRemove)
    {
        if ((val & ToRemove) > 0)
        {
            val ^= ToRemove;
        }
    }
    public List<uint> ListPossibilities()
    {
        var possibilityCombinations = new List<uint>();
        for (var i = 0; i < Consts.BOARD_SIZE; i++)
            if ((val & (1 << i)) != 0)
                possibilityCombinations.Add((uint)(1 << i));

        return possibilityCombinations;
    }
    public static int FindPosition(uint n)
    {
        int i = 1, pos = 1;

        // Iterate through bits of n till we find a set bit
        // i&n will be non-zero only when 'i' and 'n' have a set bit
        // at same position
        if (n == 0)
        {
            return 0;
        }
        while ((i & n) == 0)
        {
            // Unset current bit and set the next bit in 'i'
            i = i << 1;

            // increment position
            ++pos;
        }

        return pos;
    }
    public  bool BitContains(char contains)
    {
        return BitContains(ValueToPossibility(contains));
    }
    public  bool BitContains(uint contains)
    {
        return (val & contains) > 0 || contains == 0;
    }
    //checks if a possibility exsists in boxPeers of cell
    public static bool ExsistInBoxPeers(Board board, Cell cell, uint possibility)
    {
        foreach (var peer in cell.BoxPeers)
            if (board[peer].possibilities.BitContains(possibility) && !board[peer].Isfilled)
                return true;
        return false;
    }

    public static bool ExsistInColPeers(Board board, Cell cell, uint possibility)
    {
        foreach (var peer in cell.ColPeers)
            if (board[peer].possibilities.BitContains(possibility) && !board[peer].Isfilled)
                return true;
        return false;
    }

    //checks if a possibility exsists in rowPeers of cell
    public static bool ExsistInRowPeers(Board board, Cell cell, uint possibility)
    {
        foreach (var peer in cell.RowPeers)
            if (board[peer].possibilities.BitContains(possibility) && !board[peer].Isfilled)
                return true;
        return false;
    }

}