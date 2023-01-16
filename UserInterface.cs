using SodukoSolverOmega.Configuration.Consts;
using SodukoSolverOmega.IO;

namespace SodukoSolverOmega;

public class UserInterface
{
    public static void Start_Solver()
    {
        ConsoleIO console = new ConsoleIO();
        while(true){
        console.OutputText(Consts.welcomeMsg);
        string input = console.GetInput();
        switch (Convert.ToInt32(console.GetInput()))
        {
            case (1):
                console.OutputText("enter a sudoku board");
                //console ,ode
                break;
            case(2):
                //file mode
                console.OutputText("enter file path");
                break;
            case(3):
                //exit to system
                Environment.Exit(0);
                
                break;
            
        }
        }
    }
}