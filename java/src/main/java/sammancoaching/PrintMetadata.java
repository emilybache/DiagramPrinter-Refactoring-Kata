package sammancoaching;

import java.io.File;
import java.io.IOException;

public class PrintMetadata {
    private final String infoFileType;
    private final String filename;

    public PrintMetadata(String infoFileType) throws IOException {
        this.infoFileType = infoFileType;
        File tempFile = File.createTempFile("temp", "." + infoFileType);
        this.filename = tempFile.getAbsolutePath();
    }

    public String getFilename() {
        return filename;
    }
}
