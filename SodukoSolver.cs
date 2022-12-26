﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolverOmega
{
    internal class SodukoSolver
    {
        private Lexer lexer;
        private Board BoardToSolve;


        public SodukoSolver()
        {
            lexer = new Lexer();
            BoardToSolve = new Board();
        }
    }
}
