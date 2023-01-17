using SodukoSolverOmega.Configuration.Consts;
using SodukoSolverOmega.Configuration.Exceptions;
using SodukoSolverOmega.IO;
using SodukoSolverOmega.SodukoEngine.Solvers;

namespace SodukoSolverOmega;

public class UserInterface
{
    public static void SolvingHandler(I_InputOuput manager)
    {
        var input = manager.GetInput();
        //input is larger than 25*25 or doe not have an integer square root
        if (input.Length > Consts.MAX_LENGTH || Math.Sqrt(input.Length) % 1 != 0)
            throw new BoardSizeMismatchExeption();
        Consts.setSize(input.Length);
        foreach (var c in input)
            if (c - 48 > Consts.BOARD_SIZE || c - 48 < 0)
                throw new InvalidCharException();
        var solver = new SodukoSolver(input);
        var ansewr = solver.Solve().ToString;
        manager.OutputText(ansewr);
    }

    public static void Start_Solver()
    {
        var console = new ConsoleIO();
        while (true)
        {
            console.OutputText(Consts.welcomeMsg);
            switch (console.GetInput())
            {
                case "1":
                    console.OutputText("enter a sudoku board");
                    //console ,mode
                    try
                    {
                        SolvingHandler(console);
                    }
                    catch (SodukoExceptions exception)
                    {
                        console.OutputText(exception.Message);
                    }

                    break;
                case "2":
                    //file mode
                    FileIO fileManager = null;
                    console.OutputText("enter file path");
                    try
                    {
                        var path = console.GetInput();
                        fileManager = new FileIO(path);
                    }
                    catch (FileNotFoundException)
                    {
                        console.OutputText("file does not exist");
                        break;
                    }

                    try
                    {
                        SolvingHandler(fileManager);
                    }
                    catch (SodukoExceptions exception)
                    {
                        console.OutputText(exception.Message);
                    }

                    break;
                default:
                    //exit to system
                    console.OutputText("\n goodbye and see you later");
                    Environment.Exit(0);
                    break;
            }
        }
    }
}