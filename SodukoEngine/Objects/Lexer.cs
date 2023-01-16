﻿using System.Threading.Channels;
using SodukoSolverOmega.Configuration.Consts;
using SodukoSolverOmega.Configuration.Exceptions;
using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Solvers;

public class Lexer
{
    private readonly Board board;
    private string boardTxt;
    private char curVal;
    private int pos;


    public Lexer()
    {
        //create a new lexer with an empty board to initialise
        board = new Board();
    }

    //func that initialises the board based on the input, iterates over Boardtxt and creates a board
    private void CreateBoard()
    {
        Console.WriteLine(boardTxt);
        for (var i = 0; i < Consts.BOARD_SIZE; i++)
        {
            for (var j = 0; j < Consts.BOARD_SIZE; j++)
            {
                //if Value is 0 create an empty cell
                int currVal = boardTxt[i * Consts.BOARD_SIZE + j];
                currVal = currVal;
                board[i, j] = currVal == '0' ? new Cell(i, j) : board[i, j] = new Cell((char)currVal, i, j);
                Console.WriteLine($"{i}, {j} -> {board[i,j].Value}, {currVal}");
            }

            Console.WriteLine();
        }
    }
    //public func to get a board from the lexer
    public Board getBoard(string inputTxt)
    {
        Consts.BOARD_SIZE = (int)Math.Sqrt(inputTxt.Length);
        boardTxt = inputTxt;
        CreateBoard();
        return board;
    }
}