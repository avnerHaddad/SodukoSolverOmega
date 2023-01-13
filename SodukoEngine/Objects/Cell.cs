using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SodukoSolverOmega.Configuration.Consts;

namespace SodukoSolverOmega.SodukoEngine.Objects
{
    internal class Cell
    {
        //possivilities and value
        private List<char> possibilities;
        private char value;
        private ValueTuple<int, int> cords;
        //refrence to its perrs
        private List<ValueTuple<int, int>> Rowpeers;
        private List<ValueTuple<int, int>> Colpeers;
        private List<ValueTuple<int, int>> Boxpeers;
        //helper vars
        private bool isFilled;

        public ValueTuple<int, int> Cords { get { return cords; } set { cords = value; } }
        public List<ValueTuple<int, int>> rowpeers
        {
            get { return Rowpeers; }
            set { Rowpeers = value; }
        }
        public List<ValueTuple<int, int>> colpeers
        {
            get { return Colpeers; }
            set { Colpeers = value; }
        }
        public List<ValueTuple<int, int>> boxpeers
        {
            get { return Boxpeers; }
            set { Boxpeers = value; }
        }
        public char Value { get { return value; } }
        public bool Isfilled { get { return isFilled; } }
        public bool HasPosssibilities { get { return possibilities.Count > 0; } }

        public List<char> Possibilities { get { return possibilities; } set { possibilities = value; } }

        public Cell(int i, int j)
        {
            possibilities = new List<char>();
            //initList(possibilities);
            value = '0';
            isFilled = false;
            Cords = new ValueTuple<int, int>(i, j);

        }
        public Cell(char val, int i, int j)
        {
            possibilities = new List<char>();
            //initList(possibilities);
            value = val;
            if (val != '0')
            {
                isFilled = true;
            }
            Cords = new ValueTuple<int, int>(i, j);
            //eliminatePeersPossibility();

        }

        //rests val to 0 and resets possibilities to what it was before
        public void ResetVal()
        {
            value = '0';
            isFilled = false;
            InitList();
        }


        //creates a backuup of the old possibilities before algorithems




        //func that tests all the remaining values for the cell and places the first one that does not conflict
        //return true if found a value to place

        //checks if the testVal exsits in one of the cells peers and return false if does

        public void SetVal(char val)
        {
            value = val;
            possibilities.Remove(val);
            //possibilities.Remove(val);
            isFilled = true;
            //eliminatePeersPossibility();
            //NakedPairs();
            //InterSectionRemoval();
        }
        public void InitList()
        {
            possibilities.Clear();
            for (int i = 49; i < Consts.BOARD_SIZE + 50; i++)
            {
                possibilities.Add((char)i);
            }
        }
        public string ToString()
        {
            return value.ToString();
        }

        internal void HiddenSet()
        {
            //called when found hidden single
            SetVal(possibilities[0]);
        }
    }
}
