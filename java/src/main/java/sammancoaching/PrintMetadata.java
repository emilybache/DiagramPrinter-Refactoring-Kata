package sammancoaching;

import java.io.File;
import java.io.IOException;

public class PrintMetadata {
    private final String infoFileType;

    private final File tempFile;

    public PrintMetadata(String infoFileType) throws IOException {
        this.infoFileType = infoFileType;
        tempFile = File.createTempFile("filename", "." + infoFileType);
    }

    public File getFile() {
        return tempFile;
    }

    public String getFilename() {
        return tempFile.getName();
    }

}
