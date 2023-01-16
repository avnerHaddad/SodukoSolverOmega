using SodukoSolverOmega.Configuration.Consts;
using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Algorithems;

internal static class BacktrackingHueristics
{
    //todo
    //add a hueristic that chooses the possibility with the most influence to guess with?

    //return the number of empty cells the current cell has in its peers,
    //used to get the cell that has the most influence when picking a next cell
    public static int GetDegreeHueristic(Board board, Cell cell)
    {
        return GetUnfilledCells(board, cell.getAllPeers()).Count;
    }

    //return a list of cells with the minimum amount of posssiblities,
    //used to pick the least risky next cell
    public static List<Cell> GetMinPossibilityHueristic( Board board)
    {
        List<Cell> LowestPosiibilities = new();
        var minPossibilities = Consts.BOARD_SIZE;
        //first loop, find the smallest amount of min Possibilities
        for (var i = 0; i < Consts.BOARD_SIZE; i++)
        for (var j = 0; j < Consts.BOARD_SIZE; j++)
            if(!board[i, j].Isfilled){
            if (board[i, j].possibilities.CountOfSetBits() < minPossibilities )
                minPossibilities = board[i, j].possibilities.CountOfSetBits();
            }
        //second loop create a list of those who have it
        for (var i = 0; i < Consts.BOARD_SIZE; i++)
        for (var j = 0; j < Consts.BOARD_SIZE; j++)
            if (!board.cells[i, j].Isfilled)
            {
                if (board.cells[i, j].possibilities.CountOfSetBits() == minPossibilities)
                    LowestPosiibilities.Add(board[i, j]);
            }

        return LowestPosiibilities;
    }
    
    public static List<ValueTuple<int, int>> GetUnfilledCells(Board board, List<ValueTuple<int, int>> cells)
    {
        List<ValueTuple<int, int>> Unfilled = new();
        foreach (var cell in cells)
            if (!board[cell].Isfilled)
                Unfilled.Add(cell);
        return Unfilled;
    }

}