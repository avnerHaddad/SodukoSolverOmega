using SodukoSolverOmega.Configuration.Consts;

namespace SodukoSolverOmega.SodukoEngine.Objects;

public class Cell
{
    //creates an empty cell at row i and col j
    public Cell(int i, int j)
    {
        possibilities = new Possibilities(0);
        Value = '0';
        Isfilled = false;
        Cords = new ValueTuple<int, int>(i, j);
    }

    //creates a fixed cell at row i and col j
    public Cell(char val, int i, int j)
    {
        possibilities = new Possibilities(0);
        Isfilled = true;
        Value = val;
        if (val != '0') Isfilled = true;
        Cords = new ValueTuple<int, int>(i, j);
    }

    public Cell(Cell cell)
    {
        possibilities = cell.possibilities;
        Value = cell.Value;
        if (Value != '0') Isfilled = true;
        Cords = cell.Cords;
    }

    //cell cordinates in board
    public ValueTuple<int, int> Cords { get; set; }

    //cell value
    public char Value { get; set; }

    //is cell filled
    public bool Isfilled { get; set; }

    //does cell have possibilites/candiates that can be filled?
    public bool HasPosssibilities => possibilities.GetVal() > 0;

    //return the possibilities of cell
    public Possibilities possibilities { get; set; }

    //list of cords of the cells that exsist in the same row as cell
    public List<ValueTuple<int, int>> RowPeers { get; set; }

    //list of cords of the cells that exsist in the same col as cell
    public List<ValueTuple<int, int>> BoxPeers { get; set; }

    //list of cords of the cells that exsist in the same box as cell
    public List<ValueTuple<int, int>> ColPeers { get; set; }
    public new string ToString => Value.ToString();

    public List<ValueTuple<int, int>> getAllPeers()
    {
        //return a list with all of the cells peers combined
        List<ValueTuple<int, int>> allPeers = new();
        allPeers.AddRange(RowPeers);
        allPeers.AddRange(ColPeers);
        allPeers.AddRange(BoxPeers);
        return allPeers;
    }

    //sets val to param, cleans Possibilities and marks cell as filled
    public void SetVal(uint val)
    {
        SetVal((char)(Possibilities.OnlyPossibility(val) + 48));
    }

    private void SetVal(char val)
    {
        //sets val and clears possibilities
        Value = val;
        possibilities.SetVal(0);
        Isfilled = true;
    }

    //creates a list of character possibilites, based on board size, 1-9 + procceeding asci chars
    public void InitList()
    {
        //set possibilities of cell to all posssible option
        possibilities = new Possibilities(Consts.FULL_BIT);
    }

    //called when found hidden single, sets cell to its only possibility
    public void HiddenSet()
    {
        SetVal((char)(Possibilities.OnlyPossibility(possibilities.GetVal()) + 48));
    }
}