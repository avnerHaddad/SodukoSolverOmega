using SodukoSolverOmega.Configuration.Consts;
using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Algorithems;

public class NakedTuples
{
    public bool DoNakedTuples(Board board, int tupleSize)
    {
        var Found = false;
        //iterate over all cells in te board that have tupleSize amount of possibilities
        for (var i = 0; i < Consts.BOARD_SIZE; i++)
        for (var j = 0; j < Consts.BOARD_SIZE; j++)
            if (!board[i, j].Isfilled)
                if (board[i, j].possibilities.CountOfSetBits() == tupleSize)
                {
                    //might be a hidden pair, now check
                    //scan all cells and count all the cells with a perfect alighment(exact same possibilities)
                    var count = 0;
                    foreach (var peerCords in board[i, j].ColPeers)
                        if (board[peerCords].possibilities == board[i, j].possibilities)
                            count++;

                    //if there were tupleSize cells like this then this is a naked tuple and we remove all possibilities the tuple has from its peers possibilities 
                    if (count == tupleSize)
                        //is naked tuple
                        //remove it
                        foreach (var peerCords in board[i, j].ColPeers)
                            if (board[peerCords].possibilities != board[i, j].possibilities)
                            {
                                var temp = board[peerCords].possibilities.GetVal();
                                board[peerCords].possibilities.RemovePossibility(board[i, j].possibilities.GetVal());
                                if (temp != board[peerCords].possibilities.GetVal()) Found = true;
                            }

                    //reset count and the same for rowPeers
                    count = 0;
                    foreach (var peerCords in board[i, j].RowPeers)
                        if (board[peerCords].possibilities == board[i, j].possibilities)
                            count++;

                    if (count == tupleSize)
                        //is naked tuple
                        //remove it
                        foreach (var peerCords in board[i, j].RowPeers)
                            if (board[peerCords].possibilities != board[i, j].possibilities)
                            {
                                var temp = board[peerCords].possibilities.GetVal();
                                board[peerCords].possibilities.RemovePossibility(board[i, j].possibilities.GetVal());
                                if (temp != board[peerCords].possibilities.GetVal()) Found = true;
                            }

                    //reset count and the same for Boxpeers
                    count = 0;
                    foreach (var peerCords in board[i, j].BoxPeers)
                        if (board[peerCords].possibilities == board[i, j].possibilities)
                            count++;

                    if (count == tupleSize)
                        //is naked tuple
                        //remove it
                        foreach (var peerCords in board[i, j].BoxPeers)
                            if (board[peerCords].possibilities != board[i, j].possibilities)
                            {
                                var temp = board[peerCords].possibilities.GetVal();
                                board[peerCords].possibilities.RemovePossibility(board[i, j].possibilities.GetVal());
                                if (temp != board[peerCords].possibilities.GetVal()) Found = true;
                            }
                }

        return Found;
    }
}