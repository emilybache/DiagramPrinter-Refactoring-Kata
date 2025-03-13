namespace DiagramPrinter;

public class PngDocument
{
    private readonly string _filename;

    public PngDocument(string filename)
    {
        _filename = filename;
    }

    public string Filename()
    {
        return _filename;
    }
}