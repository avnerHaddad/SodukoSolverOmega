using SodukoSolverOmega.SodukoEngine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolverOmega.IO
{
    internal interface I_InputOuput
    {
        void OutputText(string text);
        string GetInput();
    }
}
