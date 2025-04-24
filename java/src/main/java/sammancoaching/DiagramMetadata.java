package sammancoaching;

public class DiagramMetadata {
    protected String fullFilename;
    protected String fileType;
    protected boolean fileAvailable;

    public DiagramMetadata(FlowchartDiagram diagram) {
        this.fullFilename = diagram.getName() + "_" + diagram.getSerialNumber();
        this.fileType = diagram.getName().contains("Flowchart") ? "PDF" : "Spreadsheet";
        this.fileAvailable = !diagram.isDisposed();
    }

    protected DiagramMetadata() {
        // Enable subclassing for testing purposes
    }
}
