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
        public void OutputBoard(Board board)
        {
            for (int i = 0; i < Consts.BOARD_HEIGHT; i++)
            {
                OutputText("\n");
                for (int j = 0; j < Consts.BOARD_WIDTH; j++)
                {
                    OutputText(Convert.ToString(board[i, j].Value) + " ");
                }
            }
        }

        public void OutputText(string text)
        {
            Console.Write(text);
        }
    }
}
