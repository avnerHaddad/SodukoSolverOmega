using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolverOmega
{
    internal class Cell
    {
        private List<int> possibilities;
        private int value;
        //refrence to its perrs
        private List<Cell> Rowpeers;
        private List<Cell> Colpeers;
        private List<Cell> Boxpeers;

        public List<Cell> rowpeers
        {
            get { return Rowpeers; }
            set { Rowpeers = value; }
        }
        public List<Cell> colpeers
        {
            get { return Colpeers; }
            set { Colpeers = value; }
        }
        public List<Cell> boxpeers
        {
            get { return Boxpeers; }
            set { Boxpeers = value; }
        }

        public Cell()
        {
            Rowpeers = new List<Cell>();
            Colpeers = new List<Cell>();
            Boxpeers = new List<Cell>();
            possibilities = new List<int>();
            initList(possibilities);
            value = 0;
        }
        public Cell(int val)
        {
            Rowpeers = new List<Cell>();
            Colpeers = new List<Cell>();
            Boxpeers = new List<Cell>();
            possibilities = new List<int>();
            value = val;

        }
        private void initList(List<int> possibilities)
        {
            for (int i = 1; i < 10; i++)
            {
                possibilities.Add(i);
            }
        }

    }
}
