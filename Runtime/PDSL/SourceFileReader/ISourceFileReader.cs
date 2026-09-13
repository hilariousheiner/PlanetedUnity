namespace Planeted
{
    public interface ISourceFileReader
    {
        bool TryReadSourceFile(string path, out string code);
    }
}