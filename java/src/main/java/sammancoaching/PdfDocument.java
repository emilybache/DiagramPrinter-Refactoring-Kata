package sammancoaching;

/**
 * This is the real production code class.
 * The implementation is omitted here, imagine it is from a third party library
 * which you can't change.
 */
public final class PdfDocument {

    private PdfDocument() {
        // You can't construct this in a unit test
    }

    public boolean copyFile(String fromPath, String targetPath, boolean failIfAlreadyExists) {
        throw new UnsupportedOperationException("Can't call this from a unit test");
    }
}
