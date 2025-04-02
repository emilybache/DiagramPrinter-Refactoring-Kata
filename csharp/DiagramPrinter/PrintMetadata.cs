namespace DiagramPrinter;

public class PrintMetadata
{
    private readonly string _infoFileType;

    public PrintMetadata(string infoFileType)
    {
        _infoFileType = infoFileType;
        Filename = Path.GetTempFileName();
        Filename = Path.ChangeExtension(Filename, infoFileType);
    }

    public string Filename { get; }
}