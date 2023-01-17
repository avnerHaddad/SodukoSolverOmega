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
        string input = "unset";
        try
        {
            input = manager.GetInput();
        }
        catch (BoardSizeMismatchExeption)
        {
            //do
        }
        catch (InvalidCharException)
        {
            //do
        }
        try
        {
            SodukoSolver solver = new SodukoSolver(input);
            string ansewr = solver.Solve().ToString;
            manager.OutputText(ansewr);
        }
        catch (UnsolvableSudokuException)
        {
            //do
        }
    }
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
                    //console ,mode
                    HandleSolving(console);
                    break;
                case(2):
                    //file mode
                    FileIO fileManager = null;
                    console.OutputText("enter file path");
                    string path = console.GetInput();
                    fileManager = new FileIO(path);
                    HandleSolving(fileManager);
                    break;
                case(3):
                    //exit to system
                    Environment.Exit(0);  
                    break;
            
            }
        }
    }

    
}