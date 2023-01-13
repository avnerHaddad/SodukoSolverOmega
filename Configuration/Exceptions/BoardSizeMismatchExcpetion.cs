using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolverOmega.Configuration.Exceptions
{
    internal class BoardSizeMismatchExeption : SodukoExceptions
    {
        public BoardSizeMismatchExeption() : base("the string you entered is too long to create a board from")
        {
        }
    }
}
