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
    public void printReport_EmptyDocument_Fails() {
        DiagramPrinter printer = new DiagramPrinter();
        String template = "Report for FlowchartDiagram %s %s %s";
        FakeFlowchartReportItems fakeFlowchartReportItems = new FakeFlowchartReportItems("DiagramName", "Serial Number", "Filename");
        boolean result = printer.printReport(null, template, null, null, false);
        assertFalse(result);
    }

    @Test
    public void printPages_EmptyDocument_Fails() {
        DiagramPrinter printer = new DiagramPrinter();
        boolean result = printer.printPages(null, new PagesBuilder());
        assertFalse(result);
    }

    @Test
    public void validateReport_MatchingTemplate_Succeeds() {
        DiagramPrinter printer = new DiagramPrinter();
        String template = "Report for FlowchartDiagram %s %s %s";
        FakeFlowchartReportItems fakeFlowchartReportItems = new FakeFlowchartReportItems("DiagramName", "Serial Number", "Filename");

        boolean result = printer.validateReport(template, fakeFlowchartReportItems);

        assertTrue(result);
    }
}
