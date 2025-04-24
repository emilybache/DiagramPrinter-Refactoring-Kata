namespace DiagramPrinter;

public class DiagramMetadata
{
    public string FullFilename { get; protected set; }
    public string FileType { get; protected set; }
    public bool FileAvailable { get; protected set; }

    public DiagramMetadata(FlowchartDiagram diagram)
    {
        FullFilename = diagram.Name() + "_" + diagram.SerialNumber();
        FileType = diagram.Name().Contains("Flowchart") ? "PDF" : "Spreadsheet";
        FileAvailable = !diagram.IsDisposed();
    }

    protected DiagramMetadata()
    {
        // for subclass for testing
    }
}