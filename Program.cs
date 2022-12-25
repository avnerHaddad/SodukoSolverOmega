// See https://aka.ms/new-console-template for more information
using System.Configuration;
using static SodukoSolverOmega.IOManager;

internal class Program
{
    private static void Main(string[] args)
    {
        PrintText(ConfigurationManager.AppSettings["welcomeMsg"]);
        while (true)
        {
          if(GetInput(ConfigurationManager.AppSettings["inputMsg"]) == "1")
            {
                string boardStr = GetInput(ConfigurationManager.AppSettings["enterBoardMsg"]);
                //create a board object with boardStr
                //call the solve function on it
                //print the solved board or error if unsolvable
            }
            else
            {
                return;
                //close the program
            } 


        }
    }
}