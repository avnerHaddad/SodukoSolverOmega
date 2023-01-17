namespace SodukoSolverOmega.Configuration.Consts;

public static class Consts
{
    //number must have a square root
    public static int BOARD_SIZE = 25;
    public static uint FULL_BIT = (uint)Math.Pow(2, BOARD_SIZE) - 1;
    public static int BOX_SIZE = (int)Math.Sqrt(BOARD_SIZE);

    public static int MAX_LENGTH = (int)Math.Pow(25, 2);

    public static string welcomeMsg =
        "\n\nhello and welcome to the soduko solver, perss 1 for console, 2 for files and anything else to quit";

    //play with these to change runtime
    //board wont run constraints when the number of cells does not exceed this
    public static int NAKED_SINGLE_THRESHOLD = 0;
    public static int HIDDEN_SINGLE_THRESHOLD = BOARD_SIZE * 3;
    public static int HIDDEN_PAIR_THRESHOLD = Convert.ToInt32(BOARD_SIZE);
    public static int HIDDEN_TRIPLE_THRESHOLD = BOARD_SIZE;
    public static int NAKED_PAIRS_THRESHOLD = BOARD_SIZE;
    public static int NAKED_TRIPLE_THRESHOLD = BOARD_SIZE;

    public static void setSize(int length)
    {
        BOARD_SIZE = (int)Math.Sqrt(length);
        FULL_BIT = (uint)Math.Pow(2, BOARD_SIZE) - 1;
        BOX_SIZE = (int)Math.Sqrt(BOARD_SIZE);
    }
}