namespace DiagramPrinter;

public class PrintableDiagram
{

    public FlowchartDiagram Diagram { get; }
    
    public PrintableDiagram(FlowchartDiagram diagram)
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

    public virtual string SummaryInformation()
    {
        return Diagram.SummaryInformation();
    }
}