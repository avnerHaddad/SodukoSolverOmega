﻿using System;
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
            Console.Write(output);
        }

        //gets a soduko board object and prints it, will be used later

        public static void PrintSoduko(Board board)
        {
            for(int i = 0; i < 9; i++)
            {
                PrintText("\n");
                for(int j = 0; j < 9; j++)
                {
                    PrintText(Convert.ToString(board[i, j].Value) + " ");
                }
            }
        }

        //gets input from user and checks if its valid
        public static string GetInput(string text)
        {
                PrintText(text);
                string input;
                input = Console.ReadLine();
                if(input.Length > 81)
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
