using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolverOmega.Configuration.Consts
{
    internal class Consts
    {
        //number must have a square root
        public static int BOARD_SIZE =25;
        public static int BOX_SIZE = (int)Math.Sqrt(BOARD_SIZE);

        public static string inputMsg = "press 1 to enter a board and anything else to quit";
        public static string welcomeMsg = "hello and welcome to the soduko";
        public static string enterBoardMsg = "enter a board";
        public static string EndMsg = "\n\n\n\n enter another board? \n";
                
    }
}
