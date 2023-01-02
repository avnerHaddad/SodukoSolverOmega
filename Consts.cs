using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolverOmega
{
    internal class Consts
    {
        //number must have a sqrt
        public static int BOARD_WIDTH = 25;
        public static int BOARD_HEIGHT = BOARD_WIDTH;
        public static int BOX_SIZE = (int)Math.Sqrt(BOARD_WIDTH);
        public static List<int> LegalChars = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public static String inputMsg = "press 1 to enter a board and anything else to quit";
        public static String welcomeMsg = "hello and welcome to the soduko";
        public static String enterBoardMsg = "enter a board";
        public static char[] ValOptions = { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P','Q'};


    }
}
