using SodukoSolverOmega.Configuration.Exceptions;
using SodukoSolverOmega.SodukoEngine.Objects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolverOmega.IO
{
    internal class FileIO : I_InputOuput
    {
        private StreamReader _reader;
        private StreamWriter _writer;
        public FileIO(string Path)
        {
            _reader = new StreamReader(Path);
            _writer = new StreamWriter(Path);
        }

        public void OutputBoard(Board board)
        {
            throw new NotImplementedException();
        }


        public void OutputText(string text)
        {
            string PreAppend = _reader.ReadToEnd();
            string NewText = PreAppend + text;
            _writer.Write(NewText);
            
        }
        public string GetInput()
        {
            return _reader.ReadToEnd();
            
        }
    }
}
