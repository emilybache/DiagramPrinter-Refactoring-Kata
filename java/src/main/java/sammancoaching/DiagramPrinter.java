package sammancoaching;

import java.io.IOException;
import java.nio.file.Path;
import java.nio.file.Paths;

/**
 * This is a class you'd like to get under test so you can change it safely.
 */
public class DiagramPrinter {
    public static final String SPREADSHEET = "Spreadsheet";
    public static final String PDF = "PDF";

    public boolean printSummary(FlowchartDiagram diagram, String language, StringBuilder summaryText) {
        if (diagram == null) {
            summaryText.setLength(0); // Clear text
            return false;
        }

        DiagramSummary summary = new DiagramSummary(language);
        summary.addTitle(diagram.getName(), diagram.getSerialNumber());
        summary.addHeader(diagram.getSummaryInformation());
        summary.addImage(diagram.getFlowchartThumbnail());
        summaryText.append(summary.export());
        return true;
    }

    public boolean printDiagram(FlowchartDiagram diagram, String folder, String filename) throws IOException {
        if (diagram == null) {
            return false;
        }

        DiagramMetadata info = new DiagramMetadata(diagram);
        if (PDF.equals(info.fileType)) {
            String targetFilename = getTargetFilename(folder, filename);
            return diagram.getFlowchartAsPdf().copyFile(info.fullFilename, targetFilename, true);
        }

        if (SPREADSHEET.equals(info.fileType)) {
            String targetFilename = getTargetFilename(folder, filename);
            if (!targetFilename.endsWith(".xls")) {
                targetFilename += ".xls";
            }
            return diagram.getFlowchartDataAsSpreadsheet().copyFile(info.fullFilename, targetFilename, true);
        }

        // Default case - print to a physical printer
        return new DiagramPhysicalPrinter().doPrint(diagram, info, getTargetFilename(folder, filename));
    }

    private static String getTargetFilename(String folder, String filename) {
        if (folder == null) {
            folder = System.getProperty("java.io.tmpdir"); // Equivalent to Path.GetTempPath()
        }

        if (filename == null) {
            filename = Paths.get(folder, "tempFile").toString(); // Simulate temp file creation
        }

        return Paths.get(folder, filename).toString();
    }
}
