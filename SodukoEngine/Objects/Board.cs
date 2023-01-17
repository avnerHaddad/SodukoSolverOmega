using System.Text;
using SodukoSolverOmega.Configuration.Consts;
using SodukoSolverOmega.SodukoEngine.Algorithems;

namespace SodukoSolverOmega.SodukoEngine.Objects;

public class Board
{
    public static List<List<ValueTuple<int, int>>> Groups;
    public Cell[,] cells;
    //queue holding all of the cells that have beem effected(reduced possiblities) only run constarints on these
    public Queue<ValueTuple<int, int>> EffectedQueue;
    public int FilledCells;
    public Board()
    {
        cells = new Cell[Consts.BOARD_SIZE, Consts.BOARD_SIZE];
        FilledCells = 0;
        EffectedQueue = new Queue<ValueTuple<int, int>>();
    }
    
    //setter/getter for the matrix as a whole
    public Cell[,] Cells
    {
        get => cells;
        set => cells = value;
    }

    public Cell this[int i, int j]
    {
        get => cells[i, j];
        set => cells[i, j] = value;
    }

    //get cell using a tuple type variable
    public Cell this[ValueTuple<int, int> cords]
    {
        get => cells[cords.Item1, cords.Item2];
        set => cells[cords.Item1, cords.Item2] = value;
    }
    

    public void updatePosssibilities()
    {
        //remove every fixed cell from Possibilities of others
        for (var i = 0; i < Consts.BOARD_SIZE; i++)
        for (var j = 0; j < Consts.BOARD_SIZE; j++)
            if (cells[i, j].Isfilled)
                RemoveFromPossibilities(cells[i, j]);
    }

    public void RemoveFromPossibilities(Cell cell)
    {
        foreach (var cords in cell.getAllPeers())
            if (!this[cords].Isfilled)
            {
                //hide this bit functonality in a func
                this[cords].possibilities.RemovePossibility(Possibilities.ValueToPossibility(cell.Value));
                EffectedQueue.Enqueue(this[cords.Item1, cords.Item2].Cords);
            }
    }

    //clears the queue of the effected cells
    public void ClearEffectedCells()
    {
        EffectedQueue.Clear();
    }

    //called when board is created, initialises possibilites
    //removes possibilities as needed
    //and tries to solve the board using constraints
    public void InitialiseCells()
    {
        SetCellPeers();
        //set Possibilities for all
        for (var i = 0; i < Consts.BOARD_SIZE; i++)
        for (var j = 0; j < Consts.BOARD_SIZE; j++)
            if (!cells[i, j].Isfilled)
                cells[i, j].InitList();
            else
                FilledCells++;
    }


    /*checks if the board is Valid, (no 2 values in the same group)*/
    public bool IsValidBoard()
    {
        Possibilities UnusedOptions;
        //cehck rows
        for (var i = 0; i < Consts.BOARD_SIZE; i++)
        {
            UnusedOptions = new Possibilities(Consts.FULL_BIT);
            for (var j = 0; j < Consts.BOARD_SIZE; j++)
                if (UnusedOptions.Contains(cells[i, j].Value))
                {
                    if (cells[i, j].Isfilled) UnusedOptions.RemoveValue(cells[i, j].Value);
                }
                else
                {
                    return false;
                }
        }

        //check for cols
        for (var j = 0; j < Consts.BOARD_SIZE; j++)
        {
            UnusedOptions = new Possibilities(Consts.FULL_BIT);
            for (var i = 0; i < Consts.BOARD_SIZE; i++)
                if (UnusedOptions.Contains(cells[i, j].Value))
                {
                    if (cells[i, j].Isfilled) UnusedOptions.RemoveValue(cells[i, j].Value);
                }
                else
                {
                    return false;
                }
        }

        //check for boxes
        //create temp list of all available of values 
        for (var i = 0; i < Consts.BOARD_SIZE; i += Consts.BOX_SIZE)
        for (var j = 0; j < Consts.BOARD_SIZE; j += Consts.BOX_SIZE)
        {
            UnusedOptions = new Possibilities(Consts.FULL_BIT);
            if (cells[i, j].Isfilled) UnusedOptions.RemoveValue(cells[i, j].Value);

            foreach (var Cords in cells[i, j].BoxPeers)
                if (UnusedOptions.Contains(this[Cords].Value))
                {
                    if (cells[Cords.Item1, Cords.Item2].Isfilled)
                        UnusedOptions.RemoveValue(this[Cords].Value);
                }
                else
                {
                    return false;
                }
        }

        return true;
    }

    //checks if the board is solvable(every empty cell has possibilities)
    public bool IsSolvable()
    {
        foreach (var cell in cells)
            if (!cell.Isfilled)
                if (!cell.HasPosssibilities)
                    return false;

        return true;
    }

    //checks if the board is solved, all cells are filled
    public bool IsSolved()
    {
        foreach (var cell in cells)
            if (!cell.Isfilled)
                return false;

        return true;
    }

    //gets a cell and removes its val from possibilities of its peers, adds them to effectd queue


    //function to get a deep copy of the boards matrix
    public Board CopyMatrix()
    {
        Board BoardCopy = new();
        BoardCopy.FilledCells = FilledCells;
        for (var i = 0; i < Consts.BOARD_SIZE; i++)
        for (var j = 0; j < Consts.BOARD_SIZE; j++)
            BoardCopy.cells[i, j] = new Cell(cells[i, j])
            {
                possibilities = new Possibilities(cells[i, j].possibilities),
                RowPeers = cells[i, j].RowPeers,
                ColPeers = cells[i, j].ColPeers,
                BoxPeers = cells[i, j].BoxPeers
            };
        //copies the matrix
        return BoardCopy;
    }



    //function that uses heuristics calculations to choose the best next cell to guess on
    public Cell GetNextCell()
    {
        var minPossibilities = BacktrackingHueristics.GetMinPossibilityHueristic(this);
        if (minPossibilities.Count == 1) return minPossibilities[0];

        var MaxDegree = 0;
        var maxCell = minPossibilities[0];
        foreach (var cell in minPossibilities)
        {
            var curDegree = BacktrackingHueristics.GetDegreeHueristic(this, cell);
            if (curDegree >= MaxDegree)
            {
                MaxDegree = curDegree;
                maxCell = cell;
            }
        }

        return maxCell;
    }

    //sets peers for all cells in the board
    public void SetCellPeers()
    {
        //func that goes over the initialised board and sets the correct peers for every cell in it

        //iterate over the entire board and call func to get peers
        for (var i = 0; i < Consts.BOARD_SIZE; i++)
        for (var j = 0; j < Consts.BOARD_SIZE; j++)
            SetPeersForCell(i, j);

        void SetPeersForCell(int row, int col)
        {
            List<ValueTuple<int, int>> CellrowPeers = new();
            List<ValueTuple<int, int>> CellcolPeers = new();
            List<ValueTuple<int, int>> CellboxPeers = new();
            for (var i = 0; i < Consts.BOARD_SIZE; i++)
            {
                if (i != col)
                {
                    var cord = new ValueTuple<int, int>(row, i);
                    CellrowPeers.Add(cord);
                }

                if (i != row)
                {
                    var cord = new ValueTuple<int, int>(i, col);
                    CellcolPeers.Add(cord);
                }

                var blockRow = row / Consts.BOX_SIZE * Consts.BOX_SIZE + i / Consts.BOX_SIZE;
                var blockCol = col / Consts.BOX_SIZE * Consts.BOX_SIZE + i % Consts.BOX_SIZE;
                if (blockRow != row || blockCol != col) CellboxPeers.Add(new ValueTuple<int, int>(blockRow, blockCol));
            }

            this[row, col].RowPeers = CellrowPeers;
            this[row, col].ColPeers = CellcolPeers;
            this[row, col].BoxPeers = CellboxPeers;
        }
    }
    public string ToString
    {
        get
        {
            int BoxDividerCounter;
            var RowCounter = 1;
            var sb = new StringBuilder();
            for (var i = 0; i < Consts.BOARD_SIZE; i++)
            {
                RowCounter--;
                BoxDividerCounter = Consts.BOX_SIZE;
                if (RowCounter == 0)
                {
                    sb.Append("\n");
                    RowCounter = Consts.BOX_SIZE;
                }

                sb.Append("\n");
                for (var j = 0; j < Consts.BOARD_SIZE; j++)
                {
                    if (BoxDividerCounter == Consts.BOX_SIZE)
                    {
                        sb.Append("| ");
                        BoxDividerCounter = 0;
                    }

                    sb.Append(cells[i, j].ToString);
                    sb.Append(" ");
                    BoxDividerCounter++;
                }
            }

            return sb.ToString();
        }
    }

    public string ToCleanString()
    {
        var sb = new StringBuilder();
        for (var i = 0; i < Consts.BOARD_SIZE; i++)
        for (var j = 0; j < Consts.BOARD_SIZE; j++)
            sb.Append(cells[i, j].Value);
        return sb.ToString();
    }
}