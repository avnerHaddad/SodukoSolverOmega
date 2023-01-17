using SodukoSolverOmega.Configuration.Consts;
using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Algorithems;

public class HiddenTupple
{
    //for every cell in the board
    //for every group in the cell
    //for every combination in its peers
    //check if it apears tuple times
    //apear: at least one elemnt of it is inside 1 cell
    public bool HiddenTuples(Board board, int tupleSize)
    {
        var valUpdated = false;
        //iterate over all empty board cells
        for (var i = 0; i < Consts.BOARD_SIZE; i++)
        for (var j = 0; j < Consts.BOARD_SIZE; j++)
            if (!board[i, j].Isfilled)
                //for evey combination of 'tuplesize' possibilities check if it exists tuplesize times in each peer group
                //if it does its a hidden tupple, and we remove all the other possibilites from the cells that contain the combo
                foreach (var possibility in GetAllSubsets(board[i, j].possibilities.GetVal(), tupleSize))
                {
                    if (PossibilityCountInPeers(board, possibility, board[i, j].RowPeers) == tupleSize - 1)
                    {
                        //found a pair
                        var temp = board[i, j].possibilities.GetVal();
                        board[i, j].possibilities.SetVal(possibility);
                        if (temp != board[i, j].possibilities.GetVal())
                            //if value was updated state it in the bool
                            valUpdated = true;
                        //find the other tuples
                        foreach (var peercords in board[i, j].RowPeers)
                            if (!board[peercords].Isfilled)
                                if (board[peercords].possibilities.Contains(possibility))
                                {
                                    temp = board[peercords].possibilities.GetVal();
                                    board[peercords].possibilities
                                        .SetVal(Intersect(board[peercords].possibilities.GetVal(), possibility));
                                    if (temp != board[peercords].possibilities.GetVal())
                                        //if value was updated state it in the bool
                                        valUpdated = true;
                                }

                        ;
                    }

                    if (PossibilityCountInPeers(board, possibility, board[i, j].ColPeers) == tupleSize - 1)
                    {
                        //found a pair
                        var temp = board[i, j].possibilities.GetVal();
                        board[i, j].possibilities.SetVal(possibility);
                        if (temp != board[i, j].possibilities.GetVal())
                            //if value was updated state it in the bool
                            valUpdated = true;
                        //find the other tuples
                        foreach (var peercords in board[i, j].ColPeers)
                            if (!board[peercords].Isfilled)
                                if (board[peercords].possibilities.Contains(possibility))
                                {
                                    temp = board[peercords].possibilities.GetVal();
                                    board[peercords].possibilities
                                        .SetVal(Intersect(board[peercords].possibilities.GetVal(), possibility));
                                    if (temp != board[peercords].possibilities.GetVal()) valUpdated = true;
                                }
                    }

                    if (PossibilityCountInPeers(board, possibility, board[i, j].BoxPeers) == tupleSize - 1)
                    {
                        //found a pair
                        var temp = board[i, j].possibilities.GetVal();
                        board[i, j].possibilities.SetVal(possibility);
                        if (temp != board[i, j].possibilities.GetVal()) valUpdated = true;
                        //find the other tuples
                        foreach (var peercords in board[i, j].BoxPeers)
                            if (!board[peercords].Isfilled)
                                if (board[peercords].possibilities.Contains(possibility))
                                {
                                    temp = board[peercords].possibilities.GetVal();
                                    board[peercords].possibilities
                                        .SetVal(Intersect(board[peercords].possibilities.GetVal(), possibility));
                                    if (temp != board[peercords].possibilities.GetVal()) valUpdated = true;
                                }
                    }
                }

        return valUpdated;
    }


    //counts the amount of peers that contain a possibility
    private int PossibilityCountInPeers(Board board, uint possibility, List<ValueTuple<int, int>> peers)
    {
        var count = 0;
        foreach (var cords in peers)
            if (!board[cords].Isfilled)
            {
                if (board[cords].possibilities.Contains(possibility)) ;
                {
                    count++;
                }
            }

        return count;
    }

    //generates all the combinations of set bits given a tuple size
    private List<uint> GetAllSubsets(uint num, int amount)
    {
        var subsets = new List<uint>();
        var setBitsCount = 0;

        // Count the number of set bits in num
        for (var i = 0; i < 32; i++)
            if (((num >> i) & 1) == 1)
                setBitsCount++;
        if (setBitsCount < amount) return subsets;

        // Iterate through all subsets of size "amount"
        for (uint i = 0, subset = (1u << amount) - 1; i < 1u << setBitsCount; i++)
            if ((i & subset) == subset)
            {
                uint set = 0;
                var index = 0;
                for (var j = 0; j < 32; j++)
                    if (((num >> j) & 1) == 1)
                    {
                        if (((i >> index) & 1) == 1) set += (uint)Math.Pow(2, j);
                        index++;
                    }

                subsets.Add(set);
            }

        return subsets;
    }


    //return the common possibilities between two cells possibilities
    private uint Intersect(uint possibilities, uint possibility)
    {
        return possibilities & possibility;
    }
}