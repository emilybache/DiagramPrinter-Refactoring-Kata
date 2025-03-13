namespace DiagramPrinter;

public class DiagramMetadata
{
    public string FullFilename { get; internal set; }
    public string FileType { get; internal set; }
    public bool FileAvailable { get; internal set; }

    public DiagramMetadata(ref IDiagram idiagram)
    {
        FullFilename = idiagram.Name() + "_" + idiagram.SerialNumber();
        FileType = idiagram.Name().Contains("Flowchart") ? "PDF" : "Spreadsheet";
        FileAvailable = !idiagram.IsDisposed();
    }
}