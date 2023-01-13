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
        //cell cordinates in board
        public ValueTuple<int, int> Cords { get; set; }

        //cell value
        public char Value { get; set; }

        //is cell filled
        public bool Isfilled { get; set; }

        //does cell have possibilites/candiates that can be filled?
        public bool HasPosssibilities { get { return Possibilities.Count > 0; } }

        //return the possibilities of cell
        public List<char> Possibilities { get; set; }

        //creates an empty cell at row i and col j
        public Cell(int i, int j)
        {
            Possibilities = new List<char>();
            Value = '0';
            Isfilled = false;
            Cords = new ValueTuple<int, int>(i, j);

        }

        //creates a fixed cell at row i and col j
        public Cell(char val, int i, int j)
        {
            Possibilities = new List<char>();
            Value = val;
            if (val != '0')
            {
                Isfilled = true;
            }
            Cords = new ValueTuple<int, int>(i, j);

        }

        //rests val to 0 and resets Possibilities to what it was before
        public void ResetVal()
        {
            Value = '0';
            Isfilled = false;
            InitList();
        }

        //sets val to param, cleans Possibilities and marks cell as filled
        public void SetVal(char val)
        {
            Value = val;
            Possibilities.Clear();
            Isfilled = true;
        }

        //creates a list of character possibilites, based on board size, 1-9 + procceeding asci chars
        public void InitList()
        {
            Possibilities.Clear();
            for (int i = 49; i < Consts.BOARD_SIZE + 50; i++)
            {
                Possibilities.Add((char)i);
            }
        }
        public new string ToString => Value.ToString();

        //called when found hidden single, sets cell to its only possibility
        internal void HiddenSet()
        {
            SetVal(Possibilities[0]);
        }
    }
}
