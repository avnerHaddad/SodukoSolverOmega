using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using SodukoSolverOmega.Configuration.Exceptions;
using SodukoSolverOmega.SodukoEngine.Objects;
using SodukoSolverOmega.Configuration.Consts;

namespace SodukoSolverOmega.IO
{
    //deleting this later just keepig cuz its easier to test with
    //deleting this later just keepig cuz its easier to test with
    //deleting this later just keepig cuz its easier to test with
    //deleting this later just keepig cuz its easier to test with
    //deleting this later just keepig cuz its easier to test with
    //deleting this later just keepig cuz its easier to test with
    //deleting this later just keepig cuz its easier to test with
    //deleting this later just keepig cuz its easier to test with
    //deleting this later just keepig cuz its easier to test with
    //deleting this later just keepig cuz its easier to test with
    //deleting this later just keepig cuz its easier to test with


    internal class IOManager
    {
        //prints text to the desired user interface
        public static void PrintText(string output)
        {
            Console.Write(output);
        }

        //gets a soduko board object and prints it, will be used later

        public static void PrintSoduko(Board board)
        {
            for (int i = 0; i < Consts.BOARD_SIZE; i++)
            {
                PrintText("\n");
                for (int j = 0; j < Consts.BOARD_SIZE; j++)
                {
                    PrintText(Convert.ToString(board[i, j].Value) + " ");
                }
            }
        }
        public void WriteAnswerToSourceFile(StreamReader reader, string text)
        {
            string Unsolved = reader.ReadToEnd();

            string Answer = Unsolved + "\n\n\n" + "answer is: \n" + text;

            using (StreamWriter writer = new StreamWriter(reader.BaseStream))
            {
                writer.Write(Answer);
            }
        }
        //gets input from user and checks if its valid
        public static string GetInput(string text)
        {
            PrintText(text);
            string input;
            input = Console.ReadLine();
            if (input.Length > Math.Pow(Consts.BOARD_SIZE, 2))
            {
                throw new BoardTooLongException();
            }
            

            return input;
        }
        public static string GetInput(StreamReader File, string text)
        {
            PrintText(text);
            string input;
            input = File.ReadToEnd();
            if (input.Length > 81)
            {
                throw new BoardTooLongException();
            }
            for (int i = 0; i < input.Length; i++)
            {
                char ch = input[i];
                if (!ConfigurationManager.AppSettings["LegalChars"].Contains(ch))
                {
                    throw new InvalidCharException();
                }

            }

            return input;
        }


    }



}
