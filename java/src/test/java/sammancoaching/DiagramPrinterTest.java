package sammancoaching;

import org.approvaltests.Approvals;
import org.approvaltests.core.Options;
import org.approvaltests.scrubbers.RegExScrubber;
import org.junit.jupiter.api.Test;

import java.io.IOException;
import java.net.URI;

import static org.junit.jupiter.api.Assertions.*;

public class DiagramPrinterTest {

    @Test
    public void translatingEmptyDocumentFails() {
        DiagramPrinter printer = new DiagramPrinter();
        StringBuilder output = new StringBuilder();
        boolean result = printer.printSummary(null, "swedish", output);
        assertFalse(result);
    }

    @Test
    public void printingEmptyDocumentFails() throws IOException {
        DiagramPrinter printer = new DiagramPrinter();
        FlowchartDiagram diagram = null;
        boolean result = printer.printDiagram(diagram, null, null);
        assertFalse(result);
    }


    @Test
    public void printingPdfDocumentSucceeds() throws IOException {
        StringBuilder spy = new StringBuilder();
        DiagramPrinter printer = new DiagramPrinter();
        SpyPrintableDiagram diagram = new SpyPrintableDiagram(spy, "filename", DiagramPrinter.PDF, true);

        boolean result = printer.printDiagram(diagram, "temp", "output.pdf");

        assertTrue(result);
        assertEquals("Print to File (Filename=filename, folderValid=true)", spy.toString().trim());
    }

    @Test
    public void printingSpreadsheetDocumentSucceeds() throws IOException {
        StringBuilder spy = new StringBuilder();
        DiagramPrinter printer = new DiagramPrinter();
        SpyPrintableDiagram diagram = new SpyPrintableDiagram(spy, "filename", DiagramPrinter.SPREADSHEET, true);

        boolean result = printer.printDiagram(diagram, "temp", "output.xls");

        assertTrue(result);
        assertEquals("Print to File (Filename=filename, folderValid=true)", spy.toString().trim());
    }

    @Test
    public void printingPhysicalDocumentSucceeds() throws IOException {
        StringBuilder spy = new StringBuilder();
        spy.append("\n");
        SpyPhysicalPrinter spyPrinter = new SpyPhysicalPrinter(spy);
        SpyQueue spyQueue = new SpyQueue(spy, spyPrinter);
        DiagramPhysicalPrinter printer = new DiagramPhysicalPrinter(spyPrinter, spyQueue);
        PrinterDriverFactoryTestAdapter printerDriverFactory = new PrinterDriverFactoryTestAdapter(spy);
        PrinterDriverFactory.setInstance(printerDriverFactory);

        SpyPrintableDiagram diagramWrapper = new SpyPrintableDiagram(spy, "filename", "PDF", true);
        DiagramMetadata info = new DiagramMetadataTestAdapter("filename", "Physical", true);
        String filename = "random output filename";

        boolean result = printer.doPrint(diagramWrapper, info, filename);

        assertTrue(result);
        Approvals.verify(spy, new Options(new RegExScrubber("filename(\\d+).Physical", "[temp filename]")));
    }


}

class SpyPhysicalPrinter extends PhysicalPrinter {
    private final StringBuilder spy;

    public SpyPhysicalPrinter(StringBuilder spy) {
        this.spy = spy;
        this.isAvailable = true;
    }

    @Override
    public boolean startDocument(boolean isSummary, boolean isPdf, String name) {
        spy.append("Start Document\n");
        return true;
    }

    @Override
    public void endDocument() {
        spy.append("End Document\n");
    }
}

class SpyQueue extends PrintQueue {
    private final StringBuilder spy;

    public SpyQueue(StringBuilder spy, PhysicalPrinter physicalPrinter) {
        super(physicalPrinter);
        this.spy = spy;
    }

    @Override
    public void add(PrintMetadata data) {
        super.add(data);
        spy.append("Added metadata to print queue\n");
    }
}

class SpyPrintableDiagram extends PrintableDiagram {
    private final StringBuilder spy;
    private final String fullFilename;
    private final String fileType;
    private final boolean fileAvailable;

    public SpyPrintableDiagram(StringBuilder spy, String fullFilename, String fileType, boolean fileAvailable) {
        super(null);
        this.spy = spy;
        this.fullFilename = fullFilename;
        this.fileType = fileType;
        this.fileAvailable = fileAvailable;
    }

    @Override
    public DiagramMetadata getDiagramMetadata() {
        return new DiagramMetadataTestAdapter(fullFilename, fileType, fileAvailable);
    }

    @Override
    public String getSummaryInformation() {
        return "summary information";
    }

    @Override
    public boolean printToFile(String toFilename, String targetFilename) {
        boolean folderOk = isWellFormedUriString(targetFilename);
        spy.append(String.format("Print to File (Filename=%s, folderValid=%b)\n",
                toFilename, folderOk));
        return true;
    }

    public static boolean isWellFormedUriString(String targetFilename) {
        try {
            new URI(targetFilename);
            return true;
        } catch (Exception e) {
            return false;
        }
    }


}

class DiagramMetadataTestAdapter extends DiagramMetadata {
    public DiagramMetadataTestAdapter(String filename, String fileType, boolean available) {
        this.fullFilename = filename;
        this.fileType = fileType;
        this.fileAvailable = available;
    }
}

class PrinterDriverFactoryTestAdapter extends PrinterDriverFactory {
    private final StringBuilder spy;

    public PrinterDriverFactoryTestAdapter(StringBuilder spy) {
        this.spy = spy;
    }

    @Override
    public DiagramPrintDriver createDriverForPrint() {
        return new SpyDiagramPrintDriver(spy);
    }
}

class SpyDiagramPrintDriver extends DiagramPrintDriver {
    private final StringBuilder spy;

    public SpyDiagramPrintDriver(StringBuilder spy) {
        this.spy = spy;
    }

    @Override
    public boolean printTo(PhysicalPrinter physicalPrinter) {
        spy.append("DiagramPrintDriver is printing to physical printer\n");
        return true;
    }
}


