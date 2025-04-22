namespace DiagramPrinter;


/**
 * This is the real production code class.
 * The implementation is omitted here, imagine it is from a third party library
 * which you can't change.
 */
public sealed class SpreadsheetDocument
{
    private SpreadsheetDocument()
    {
        // can't construct it in a unit test
    }

    public bool CopyFile(string infoFullFilename, string targetFilename, bool overwrite)
    {
        throw new NotImplementedException("Can't call this from a unit test");
    }
}