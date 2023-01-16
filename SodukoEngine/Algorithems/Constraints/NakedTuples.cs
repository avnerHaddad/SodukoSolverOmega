using SodukoSolverOmega.Configuration.Consts;
using SodukoSolverOmega.IO;
using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Algorithems;

internal class NakedTuples
{
    public bool DoNakedTuples(Board board, int tupleSize)
    {
        bool Found = false;
        for (int i = 0; i < Consts.BOARD_SIZE; i++)
        {
            for (int j = 0; j < Consts.BOARD_SIZE; j++)
            {
                if (!board[i, j].Isfilled)
                {
                    if (board[i, j].possibilities.CountOfSetBits() == tupleSize)
                    {
                        //might be a hidden pair lets see
                        //scan all cells and look for a cell with a perfect alighment
                         int count = 0;
                        foreach(var peerCords in board[i,j].ColPeers)
                        {
                            if (board[peerCords].possibilities == board[i, j].possibilities)
                            {
                                count++;
                            }
                            
                        }

                        if (count == tupleSize)
                        {
                            //is naked tuple
                            //remove it
                            foreach(ValueTuple<int,int> peerCords in board[i,j].ColPeers)
                            {
                                if (board[peerCords].possibilities != board[i, j].possibilities)
                                {
                                    uint temp = board[peerCords].possibilities.val;
                                    board[peerCords].possibilities.RemovePossibility(board[i, j].possibilities.val);
                                    if (temp != board[peerCords].possibilities.val)
                                    {
                                        Found = true;

                                    }

                                }
                            
                            }
                            
                        }
                        
                        count = 0;
                        foreach(var peerCords in board[i,j].RowPeers)
                        {
                            if (board[peerCords].possibilities == board[i, j].possibilities)
                            {
                                count++;
                            }
                            
                        }

                        if (count == tupleSize)
                        {
                            //is naked tuple
                            //remove it
                            foreach(var peerCords in board[i,j].RowPeers)
                            {
                                if (board[peerCords].possibilities != board[i, j].possibilities)
                                {
                                    uint temp = board[peerCords].possibilities.val;
                                    board[peerCords].possibilities.RemovePossibility(board[i, j].possibilities.val);
                                    if (temp != board[peerCords].possibilities.val)
                                    {
                                        Found = true;

                                    }

                                }
                            
                            }
                            
                        }
                        
                        count = 0;
                        foreach(var peerCords in board[i,j].BoxPeers)
                        {
                            if (board[peerCords].possibilities == board[i, j].possibilities)
                            {
                                count++;
                            }
                            
                        }

                        if (count == tupleSize)
                        {
                            //is naked tuple
                            //remove it
                            foreach(var peerCords in board[i,j].BoxPeers)
                            {
                                if (board[peerCords].possibilities != board[i, j].possibilities)
                                {
                                    uint temp = board[peerCords].possibilities.val;
                                    board[peerCords].possibilities.RemovePossibility(board[i, j].possibilities.val);
                                    if (temp != board[peerCords].possibilities.val)
                                    {
                                        Found = true;

                                    }

                                }
                            
                            }
                            
                        }
                    }
                }
            }
        }

        return Found;
    }
}