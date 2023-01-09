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


namespace SodukoSolverOmega.SodukoEngine.Objects
{
    internal class Board
    {
        private Cell[,] cells;
        private static Dictionary<Tuple<int, int>, List<Tuple<int, int>>> rowPeers;
        private static Dictionary<Tuple<int, int>, List<Tuple<int, int>>> colPeers;
        private static Dictionary<Tuple<int, int>, List<Tuple<int, int>>> boxPeers;
        private HashSet<Tuple<int, int>> EffectedSet;
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
            rowPeers = new Dictionary<Tuple<int, int>, List<Tuple<int, int>>>();
            colPeers = new Dictionary<Tuple<int, int>, List<Tuple<int, int>>>();
            boxPeers = new Dictionary<Tuple<int, int>, List<Tuple<int, int>>>();
            setCellPeers();
        }
        public Board()
        {
            cells = new Cell[Consts.BOARD_HEIGHT, Consts.BOARD_WIDTH];
            EffectedSet = new HashSet<Tuple<int, int>>();
            
            
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
                }
            }
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
                    foreach (Tuple<int,int> Cords in boxPeers[cells[i,j].Cords])
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
        (int, int) x = new (2, 2);
        public void RemoveFromPossibilities(Cell cell)
        {
            foreach(Tuple<int,int> cords in rowPeers[cell.Cords])
            {
                cells[cords.Item1, cords.Item2].Possibilities.Remove(cell.Value);
                EffectedSet.Add(cells[cords.Item1, cords.Item2].Cords);
            }
            foreach (Tuple<int, int> cords in colPeers[cell.Cords])
            {
                cells[cords.Item1, cords.Item2].Possibilities.Remove(cell.Value);
                EffectedSet.Add(cells[cords.Item1, cords.Item2].Cords);

            }
            foreach (Tuple<int, int> cords in boxPeers[cell.Cords])
            {
                cells[cords.Item1, cords.Item2].Possibilities.Remove(cell.Value);
                EffectedSet.Add(cells[cords.Item1, cords.Item2].Cords);

            }
        }
        public void NakedSingles()
        {
            //check for hidden singles and set them vals
            foreach(Tuple<int,int> cords in EffectedSet.ToList()){

                    if (cells[cords.Item1,cords.Item2].Possibilities.Count == 1 && !cells[cords.Item1,cords.Item2].isfilled)
                    {
                        cells[cords.Item1, cords.Item2].hiddenSet();
                        RemoveFromPossibilities(cells[cords.Item1, cords.Item2]);
                    }
                }
            }
        

        public void PropagateConstraints()
        {
            while(EffectedSet.Count > 0)
            {
                //put all this shit in a func???
                List<Tuple<int,int>> toArray = EffectedSet.ToList();
                Tuple<int, int> cellCords = toArray[0];
                EffectedSet.Remove(toArray[0]);
                toArray.RemoveAt(0);
                NakedSingles();
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
        

        public void HiddenSingles()
        {
                
        }

        //removes possibilities for pairs that are lonely with 2 possibilities in a group
        public void HiddenPairs()
        {
            //if  2 cells in a group contain the same 2 possibilities that do not exsist anywhere else in the group
            //remove all of thier other possibilities
            //remove the 2 possibilities from the second group they are both in, ie if in same row remove from box etc

            //algorithem
            //scan the board
            //check for each cell if it ha

        }

        public Board CreateNextMatrix(int row, int col, char value)
        {
            Board NextMat = copyMatrix();
            //add set val and remove from possibilities to the same func?
            NextMat[row, col].setVal(value);
            NextMat.RemoveFromPossibilities(NextMat[row,col]);
            NextMat.NakedSingles();
            NextMat.PropagateConstraints();
            return NextMat;
          
        }

        public Tuple<int,int> GetNextCell()
        {
            List<Tuple<int,int>> minPossibilities = getMinPossibilityHueristic();
            if(minPossibilities.Count == 1)
            {
                return minPossibilities[0];

            }
            int MaxDegree = 0;
            Tuple<int, int> maxCord = null;
            foreach(Tuple<int,int> cellCords in minPossibilities)
            {

                int curDegree = GetDegreeHueristic(cellCords);
                if (curDegree >= MaxDegree){
                    MaxDegree = curDegree;
                    maxCord = cellCords;
                }
            }
            return maxCord;
            //make degree later, for now return the first one in this list

        }

        public int GetDegreeHueristic(Tuple<int,int> cords)
        {
            int count = 0;
            //return the number of empty cells the current cell has in its peers
            foreach(Tuple<int,int> peerCords in rowPeers[cords])
            {
                if (!cells[peerCords.Item1, peerCords.Item2].isfilled){
                    count++;
                }
            }
            foreach (Tuple<int, int> peerCords in colPeers[cords])
            {
                if (!cells[peerCords.Item1, peerCords.Item2].isfilled)
                {
                    count++;
                }
            }
            foreach (Tuple<int, int> peerCords in boxPeers[cords])
            {
                if (!cells[peerCords.Item1, peerCords.Item2].isfilled)
                {
                    count++;
                }
            }
            return count;
        }
        public List<Tuple<int, int>> getMinPossibilityHueristic()
        {
            List<Tuple<int, int>> LowestPosiibilities = new List<Tuple<int, int>>();
            int minPossibilities = Consts.BOARD_WIDTH;
            //first loop, find the smallest amount of min possibilities
            for(int i = 0; i < Consts.BOARD_HEIGHT; i++)
            {
                for(int j = 0; j < Consts.BOARD_WIDTH; j++)
                {
                    if (cells[i,j].Possibilities.Count < minPossibilities && !cells[i, j].isfilled)
                    {

                        minPossibilities = cells[i,j].Possibilities.Count;
                    }
                }
            }

            //second loop create a list of those who have it
            for (int i = 0; i < Consts.BOARD_HEIGHT; i++)
            {
                for (int j = 0; j < Consts.BOARD_WIDTH; j++)
                {
                    if (cells[i, j].Possibilities.Count == minPossibilities && !cells[i, j].isfilled)
                    {
                        LowestPosiibilities.Add(cells[i, j].Cords);
                    }
                }
            }
            return LowestPosiibilities;
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
            List<Tuple<int,int>> CellrowPeers = new List<Tuple<int,int>>();
            List<Tuple<int, int>> CellcolPeers = new List<Tuple<int, int>>();
            List<Tuple<int, int>> CellboxPeers = new List<Tuple<int, int>>();


            for (int i = 0; i < Consts.BOARD_HEIGHT; i++)
            {
                if (i != col)
                {
                    //if (colPeers[cells[row,i].Cords].Contains(cells[row, i].Cords))
                   // {
                        Tuple<int,int> cord = new Tuple<int, int>(row, i);
                        CellcolPeers.Add(cord);
                        //cells[row, col].colpeers.Add(cells[row, i].Cords);
                    //}

                }
                if (i != row)
                {
                    //if (rowPeers[cells[i,col].Cords].Contains(cells[i, col].Cords))
                    //{
                    Tuple<int, int> cord = new Tuple<int, int>(i, col);
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
                    Tuple<int, int> cord = new Tuple<int, int>(blockRow, blockCol);
                    CellboxPeers.Add(cord);
                    //}
                }
            }
            Tuple<int, int> cell = new Tuple<int, int>(row, col);
            rowPeers.Add(cell, CellrowPeers);
            colPeers.Add(cell, CellcolPeers);
            boxPeers.Add(cell, CellboxPeers);
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
