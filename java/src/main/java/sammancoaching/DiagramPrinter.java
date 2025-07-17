package sammancoaching;

import java.util.logging.Logger;
import java.io.File;
import java.io.IOException;
import java.nio.file.Paths;
import java.util.List;
import java.util.ArrayList;
import java.util.IllegalFormatException;

/**
 * This is a class you'd like to get under test so you can change it safely.
 */
public class DiagramPrinter {
    public static final String SPREADSHEET = "Spreadsheet";
    public static final String PDF = "PDF";
    private static final Logger _logger = Logger.getLogger(DiagramPrinter.class.getName());

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



    public boolean printReport(FlowchartDiagram diagram, String reportTemplate, String folder,
                               String filename, boolean summarize) {
        if (diagram == null) {
            return false;
        }

        FlowchartReport report = diagram.report();
        String targetFilename = getTargetFilename(folder, filename);
        _logger.info(String.format("Creating report for %s to file %s", new Object[]{diagram.getName(), targetFilename}));

        if (summarize) {
            diagram = diagram.summary();
            report.close();
            report = diagram.report();
            report.open(true);
            _logger.info("Switched to summary report for " + diagram.getName());
        }

        if (!report.isOpen()) {
            _logger.warning("Failed to open report for writing.");
            return false;
        }

        FlowchartReportItems data = diagram.reportData();

        if (!validateReport(reportTemplate, data)) {
            _logger.warning("Failed to validate report template.");
            return false;
        }

        if (summarize) {
            data.add(diagram.getSummaryInformation());
            report.openWithContents(reportTemplate, data, true);
        } else {
            report.openWithContents(reportTemplate, data, false);
        }

        report.saveToFile(targetFilename);
        _logger.info("Report creation succeeded");
        return true;
    }

    public boolean printPages(FlowchartDiagram diagram, PagesBuilder builder) {
        if (diagram == null) {
            return false;
        }

        FlowchartReportItems data = diagram.reportData();
        List<DiagramPage> pages = diagram.PagesData();

        DiagramPagesReport report = new DiagramPagesReport();
        List<DiagramReportPage> reportPages = new ArrayList<>();

        for (DiagramPage page : pages) {
            DiagramReportPage processedPage = builder.processPage(page, data);
            reportPages.add(processedPage);
        }

        return builder.apply(report, reportPages);
    }

    private String getTargetFilename(String folder, String filename) {
        if (folder == null) {
            folder = System.getProperty("java.io.tmpdir");
        }

        if (filename == null) {
            try {
                filename = File.createTempFile("temp", null).getName();
            } catch (IOException e) {
                _logger.warning("Failed to create temporary file");
                filename = "temp_" + System.currentTimeMillis();
            }
        }

        return Paths.get(folder, filename).toString();
    }

    private boolean validateReport(String template, FlowchartReportItems substitutions) {
        try {
            createReport(template, substitutions.toArray());
            return true;
        } catch (IllegalFormatException e) {
            _logger.warning("Report template did not match substitutions");
            return false;
        }
    }

    public String createReport(String template, Object[] substitutions) {
        return String.format(template, substitutions);
    }
}
