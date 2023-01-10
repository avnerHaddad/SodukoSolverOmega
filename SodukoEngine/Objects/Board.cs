using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SodukoSolverOmega.Configuration.Consts;
using SodukoSolverOmega.SodukoEngine.Algorithems;

namespace SodukoSolverOmega.SodukoEngine.Objects
{
    internal class Board
    {
        public Cell[,] cells;
        public static Dictionary<ValueTuple<int, int>, List<ValueTuple<int, int>>> rowPeers;
        public static Dictionary<ValueTuple<int, int>, List<ValueTuple<int, int>>> colPeers;
        public static Dictionary<ValueTuple<int, int>, List<ValueTuple<int, int>>> boxPeers;
        public HashSet<ValueTuple<int, int>> EffectedSet;
        //fix bug where the new lists are not initialised on the new board




        //setter/getter for the matrix as a whole
        public Cell[,] Cells
        {
            get { return cells; }
            set { cells = value; }
        }
        //adding an iterator for the matrix
        public Cell this[int i, int j]
        {
            get { return cells[i, j]; }
            set { cells[i, j] = value; }
        }

        static Board()
        {
            rowPeers = new Dictionary<ValueTuple<int, int>, List<ValueTuple<int, int>>>();
            colPeers = new Dictionary<ValueTuple<int, int>, List<ValueTuple<int, int>>>();
            boxPeers = new Dictionary<ValueTuple<int, int>, List<ValueTuple<int, int>>>();
            setCellPeers();
        }
        public Board()
        {
            cells = new Cell[Consts.BOARD_HEIGHT, Consts.BOARD_WIDTH];
            EffectedSet = new HashSet<ValueTuple<int, int>>();
            
            
        }

        public void InitialiseConstarints()
        {

            //set possibilities for all
            for (int i = 0; i < Consts.BOARD_HEIGHT; i++)
            {
                for (int j = 0; j < Consts.BOARD_WIDTH; j++)
                {
                    cells[i, j].initList(cells[i, j].Possibilities);
                }
            }

            //remove every fixed cell from possibilities of others
            for (int i = 0; i < Consts.BOARD_HEIGHT; i++)
            {
                for(int j = 0; j < Consts.BOARD_WIDTH; j++)
                {
                    if (cells[i, j].isfilled)
                    {
                        RemoveFromPossibilities(cells[i,j]);
                    }
                    if (cells[i,j].Possibilities.Count == 1)
                    {
                        ValueTuple<int, int> temp = (i, j);
                        HelperFuncs.fixCellHidden(this,temp);
                    }
                }
            }

            ClearEffectedCells();
            //setToMinimumClue();
            //UpdateConstraints();
        }
        /*checks if the board is Valid, no 2 values in the same group*/ 
        public bool IsValidBoard()
        {
            
            List<char> AvailableOptions = new List<char> (Consts.ValOptions);
            List<char> UnusedOptions = new List<char>(AvailableOptions);
            //cehck rows
            for(int i = 0; i < Consts.BOARD_HEIGHT; i++)
            {
                UnusedOptions =  new List<char>(AvailableOptions);
                for (int j = 0; j < Consts.BOARD_WIDTH; j++)
                {
                    if (UnusedOptions.Contains(cells[i, j].Value))
                    {
                        if(cells[i, j].isfilled)
                        {
                            UnusedOptions.Remove(cells[i, j].Value);
                        }
                    }
                    else
                    {
                        return false;
                    }
                    

                }
            }
            //check for cols
            UnusedOptions = new List<char>(AvailableOptions);
            for (int j = 0; j < Consts.BOARD_HEIGHT; j++)
            {
                UnusedOptions = new List<char>(AvailableOptions);
                for (int i = 0; i < Consts.BOARD_WIDTH; i++)
                {
                    if (UnusedOptions.Contains(cells[i, j].Value))
                    {
                        if (cells[i, j].isfilled)
                        {
                            UnusedOptions.Remove(cells[i, j].Value);
                        }
                    }
                    else
                    {
                        return false;
                    }


                }
            }

            //check for boxes
            //create temp list of all available of values 
            UnusedOptions = new List<char>(AvailableOptions);
            for (int i = 0; i < Consts.BOARD_WIDTH; i+=Consts.BOX_SIZE)
            {
                for(int j = 0; j < Consts.BOARD_WIDTH; j+= Consts.BOX_SIZE)
                {
                    UnusedOptions = new List<char>(AvailableOptions);
                    if (cells[i, j].isfilled) { UnusedOptions.Remove(cells[i, j].Value);}
                    foreach (ValueTuple<int,int> Cords in boxPeers[cells[i,j].Cords])
                    {
                        if (UnusedOptions.Contains(cells[Cords.Item1,Cords.Item2].Value))
                        {
                            if (cells[Cords.Item1, Cords.Item2].isfilled)
                            {
                                UnusedOptions.Remove(cells[Cords.Item1, Cords.Item2].Value);
                            }                            
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
            
        }
        public bool isSolvable()
        {
            foreach(Cell cell in cells)
            {
                if (!cell.isfilled)
                {
                    if (!cell.hasPosssibilities)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public bool isSolved()
        {
            foreach(Cell cell in cells)
            {
                if (!cell.isfilled)
                {
                    return false;
                }
            }
            return true;
        }
        public void RemoveFromPossibilities(Cell cell)
        {
            foreach(ValueTuple<int,int> cords in rowPeers[cell.Cords])
            {
                cells[cords.Item1, cords.Item2].Possibilities.Remove(cell.Value);
                EffectedSet.Add(cells[cords.Item1, cords.Item2].Cords);
            }
            foreach (ValueTuple<int, int> cords in colPeers[cell.Cords])
            {
                cells[cords.Item1, cords.Item2].Possibilities.Remove(cell.Value);
                EffectedSet.Add(cells[cords.Item1, cords.Item2].Cords);

            }
            foreach (ValueTuple<int, int> cords in boxPeers[cell.Cords])
            {
                cells[cords.Item1, cords.Item2].Possibilities.Remove(cell.Value);
                EffectedSet.Add(cells[cords.Item1, cords.Item2].Cords);

            }
        }
        public ValueTuple<int,int> pickNextFill()
        {
            //pick next cell with maximum possibilites
            //from these puck the one who constraints the most cells
            //placeholder
            List<ValueTuple<int, int>> maxPossibilities = BacktrackingHueristics.getMaxPossibilityHueristic(this);
            if (maxPossibilities.Count == 1)
            {
                return maxPossibilities[0];

            }
            int MaxDegree = 0;
            ValueTuple<int, int> maxCord = maxPossibilities[0];
            foreach (ValueTuple<int, int> cellCords in maxPossibilities)
            {

                int curDegree = BacktrackingHueristics.GetDegreeHueristic(this, cellCords);
                if (curDegree >= MaxDegree)
                {
                    MaxDegree = curDegree;
                    maxCord = cellCords;
                }
            }
            return maxCord;
        }

        public void ClearEffectedCells()
        {
            EffectedSet.Clear();
        }




        public void PropagateConstraints()
        {
            while (EffectedSet.Count > 0)
            {
                //put all this shit in a func???
                List<ValueTuple<int,int>> toArray = EffectedSet.ToList();
                ValueTuple<int, int> cellCords = toArray[0];
                EffectedSet.Remove(toArray[0]);
                toArray.RemoveAt(0);
                SudokuStrategies.NakedSingles(this,cellCords);
                if (!cells[cellCords.Item1, cellCords.Item2].isfilled)
                {
                    SudokuStrategies.HiddenSingles(this, cellCords);
                }
                

                //do constraints on him

            }
        }


        private Board copyMatrix()
        {
            Board BoardCopy = new Board();
            for(int i = 0; i < Consts.BOARD_HEIGHT; i++)
            {
                for(int j = 0; j < Consts.BOARD_WIDTH; j++)
                {
                    BoardCopy.cells[i,j] = new Cell(cells[i,j].Value,i,j);
                    BoardCopy.cells[i, j].Possibilities = new List<char> (cells[i, j].Possibilities);
                }
            }
            //copies the matrix
            //does not include possibilities
            return BoardCopy;
        }

        public void UpdateConstraints()
        {
            //remove every fixed cell from possibilities of others
            for (int i = 0; i < Consts.BOARD_HEIGHT; i++)
            {
                for (int j = 0; j < Consts.BOARD_WIDTH; j++)
                {
                    if (cells[i, j].isfilled)
                    {
                        RemoveFromPossibilities(cells[i, j]);
                    }
                }
            }
        }


        public Board CreateNextMatrix(int row, int col, char value)
        {
            Board NextMat = copyMatrix();
            //add set val and remove from possibilities to the same func?
            NextMat[row, col].setVal(value);
            NextMat.RemoveFromPossibilities(NextMat[row,col]);
            NextMat.PropagateConstraints();
            return NextMat;
          
        }

        public ValueTuple<int,int> GetNextCell()
        {
;
            List<ValueTuple<int,int>> minPossibilities = BacktrackingHueristics.getMinPossibilityHueristic(this);
            if(minPossibilities.Count == 1)
            {
                return minPossibilities[0];

            }
            int MaxDegree = 0;
            ValueTuple<int, int> maxCord = minPossibilities[0];
            foreach(ValueTuple<int,int> cellCords in minPossibilities)
            {

                int curDegree = BacktrackingHueristics.GetDegreeHueristic(this, cellCords);
                if (curDegree >= MaxDegree){
                    MaxDegree = curDegree;
                    maxCord = cellCords;
                }
            }
            return maxCord;

        }


        




        public static void setCellPeers()
        {
            //func that goes over the initialised board and sets the correct peers for every cell in it

            //iterate over the entire board and call func to get peers
            for (int i = 0; i < Consts.BOARD_HEIGHT; i++)
            {
                for (int j = 0; j < Consts.BOARD_WIDTH; j++)
                {
                    SetPeersForCell(i, j);
                }
            }

        }

        //func recives cords for a cell, adds to it, its corosponding peers
        private static void SetPeersForCell(int row, int col)
        {
            List<ValueTuple<int,int>> CellrowPeers = new List<ValueTuple<int,int>>();
            List<ValueTuple<int, int>> CellcolPeers = new List<ValueTuple<int, int>>();
            List<ValueTuple<int, int>> CellboxPeers = new List<ValueTuple<int, int>>();


            for (int i = 0; i < Consts.BOARD_HEIGHT; i++)
            {
                if (i != col)
                {
                    //if (colPeers[cells[row,i].Cords].Contains(cells[row, i].Cords))
                   // {
                        ValueTuple<int,int> cord = new ValueTuple<int, int>(row, i);
                        CellcolPeers.Add(cord);
                        //cells[row, col].colpeers.Add(cells[row, i].Cords);
                    //}

                }
                if (i != row)
                {
                    //if (rowPeers[cells[i,col].Cords].Contains(cells[i, col].Cords))
                    //{
                    ValueTuple<int, int> cord = new ValueTuple<int, int>(i, col);
                    CellrowPeers.Add(cord);
                        //cells[row, col].rowpeers.Add(cells[i, col].Cords);
                    //}
                }
                int blockRow = row / Consts.BOX_SIZE * Consts.BOX_SIZE + i / Consts.BOX_SIZE;
                int blockCol = col / Consts.BOX_SIZE * Consts.BOX_SIZE + i % Consts.BOX_SIZE;
                if (blockRow != row && blockCol != col)
                {
                    //if (!boxPeers[cells[row,col].Cords].Contains(cells[blockRow, blockCol].Cords))
                    //{
                    //cells[row, col].boxpeers.Add(cells[blockRow, blockCol].Cords);
                    ValueTuple<int, int> cord = new ValueTuple<int, int>(blockRow, blockCol);
                    CellboxPeers.Add(cord);
                    //}
                }
            }
            ValueTuple<int, int> cell = new ValueTuple<int, int>(row, col);
            rowPeers.Add(cell, CellrowPeers);
            colPeers.Add(cell, CellcolPeers);
            boxPeers.Add(cell, CellboxPeers);
        }
        public void setToMinimumClue()
        {
            int maxClue = 0;
            switch (Consts.BOARD_HEIGHT)
            {
                case 4:
                    maxClue = Consts.minGuesses[0];
                    break;
                case 9:
                    maxClue = Consts.minGuesses[1];
                    break;
                case 16:
                    maxClue = Consts.minGuesses[2];
                    break;
                default:
                    maxClue = Consts.minGuesses[3];
                    break;
            }
            while (maxClue > 0)
            {
                ValueTuple<int, int> cellToFill = pickNextFill();
                //set the value
                cells[cellToFill.Item1, cellToFill.Item2].hiddenSet();
                RemoveFromPossibilities(cells[cellToFill.Item1, cellToFill.Item2]);
                maxClue--;

            }
            UpdateConstraints();

        }
        public string ToString()
        {
            int BoxDividerCounter;
            int RowCounter = 1;
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < Consts.BOARD_HEIGHT; i++)
            {
                RowCounter--;
                BoxDividerCounter = Consts.BOX_SIZE;
                if(RowCounter == 0)
                {
                    sb.Append("\n");
                    for (int RowLen = 0; RowLen < Consts.BOARD_WIDTH; RowLen++)
                    {
                        if((RowLen+1)%(Consts.BOX_SIZE) != 0)
                        {
                            sb.Append("");
                        }
                        else
                        {
                            sb.Append("*--");
                            RowLen++;
                        }
                        
                        
                    }
                    RowCounter = Consts.BOX_SIZE;
                }
                sb.Append("\n");
                for(int j = 0; j < Consts.BOARD_HEIGHT; j++)
                {
                    if(BoxDividerCounter == Consts.BOX_SIZE)
                    {
                        sb.Append("| ");
                        BoxDividerCounter = 0;
                    }
                    sb.Append(cells[i,j].ToString());
                    sb.Append(" ");
                    BoxDividerCounter++;
                }
            }
            return sb.ToString();
        }


    }
}
