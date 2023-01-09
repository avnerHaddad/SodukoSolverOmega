﻿// See https://aka.ms/new-console-template for more information
using System.Configuration;
using static SodukoSolverOmega.IO.IOManager;
using System.Diagnostics;
using SodukoSolverOmega.SodukoEngine.Objects;
using SodukoSolverOmega.SodukoEngine.Solvers;
using SodukoSolverOmega.Configuration.Consts;

internal class Program
{
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
                Board myboard = solver.solve(boardStr);

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

    private static void Main(string[] args)
    {
        SodukoSolver solver = new SodukoSolver();
        var watch = new System.Diagnostics.Stopwatch();
        string inp = GetInput(Consts.inputMsg);
        watch.Start();
        Board solved = solver.solve(inp);
        watch.Stop();
        PrintText(solved.ToString());
        Console.WriteLine(" ");
        Console.WriteLine($"time : {watch.ElapsedMilliseconds} ms");
    }
}