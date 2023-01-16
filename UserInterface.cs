using SodukoSolverOmega.Configuration.Consts;
using SodukoSolverOmega.Configuration.Exceptions;
using SodukoSolverOmega.IO;
using SodukoSolverOmega.SodukoEngine.Solvers;
using System;
using System.Drawing.Printing;

namespace SodukoSolverOmega;

public class UserInterface
{
    public static void HandleSolving(I_InputOuput manager)
    {
        string input = manager.GetInput();
        if (input.Length > Consts.MAX_STR_LEN || (Math.Sqrt(input.Length) % 1 != 0))
        {
            throw new BoardSizeMismatchExeption();
        }

        foreach (char ch in input)
        {
            if (ch - 48 > Math.Sqrt(input.Length) || ch - 48 < 0)
            {
                throw new InvalidCharException();
            }
        }
        SodukoSolver solver = new SodukoSolver(input);
        string ansewr = solver.Solve().ToString;
        manager.OutputText(ansewr);
       
    }
    public static void Start_Solver()
    {
        ConsoleIO console = new ConsoleIO();
        while(true){
            console.OutputText("\n" + Consts.welcomeMsg);
            switch (Convert.ToInt32(console.GetInput()))
            {
                case (1):
                    console.OutputText("enter a sudoku board");
                    //console ,mode
                    try
                    {
                        HandleSolving(console);

                    }
                    catch (SodukoExceptions e)
                    {
                        console.OutputText(e.Message);
                    }
                    break;
                case(2):
                    //file mode
                    FileIO fileManager = null;
                    console.OutputText("enter file path");
                    string path = console.GetInput();
                    fileManager = new FileIO(path);
                    try
                    {
                        HandleSolving(fileManager);
                    }
                    catch (SodukoExceptions e)
                    {
                        console.OutputText(e.Message);
                    }
                    break;
                case(3):
                    //exit to system
                    Environment.Exit(0);  
                    break;
            
            }
        }
    }

    
}