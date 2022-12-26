// See https://aka.ms/new-console-template for more information
using SodukoSolverOmega;
using SodukoSolverOmega.SodukoEngine;
using System.Configuration;
using static SodukoSolverOmega.IOManager;

internal class Program
{
    private static void Main(string[] args)
    {

        PrintText(ConfigurationManager.AppSettings["welcomeMsg"]);
        while (true)
        {
            if (GetInput(ConfigurationManager.AppSettings["inputMsg"]).Equals("1"))
            {
                string boardStr = GetInput(ConfigurationManager.AppSettings["enterBoardMsg"]);
                SodukoSolver solver = new SodukoSolver();
                Board myboard = solver.solve(boardStr);
                PrintSoduko(myboard);
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