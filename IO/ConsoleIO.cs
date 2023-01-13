using SodukoSolverOmega.Configuration.Consts;
using SodukoSolverOmega.Configuration.Exceptions;
using SodukoSolverOmega.SodukoEngine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SodukoSolverOmega.IO
{
    internal class ConsoleIO : I_InputOuput
    {
        public string GetInput()
        {
            return Console.ReadLine();
        }

        //move this function to  a diferent class?
        //maybe board to string
        // dedlete this func

        public void OutputText(string text)
        {
            Console.Write(text);
        }
    }
}
