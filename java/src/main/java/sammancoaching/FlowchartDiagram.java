package sammancoaching;

import javax.naming.AuthenticationException;
import java.util.ArrayList;
import java.util.List;

public class FlowchartDiagram {

    public FlowchartDiagram() throws AuthenticationException {
        throw new RuntimeException("You can't construct this in a unit test");
    }

    public String getName() {
        throw new RuntimeException("Can't call this from a unit test");
    }

    public String getSerialNumber() {
        throw new RuntimeException("Can't call this from a unit test");
    }

    public boolean isDisposed() {
        throw new RuntimeException("Can't call this from a unit test");
    }

    public PdfDocument getFlowchartAsPdf() {
        throw new RuntimeException("Can't call this from a unit test");
    }

    public SpreadsheetDocument getFlowchartDataAsSpreadsheet() {
        throw new RuntimeException("Can't call this from a unit test");
    }

    public String getSummaryInformation() {
        throw new RuntimeException("Can't call this from a unit test");
    }

    public PngDocument getFlowchartThumbnail() {

        throw new RuntimeException("Can't call this from a unit test");
    }

    public FlowchartReport report() {
        throw new RuntimeException("Can't call this from a unit test");
    }

    public FlowchartDiagram summary() {
        throw new RuntimeException("Can't call this from a unit test");
    }

    public FlowchartReportItems ValidationProblems(String reportTemplate, List<String> data) {
        throw new RuntimeException("Can't call this from a unit test");
    }

    public FlowchartReportItems reportData() {
        return new FlowchartReportItems(this.getName(), this.getSerialNumber(), this.getFlowchartThumbnail().getFilename());
    }

    public List<DiagramPage> PagesData() {
        // fake implementation
        var list = new ArrayList<DiagramPage>();
        list.add(new DiagramPage(this.getName(), "page 1"));
        list.add(new DiagramPage(this.getName(), "page 2"));
        return list;
    }
}
