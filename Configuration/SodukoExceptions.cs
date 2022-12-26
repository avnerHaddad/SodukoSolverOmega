using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolverOmega.Configuration
{
    internal class SodukoExceptions : Exception

    {
        public SodukoExceptions(string message) : base(message)
        {
        }
    }
    internal class InvalidCharException : SodukoExceptions
    {
        public InvalidCharException() : base("the string you entered contains character that do not allighn with the soduko format")
        {
        }
    }
    internal class BoardTooLongException : SodukoExceptions
    {
        public BoardTooLongException() : base("the string you entered is too long to create a board from")
        {
        }
    }
    internal class UnsolvableSodukoException : SodukoExceptions
    {
        public UnsolvableSodukoException() : base("the soduko you entered is unsolvable and therfore cold not be solved")
        {
        }
    }

}
