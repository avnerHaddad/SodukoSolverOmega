using SodukoSolverOmega.Configuration.Consts;
using SodukoSolverOmega.IO;

namespace SodukoSolverOmega;

public class Ui
{
    public static void start()
    {
        ConsoleIO console = new ConsoleIO();
        console.OutputText(Consts.welcomeMsg);
        switch (Convert.ToInt32(console.GetInput()))
        {
            case(1):
                //console mode
                break;
            case(2):
                //file mode
                
                break;
            case(3):
                //exiting
                Environment.Exit(1);
        }

    }
}