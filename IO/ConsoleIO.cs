using SodukoSolverOmega.Configuration.Consts;
using SodukoSolverOmega.Configuration.Exceptions;

namespace SodukoSolverOmega.IO;

public class ConsoleIO : I_InputOuput
{
    public string GetInput()
    {
        return Console.ReadLine();
    }

    //move this function to  a diferent class?
    //maybe board to string
    // dedlete this func

    public void OutputText(string text)
    {
        Console.Write(text);
    }
}