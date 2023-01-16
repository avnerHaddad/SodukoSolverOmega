namespace SodukoSolverOmega.Configuration.Consts;

internal class Consts
{
    //number must have a square root
    public static int BOARD_SIZE = 4;
    public static uint FULL_BIT = (uint)Math.Pow(2, BOARD_SIZE) - 1;
    public static int BOX_SIZE = (int)Math.Sqrt(BOARD_SIZE);

    public static string inputMsg = "press 1 to enter a board and anything else to quit";
    public static string welcomeMsg = "hello and welcome to the soduko";
    public static string enterBoardMsg = "enter a board";
    public static string EndMsg = "\n\n\n\n enter another board? \n";

    //play with these
    public static int NAKED_SINGLE_THRESHOLD = 0;
    public static int HIDDEN_SINGLE_THRESHOLD = BOARD_SIZE*3;
    public static int HIDDEN_PAIR_THRESHOLD = Convert.ToInt32(BOARD_SIZE*12);
    public static int HIDDEN_TRIPLE_THRESHOLD = BOARD_SIZE-1;
    public static int NAKED_PAIRS_THRESHOLD = BOARD_SIZE*2;
}