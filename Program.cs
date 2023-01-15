﻿// See https://aka.ms/new-console-template for more information
using System.Configuration;
using System.Diagnostics;
using SodukoSolverOmega.SodukoEngine.Objects;
using SodukoSolverOmega.SodukoEngine.Solvers;
using SodukoSolverOmega.Configuration.Consts;
using SodukoSolverOmega.IO;

internal class Program
{


    private static void Main(string[] args)
    {
        ConsoleIO console = new ConsoleIO();
        string inp = console.GetInput();
        SodukoSolver solver = new SodukoSolver(inp);
        var watch = new System.Diagnostics.Stopwatch();
        watch.Start();
        Board solved = solver.Solve();
        watch.Stop();
        console.OutputText(solved.ToString);
        console.OutputText(" ");
        console.OutputText($"time : {watch.ElapsedMilliseconds} ms");
    }
}
/*
public void SodukoMode()
{
    var watch = new System.Diagnostics.Stopwatch();

    PrintText(Consts.welcomeMsg);
    while (true)
    {
        if (GetInput(Consts.inputMsg).Equals("1"))
        {
            string boardStr = GetInput(Consts.enterBoardMsg);

            watch.Start();

            SodukoSolver solver = new SodukoSolver();
            Board myboard = solver.Solve(boardStr);

            watch.Stop();
            PrintSoduko(myboard);
            Console.WriteLine(" ");
            Console.WriteLine($"time : {watch.ElapsedMilliseconds} ms");
            //create a board object with boardStr
            //call the solve function on it
            //print the solved board or error if unsolvable
        }
        else
        {
            break;
            //close the program
        }


    }
    return;
}     
}
*/