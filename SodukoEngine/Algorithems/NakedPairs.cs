using SodukoSolverOmega.Configuration.Consts;
using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Algorithems;
/*
internal class NakedPairs : Constraint
{
    public override bool Solve(Board board, (int, int) Cellcords)
    {
        for(int i = 2; i <= Consts.BOX_SIZE; i++)
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
            if (HelperFuncs.CountOfSetBits(board[cords].Possibilities) != amount)
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
                        if (board[valueTuple].Possibilities.Intersect(board.cells[cords.Item1, cords.Item2].Possibilities).Count() > 0 &&
                            !board[valueTuple].Isfilled)
                        {
                            Success = true;
                            board[valueTuple].Possibilities = board.cells[valueTuple.Item1, valueTuple.Item2].Possibilities.Except(board.cells[cords.Item1, cords.Item2].Possibilities).ToList();
                            if (HelperFuncs.CountOfSetBits(board[valueTuple].Possibilities) == 1)
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
*/
