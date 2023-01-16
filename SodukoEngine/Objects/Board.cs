using System.Text;
using SodukoSolverOmega.Configuration.Consts;
using SodukoSolverOmega.SodukoEngine.Algorithems;

namespace SodukoSolverOmega.SodukoEngine.Objects;

internal class Board
{
    
    public static List<List<ValueTuple<int, int>>> Groups;

    public static uint AvailableOptions;
    public static List<IConstraint> Constraints;
    public Cell[,] cells;

    public int FilledCells;
    //queue holding all of the cells that have beem effected(reduced possiblities) only run constarints on these
    public Queue<ValueTuple<int, int>> EffectedQueue;

    static Board()
    {
        Groups = new List<List<(int, int)>>();
        //initialise constraints/solvong strategies
        Constraints = new List<IConstraint>();
        Constraints.Add(new NakedSingle());
        Constraints.Add(new HiddenSingle());
        Constraints.Add(new NakedPairs());
        //Constraints.Add(new InterSectionRemoval());
        Constraints.Add(new HiddenPairs());
        Constraints.Add(new HiddenTriples());


        //save a list of all legal options
        AvailableOptions = Consts.FULL_BIT;

        //set peers for the dicts
        CreateGroups();
    }

    public Board()
    {
        cells = new Cell[Consts.BOARD_SIZE, Consts.BOARD_SIZE];
        FilledCells = 0;
        EffectedQueue = new Queue<ValueTuple<int, int>>();
    }
    //fix bug where the new lists are not initialised on the new board


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
                    for (var RowLen = 0; RowLen < Consts.BOARD_SIZE; RowLen++)
                        if ((RowLen + 1) % Consts.BOX_SIZE != 0)
                        {
                            sb.Append("");
                        }
                        else
                        {
                            sb.Append("*--");
                            RowLen++;
                        }

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

    private static void CreateGroups()
    {
        //create groups for rows
        var TempGroup = new List<ValueTuple<int, int>>();
        for (var i = 0; i < Consts.BOARD_SIZE; i++)
        {
            for (var j = 0; j < Consts.BOARD_SIZE; j++) TempGroup.Add(new ValueTuple<int, int>(i, j));

            Groups.Add(TempGroup);
        }

        //create groups for cols
        for (var j = 0; j < Consts.BOARD_SIZE; j++)
        {
            for (var i = 0; i < Consts.BOARD_SIZE; i++) TempGroup.Add(new ValueTuple<int, int>(i, j));

            Groups.Add(TempGroup);
        }

        //create group for boxes
        for (var row = 0; row < Consts.BOARD_SIZE; row += Consts.BOX_SIZE)
        for (var col = 0; col < Consts.BOARD_SIZE; col += Consts.BOX_SIZE)
        {
            TempGroup = new List<ValueTuple<int, int>>();
            for (var i = 0; i < Consts.BOX_SIZE; i++)
            for (var j = 0; j < Consts.BOX_SIZE; j++)
                TempGroup.Add(new ValueTuple<int, int>(row + i, col + j));

            Groups.Add(TempGroup);
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
            {
                cells[i, j].InitList();
            }
            else
            {
                FilledCells++;
            }
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
                if (UnusedOptions.BitContains(cells[i, j].Value))
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
                if (UnusedOptions.BitContains(cells[i, j].Value))
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
                if (UnusedOptions.BitContains(this[Cords].Value))
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
    

    public void PropagateConstraints()
    {
        //start with first element of constraints list
        //run it a across all cells
        //if no succces than move on to the next constraint
        //if there is success then move back to first constarint and remove the succcesful cell frrom the queue
        for (var i = 0; i < Constraints.Count; i++)
            if (Constraints[i].Solve(this))
                i = -1;
    }

    //function to get a deep copy of the boards matrix
    public Board CopyMatrix()
    {
        Board BoardCopy = new();
        BoardCopy.FilledCells = this.FilledCells;
        for (var i = 0; i < Consts.BOARD_SIZE; i++)
        {
            for (var j = 0; j < Consts.BOARD_SIZE; j++)
            {
                BoardCopy.cells[i, j] = new Cell(cells[i, j])
                {
                    possibilities = cells[i, j].possibilities,
                    RowPeers = cells[i, j].RowPeers,
                    ColPeers = cells[i, j].ColPeers,
                    BoxPeers = cells[i, j].BoxPeers
                };
            }
        }
        //copies the matrix
        //does not include Possibilities
        return BoardCopy;
    }

    //creates a deep copy of current matrix, places the new val in it and propagates constraints
    

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
                if (blockRow != row || blockCol != col)
                {
                    CellboxPeers.Add(new ValueTuple<int, int>(blockRow,blockCol));
                }
            }

            this[row, col].RowPeers = CellrowPeers;
            this[row, col].ColPeers = CellcolPeers;
            this[row, col].BoxPeers = CellboxPeers;
        }
    }

    //_______________________________________

    //constraint funcs
    //each func call the easier func
    //if easier func cant generate anymore value than proceeds to harder func untill it 
    //generats value and goes back to the easier.
    //we finish once none of the funcs generate value and continue guessing


    //________________________________
}