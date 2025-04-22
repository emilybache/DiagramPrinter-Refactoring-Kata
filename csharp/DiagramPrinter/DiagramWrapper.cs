namespace DiagramPrinter;

public class DiagramWrapper
{

    public IDiagram Diagram { get; }



    public DiagramWrapper(IDiagram diagram)
    {
        Diagram = diagram;
    }

    public virtual DiagramMetadata GetDiagramMetadata()
    {
        return new DiagramMetadata(Diagram);
    }

    public virtual bool PrintToFile(string fromFilename, string targetFilename)
    {
        return Diagram.FlowchartAsPdf().CopyFile(fromFilename, targetFilename, true);
    }
}