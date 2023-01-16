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
        for (var i = 0; i < Consts.BOARD_SIZE; i++)
        for (var j = 0; j < Consts.BOARD_SIZE; j++)
        {
            //if Value is 0 create an empty cell
            board[i, j] = curVal == '0' ? new Cell(i, j) : board[i, j] = new Cell(curVal, i, j);
            Next();
        }
    }

    //advances our iterator over the string and updates the curVal param
    private void Next()
    {
        if (pos < Consts.MAX_STR_LEN-1)
        {
            pos++;
            curVal = boardTxt[pos];
            
        }
        else
        {
            throw new BoardSizeMismatchExeption();
        }
    }

    //public func to get a board from the lexer
    public Board getBoard(string inputTxt)
    {
        boardTxt = inputTxt;
        pos = 0;
        curVal = boardTxt[pos];
        CreateBoard();
        return board;
    }
}