using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolverOmega
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

        public void FilterSinglePossibility()
        {
            //goes over board and if a cell has only one possibility it will fill it in
            for (int i = 0; i < Consts.BOARD_HEIGHT; i++)
            {
                for (int j = 0; j < Consts.BOARD_WIDTH; j++)
                {
                    cells[i, j].SinglePossibility();
                }
            }

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
                    cells[row, col].colpeers.Add(cells[row, i]);

                }
                if (i != row)
                {
                    cells[row, col].rowpeers.Add(cells[i, col]);
                }
                int blockRow = row / Consts.BOX_SIZE * Consts.BOX_SIZE + i / Consts.BOX_SIZE;
                int blockCol = col / Consts.BOX_SIZE * Consts.BOX_SIZE + i % Consts.BOX_SIZE;
                if (blockRow != row && blockCol != col)
                {
                    cells[row, col].boxpeers.Add(cells[blockRow, blockCol]);
                }
            }
        }
    }
}
