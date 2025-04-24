package sammancoaching;

import javax.naming.AuthenticationException;

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
}
