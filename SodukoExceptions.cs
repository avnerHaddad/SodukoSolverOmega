using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolverOmega
{
    internal class SodukoExceptions : Exception

    {
        public SodukoExceptions(string message) : base(message)
        {
        }
    }
    internal class MyCustomException : SodukoExceptions
    {
        public MyCustomException(string message) : base(message)
        {
        }
    }

}
