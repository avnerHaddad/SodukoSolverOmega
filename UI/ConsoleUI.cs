using SodukoSolverOmega.Configuration.Consts;
using SodukoSolverOmega.IO;
using SodukoSolverOmega.SodukoEngine.Solvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolverOmega.UI
{
    internal class ConsoleUI : ISudokuUI
    {
        private ConsoleIO Console;
        public ConsoleUI()
        {
            Console = new ConsoleIO();
        }

        //runs at start and prints a nice wewlcome message to user
        public void StartUp()
        {
            Console.OutputText(Consts.welcomeMsg);
        }

        //main loop of the program
        public void UI_Loop()
        {
            
            StartUp(); 
            while (true)
            {
                //ask for input
                Console.OutputText(Consts.inputMsg);
                //get input
                string input = Console.GetInput();
                //use input to create, solve and print the board
                SodukoSolver solver = new SodukoSolver();
                Console.OutputText(solver.Solve(input).ToString);

                //message after solving that tells user how to procceed
                Console.OutputText(Consts.EndMsg);
                //todo: get input to naviagate ui
            }
        }
    }
}
