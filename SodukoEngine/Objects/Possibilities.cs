using SodukoSolverOmega.Configuration.Consts;

namespace SodukoSolverOmega.SodukoEngine.Objects;

public class Possibilities
{
    public int count;

    public Possibilities(uint value)
    {
        if (Val == Consts.FULL_BIT)
            count = Consts.BOARD_SIZE;
        else if (Val == 0) count = 0;
        Val = value;
    }

    public Possibilities(Possibilities possibilities)
    {
        Val = possibilities.Val;
        count = possibilities.count;
    }

    private uint Val { get; set; }

    public uint GetVal()
    {
        return Val;
    }

    public void SetVal(uint value)
    {
        Val = value;
        count = CountOfSetBits();
    }

    public static uint ValueToPossibility(char val)
    {
        if (val == '0') return 0;
        return (uint)(00000000000000000000000000000000 | (1 << (val - 49)));
    }

    public int CountOfSetBits()
    {
        var valCopy = Val;
        var count = 0;
        while (valCopy > 0)
        {
            if ((valCopy & 1) == 1) count++;
            valCopy >>= 1;
        }

        return count;
    }

    public void RemoveValue(char remove)
    {
        var ToRemove = ValueToPossibility(remove);
        if ((Val & ToRemove) > 0)
        {
            ToRemove &= Val;
            Val ^= ToRemove;
        }

        count = CountOfSetBits();
    }

    public void RemovePossibility(uint ToRemove)
    {
        if ((Val & ToRemove) > 0) Val ^= ToRemove;

        count = CountOfSetBits();
    }

    public List<uint> ListPossibilities()
    {
        var possibilityCombinations = new List<uint>();
        for (var i = 0; i < Consts.BOARD_SIZE; i++)
            if ((Val & (1 << i)) != 0)
                possibilityCombinations.Add((uint)(1 << i));

        return possibilityCombinations;
    }

    public static int OnlyPossibility(uint n)
    {
        int i = 1, pos = 1;

        //finds the position of the first set bit 
        if (n == 0) return 0;
        while ((i & n) == 0)
        {
            // Unset current bit and set the next bit in 'i'
            i = i << 1;

            // increment position
            ++pos;
        }

        return pos;
    }

    public bool Contains(char contains)
    {
        return Contains(ValueToPossibility(contains));
    }

    public bool Contains(uint contains)
    {
        return (Val & contains) > 0 || contains == 0;
    }

    //checks if a possibility exsists in boxPeers of cell
    public static bool ExsistInBoxPeers(Board board, Cell cell, uint possibility)
    {
        foreach (var peer in cell.BoxPeers)
            if (board[peer].possibilities.Contains(possibility) && !board[peer].Isfilled)
                return true;
        return false;
    }

    public static bool ExsistInColPeers(Board board, Cell cell, uint possibility)
    {
        foreach (var peer in cell.ColPeers)
            if (board[peer].possibilities.Contains(possibility) && !board[peer].Isfilled)
                return true;
        return false;
    }

    //checks if a possibility exsists in rowPeers of cell
    public static bool ExsistInRowPeers(Board board, Cell cell, uint possibility)
    {
        foreach (var peer in cell.RowPeers)
            if (board[peer].possibilities.Contains(possibility) && !board[peer].Isfilled)
                return true;
        return false;
    }
}