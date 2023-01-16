using SodukoSolverOmega.Configuration.Consts;
using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Algorithems;

public class HiddenTupple
{
    
    public bool HiddenTuples(Board board, int tupleSize)
    {
        bool Found = false;
        for (int i = 0; i < Consts.BOARD_SIZE; i++)
        {
            for (int j = 0; j < Consts.BOARD_SIZE; j++)
            {
                if (!board[i, j].Isfilled)
                {
                    foreach (uint possibility in GetAllSubsets(board[i, j].possibilities.val, tupleSize))
                    {
                        if (CountInPeers(board, possibility, board[i, j].RowPeers) == tupleSize-1)
                        {
                            //found a pair
                            uint temp = board[i,j].possibilities.val;
                            board[i, j].possibilities.val = possibility;
                            if (temp != board[i,j].possibilities.val)
                            {
                                Found = true;
                            }
                            //find the other tuples
                            foreach (ValueTuple<int, int> peercords in board[i, j].RowPeers)
                            {
                                if (!board[peercords].Isfilled)
                                {
                                    if (board[peercords].possibilities.BitContains(possibility))
                                    {
                                         temp = board[peercords].possibilities.val;
                                        board[peercords].possibilities.val =
                                            Intersect(board[peercords].possibilities.val, possibility);
                                        if (temp != board[peercords].possibilities.val)
                                        {
                                            Found = true;
                                        }

                                    }
                                }
                            };
                        }

                        if (CountInPeers(board, possibility, board[i, j].ColPeers) == tupleSize-1)
                        {
                            //found a pair
                            uint temp = board[i,j].possibilities.val;
                            board[i, j].possibilities.val = possibility;
                            if (temp != board[i,j].possibilities.val)
                            {
                                Found = true;
                            }
                            //find the other tuples
                            foreach (ValueTuple<int, int> peercords in board[i, j].ColPeers)
                            {
                                if (!board[peercords].Isfilled)
                                {
                                    if (board[peercords].possibilities.BitContains(possibility))
                                    {
                                         temp = board[peercords].possibilities.val;
                                        board[peercords].possibilities.val =
                                            Intersect(board[peercords].possibilities.val, possibility);
                                        if (temp != board[peercords].possibilities.val)
                                        {
                                            Found = true;
                                        }

                                    }
                                }
                            }
                        }

                        if (CountInPeers(board, possibility, board[i, j].BoxPeers) == tupleSize-1)
                        {
                            //found a pair
                            uint temp = board[i,j].possibilities.val;
                            board[i, j].possibilities.val = possibility;
                            if (temp != board[i,j].possibilities.val)
                            {
                                Found = true;
                            }
                            //find the other tuples
                            foreach (ValueTuple<int, int> peercords in board[i, j].BoxPeers)
                            {
                                if(!board[peercords].Isfilled){
                                    if (board[peercords].possibilities.BitContains(possibility))
                                    {
                                         temp = board[peercords].possibilities.val;
                                        board[peercords].possibilities.val = Intersect(board[peercords].possibilities.val, possibility);
                                        if (temp != board[peercords].possibilities.val)
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
        }
        return Found;
    }


    //generate all possible combinations given a possibility func
        //func that gets a combination and counts how many times it apears in the group
        //for every cell in the board
        //for every group in the cell
        //for every combination in its peers
        //check if it apears tuple times
        //apear: at least one elemnt of it is inside peers

        //counts the amount of peers that contain a possibility
        int CountInPeers(Board board, uint possibility, List<ValueTuple<int, int>> peers)
        {
            int count = 0;
            foreach (ValueTuple<int, int> cords in peers)
            {
                if (!board[cords].Isfilled)
                {
                    if ((board[cords].possibilities.BitContains(possibility)));
                    {
                        count++;
                    }
                }
               
            }

            return count;
        }

        //generates all the combinations of set bits given a tuple size
        List<uint> GetAllSubsets(uint num, int amount)
        {
            List<uint> subsets = new List<uint>();
            int setBitsCount = 0;

            // Count the number of set bits in num
            for (int i = 0; i < 32; i++)
            {
                if (((num >> i) & 1) == 1)
                {
                    setBitsCount++;
                }
            }
            if (setBitsCount < amount)
            {
                return subsets;
            }

            // Iterate through all subsets of size "amount"
            for (uint i = 0, subset = (1u << amount) - 1; i < (1u << setBitsCount); i++)
            {
                if ((i & subset) == subset)
                {
                    uint set = 0;
                    int index = 0;
                    for (int j = 0; j < 32; j++)
                    {
                        if (((num >> j) & 1) == 1)
                        {
                            if (((i >> index) & 1) == 1)
                            {
                                set += (uint)Math.Pow(2, j);
                            }
                            index++;
                        }
                    }
                    subsets.Add(set);
                }
            }
            return subsets;
        }



        uint Intersect(uint possibilities, uint possibility)
        {
            return possibilities & possibility;
        }
}