using SodukoSolverOmega.Configuration.Consts;
using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Algorithems;

internal class NakedPairs : Constraint
{
    public override bool Solve(Board board, (int, int) Cellcords)
    {
        for(int i = 2; i <= 3; i++)
        {
            if (!board[Cellcords].Isfilled)
            {
                if(NakedCanidates(board, Cellcords, i)){ return true; };
            }
        }
            
        return false;
    }
    public static bool NakedCanidates(Board board, ValueTuple<int, int> cords, int amount)
        {
            if (BitUtils.CountOfSetBits(board[cords].Possibilities) != amount)
            {
                return false;
                //dont bother checking, dont waste time
            }
            //go over all peers, if 'amount' peers are a subList of this cells Possibilities
            //this is a naked'amount'
            bool Success = false;
            List<List<ValueTuple<int, int>>> peerGroups = new();
            peerGroups.Add(Board.rowPeers[cords]);
            peerGroups.Add(Board.colPeers[cords]);
            peerGroups.Add(Board.boxPeers[cords]);
            foreach(List<ValueTuple<int, int>> peerGroup in peerGroups)
            {
                List<ValueTuple<int, int>> subPossibilities = HelperFuncs.SubPossibilities(board, peerGroup, board.cells[cords.Item1, cords.Item2].Possibilities);
                if (subPossibilities.Count == amount - 1)
                {
                    List<ValueTuple<int, int>> NonTupled = peerGroup;
                    //get all cells that are not the naked tuple
                    NonTupled = NonTupled.Except(subPossibilities).ToList();
                    foreach (ValueTuple<int, int> valueTuple in NonTupled)
                    {
                        if (BitUtils.CommonSetBits(board[valueTuple].Possibilities,(board[cords].Possibilities)) > 0 &&
                            !board[valueTuple].Isfilled)
                        {
                            Success = true;
                            board[valueTuple].Possibilities = BitUtils.UniqueBits(board.cells[valueTuple.Item1, valueTuple.Item2].Possibilities, board.cells[cords.Item1, cords.Item2].Possibilities);
                            if (BitUtils.CountOfSetBits(board[valueTuple].Possibilities) == 1)
                            {
                                HelperFuncs.FixCellHidden(board, valueTuple);
                            }
                            else
                            {
                                board.EffectedQueue.Enqueue(valueTuple);

                            }
                        }

                    }

                    
                }
            }
            return Success;
            

        }

}

