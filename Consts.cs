using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolverOmega
{
    internal class Consts
    {

        public static int BOARD_WIDTH = 9;
        public static int BOARD_HEIGHT = 9;
        public static int BOX_SIZE = 3;
        public static List<int> LegalChars = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public static String inputMsg = "press 1 to enter a board and anything else to quit";
        public static String welcomeMsg = "hello and welcome to the soduko";
        public static String enterBoardMsg = "enter a board";


    }
}
