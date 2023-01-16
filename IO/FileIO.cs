using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.IO;

internal class FileIO : I_InputOuput
{
    private readonly StreamReader _reader;
    private readonly StreamWriter _writer;

    public FileIO(string Path)
    {
        //initialise file reader and writer to path
        _reader = new StreamReader(Path);
        _writer = new StreamWriter(Path);
    }


    public void OutputText(string text)
    {
        //get previously writtes text from file
        var PreAppend = _reader.ReadToEnd();
        //append the new text to it
        var NewText = PreAppend + text;
        //rewrite the file...
        _writer.Write(NewText);
    }

    public string GetInput()
    {
        //get input from file, reads the entire file to get it, so file can only contain board
        return _reader.ReadToEnd();
    }

    public void OutputBoard(Board board)
    {
        //todo
        throw new NotImplementedException();
    }
}