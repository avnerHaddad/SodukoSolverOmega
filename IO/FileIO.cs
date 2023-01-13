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
            //initialise file reader and writer to path
            _reader = new StreamReader(Path);
            _writer = new StreamWriter(Path);
        }

        public void OutputBoard(Board board)
        {
            //todo
            throw new NotImplementedException();
        }


        public void OutputText(string text)
        {
            //get previously writtes text from file
            string PreAppend = _reader.ReadToEnd();
            //append the new text to it
            string NewText = PreAppend + text;
            //rewrite the file...
            _writer.Write(NewText);
            
        }
        public string GetInput()
        {
            //get input from file, reads the entire file to get it, so file can only contain board
            return _reader.ReadToEnd();
        }
    }
}
