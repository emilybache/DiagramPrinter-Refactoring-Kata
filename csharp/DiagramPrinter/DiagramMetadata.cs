namespace DiagramPrinter;

public class DiagramMetadata
{
    public string FullFilename { get; protected set; }
    public string FileType { get; protected set; }
    public bool FileAvailable { get; protected set; }

    public DiagramMetadata(IDiagram idiagram)
    {
        FullFilename = idiagram.Name() + "_" + idiagram.SerialNumber();
        FileType = idiagram.Name().Contains("Flowchart") ? "PDF" : "Spreadsheet";
        FileAvailable = !idiagram.IsDisposed();
    }

    protected DiagramMetadata()
    {
        // for subclass for testing
    }
}