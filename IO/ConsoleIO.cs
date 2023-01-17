namespace SodukoSolverOmega.IO;

public class ConsoleIO : I_InputOuput
{
    public string GetInput()
    {
        return Console.ReadLine();
    }


    public void OutputText(string text)
    {
        Console.Write(text);
    }
}