package sammancoaching;

/**
 * This is the real production code class.
 * The implementation is omitted here, imagine it is from a third party library
 * which you can't change.
 */
public final class SpreadsheetDocument
{
    private SpreadsheetDocument()
    {
        // can't construct it in a unit test
    }

    public boolean copyFile(String infoFullFilename, String targetFilename, boolean overwrite)
    {
        throw new RuntimeException("cannot be called from a unit test");
    }
}