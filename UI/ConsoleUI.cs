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

        public void StartUp()
        {
            Console.OutputText(Consts.welcomeMsg);
        }

        public void UI_Loop()
        {
            StartUp(); 
            while (true)
            {
                Console.OutputText(Consts.inputMsg);
                //get input
                string input = Console.GetInput();
                SodukoSolver solver = new SodukoSolver();
                Console.OutputText(solver.Solve(input).ToString);
                Console.OutputText(Consts.EndMsg);
            }
        }
    }
}
