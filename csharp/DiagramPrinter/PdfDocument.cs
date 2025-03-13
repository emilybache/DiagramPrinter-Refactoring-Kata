namespace DiagramPrinter;

/**
 * This is the real production code class.
 * The implementation is omitted here, imagine it is from a third party library
 * which you can't change.
 */
public sealed class PdfDocument
{
    private PdfDocument()
    {
        // you can't construct this in a unit test
    }

    public bool CopyFile(string fromPath, string targetPath, bool failIfAlreadyExists)
    {
        throw new NotImplementedException();
    }
}