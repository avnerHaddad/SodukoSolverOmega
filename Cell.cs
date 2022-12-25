using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolverOmega
{
    internal class Cell
    {
        private List<Cell> peers;
        
        public Cell()
        {
            peers = new List<Cell>();
        }
        
    }
}
