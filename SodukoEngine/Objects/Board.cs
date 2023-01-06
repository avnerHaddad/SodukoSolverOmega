using System;
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


        public Board()
        {
            cells = new Cell[Consts.BOARD_HEIGHT, Consts.BOARD_WIDTH];
            
        }

        public bool IsValidBoard()
        {
            List<char> AvailableOptions = Consts.ValOptions.ToList();
            List<char> UnusedOptions = AvailableOptions.ToList();
            //cehck rows
            for(int i = 0; i < Consts.BOARD_HEIGHT; i++)
            {
                UnusedOptions = AvailableOptions.ToList();
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
            UnusedOptions = AvailableOptions.ToList();
            for (int j = 0; j < Consts.BOARD_HEIGHT; j++)
            {
                UnusedOptions = AvailableOptions.ToList();
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
            UnusedOptions = AvailableOptions.ToList();
            for(int j = 0; j < Consts.BOARD_WIDTH; j+=Consts.BOX_SIZE)
            {
                for(int i = 0; i < Consts.BOARD_WIDTH; i += Consts.BOX_SIZE)
                {
                    UnusedOptions = AvailableOptions.ToList();
                    if(cells[i, j].isfilled) { UnusedOptions.Remove(cells[i, j].Value);}
                    foreach (Tuple<int,int> Cords in cells[i, j].boxpeers)
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
        public void UpdateBoard()
        {
            return;
        }

        public void GetDegreeHueristic()
        {
            return;
        }
        public void getPossibilityHueristics()
        {
            return;
        }

        public int calculateHueristics()
        {
            return 0;
        }



        internal void setCellPeers()
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
        private void SetPeersForCell(int row, int col)
        {

            for (int i = 0; i < Consts.BOARD_HEIGHT; i++)
            {
                if (i != col)
                {
                    cells[row, col].colpeers.Add(cells[row, i].Cords);

                }
                if (i != row)
                {
                    cells[row, col].rowpeers.Add(cells[i, col].Cords);
                }
                int blockRow = row / Consts.BOX_SIZE * Consts.BOX_SIZE + i / Consts.BOX_SIZE;
                int blockCol = col / Consts.BOX_SIZE * Consts.BOX_SIZE + i % Consts.BOX_SIZE;
                if (blockRow != row && blockCol != col)
                {
                    cells[row, col].boxpeers.Add(cells[blockRow, blockCol].Cords);
                }
            }
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
