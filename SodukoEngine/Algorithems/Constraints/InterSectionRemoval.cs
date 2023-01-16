/*
using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Algorithems;

internal class InterSectionRemoval : IConstraint
{
    public  bool Solve(Board board, (int, int) Cellcords)
    {
        List<ValueTuple<int,int>> CountInRow, CountInCol, CountInBox;
            foreach(char possibility in BitUtils.ListPossibilities(board[Cellcords].Possibilities))
            {
                CountInRow = HelperFuncs.AllCellsWithPossibility(board, Board.rowPeers[Cellcords], possibility);
                CountInCol = HelperFuncs.AllCellsWithPossibility(board, Board.colPeers[Cellcords], possibility);
                CountInBox = HelperFuncs.AllCellsWithPossibility(board, Board.boxPeers[Cellcords], possibility);
                if(CountInRow.Count >= 2 && CountInRow.Count <= 3)//change to box size?
                {
                    if(CountInRow.Intersect(CountInBox).Count() >= 2)
                    {
                        //found the bitch
                        //remove from box - excepting the row
                        List<ValueTuple<int, int>> InBox = new(Board.boxPeers[Cellcords]);
                        List<ValueTuple<int, int>> toRemove = InBox.Except(CountInRow).ToList();
                        HelperFuncs.RemovePossibilities(board, toRemove, possibility);
                        return true;
                    }
                }
                if (CountInCol.Count >= 2 && CountInCol.Count <= 3)//change to box size?
                {
                    if (CountInCol.Intersect(CountInBox).Count() >= 2)
                    {
                        //found the bitch
                        //remove from box - excpeting the col
                        List<ValueTuple<int, int>> InBox = new(Board.boxPeers[Cellcords]);
                        List<ValueTuple<int, int>> toRemove = InBox.Except(CountInCol).ToList();
                        HelperFuncs.RemovePossibilities(board, toRemove, possibility);
                        return true;
                    }
                }
                if (CountInBox.Count >= 2 && CountInBox.Count <= 3)//change to box size?
                {
                    if (CountInBox.Intersect(CountInCol).Count() >= 2)
                    {
                        //found the bitch
                        //remove from col - excpeting the box
                        List<ValueTuple<int, int>> Incol = new(Board.colPeers[Cellcords]);
                        List<ValueTuple<int, int>> toRemove = Incol.Except(CountInBox).ToList();
                        HelperFuncs.RemovePossibilities(board, toRemove, possibility);
                        return true;
                    }
                }
                if (CountInBox.Count >= 2 && CountInBox.Count <= 3)//change to box size?
                {
                    if (CountInBox.Intersect(CountInRow).Count() >= 2)
                    {
                        //found the bitch
                        //remove from row - expecting
                        List<ValueTuple<int, int>> Inrow = new(Board.rowPeers[Cellcords]);
                        List<ValueTuple<int, int>> toRemove = Inrow.Except(CountInBox).ToList();
                        HelperFuncs.RemovePossibilities(board, toRemove, possibility);
                        return true;
                    }
                }


            }
            return false;
    }
}
*/

