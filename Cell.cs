﻿using System;
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
        private bool fixedNum;
        private bool isFilled;

        public int Value { get { return value; } }
        public bool isfilled { get { return isFilled; } }
        public bool hasPosssibilities { get { return possibilities.Count > 0; } }
        public void resetVal(){ value = 0; }
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
            fixedNum = false;
            isFilled = false;
        }
        public Cell(int val)
        {
            Rowpeers = new List<Cell>();
            Colpeers = new List<Cell>();
            Boxpeers = new List<Cell>();
            possibilities = new List<int>();
            value = val;
            fixedNum = true; ;
            isFilled = true;

        }

        //func that tests all the remaining values for the cell and places the first one that does not conflict
        //return true if found a value to place
        public bool Guess()
        {
            foreach( int guess in possibilities)
            {
                if (isValid(guess))
                {
                    value = guess;
                    isFilled=true;
                    possibilities.Remove(guess);
                    return true;
                }
            }
            return false;

        }

        //checks if the testVal exsits in one of the cells peers and return false if does
        public bool isValid(int testVal)
        {
            foreach(Cell cell in Rowpeers)
            {
                if(cell.Value == testVal)
                {
                    return false;
                }
            }
            foreach (Cell cell in Colpeers)
            {
                if (cell.Value == testVal)
                {
                    return false;
                }
            }
            foreach (Cell cell in Boxpeers)
            {
                if (cell.Value == testVal)
                {
                    return false;
                }
            }
            return true;
        }

        //func that sets up the possibilities list, fills it with nums from 1-9
        private void initList(List<int> possibilities)
        {
            for (int i = 1; i < 10; i++)
            {
                possibilities.Add(i);
            }
        }

    }
}
