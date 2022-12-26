using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolverOmega.SodukoEngine
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
            cells = new Cell[9, 9];
        }

        internal void setCellPeers()
        {
            //func that goes over the initialised board and sets the correct peers for every cell in it

            //iterate over the entire board and call func to get peers
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    SetPeersForCell(i, j);
                }
            }

        }

        //func recives cords for a cell, adds to it, its corosponding peers
        private void SetPeersForCell(int row, int col)
        {

            for (int i = 0; i < 9; i++)
            {
                if (i != col)
                {
                    cells[row, col].colpeers.Add(cells[row, i]);

                }
                if (i != row)
                {
                    cells[row, col].rowpeers.Add(cells[i, col]);
                }
                int blockRow = row / 3 * 3 + i / 3;
                int blockCol = col / 3 * 3 + i % 3;
                if (blockRow != row && blockCol != col)
                {
                    cells[row, col].boxpeers.Add(cells[blockRow, blockCol]);
                }
            }
        }
    }
}
