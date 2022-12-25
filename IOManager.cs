using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SodukoSolverOmega
{
    
    internal class IOManager
    {
        //prints text to the desired user interface
        public static void PrintText(string output)
        {
            Console.WriteLine(output);
        }

        //gets a soduko board object and prints it, will be used later

        public static void PrintSoduko(Board board)
        {
            for(int i = 0; i < 9; i++)
            {
                PrintText("\n");
                for(int j = 0; j < 9; j++)
                {
                    PrintText(Convert.ToString(board[i, j].Value));
                }
            }
        }

        //gets input from user and checks if its valid
        public static string GetInput(string text)
        {
            PrintText(text);
            try
            {
                string input = Console.ReadLine();
                for(int i = 0; i < input.Length; i++)
                {
                    if (ConfigurationManager.AppSettings[input[i]] == null)
                    {
                        PrintText("illegal Character found, aborting");
                        return null;
                    }
                }
               
                return input;
            }
            catch (Exception ex)
            {
                PrintText(text + Environment.NewLine + ex.Message);
                return null;
            }
            
        }



    }
}
