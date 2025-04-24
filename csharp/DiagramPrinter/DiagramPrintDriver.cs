namespace DiagramPrinter;

public class DiagramPrintDriver
{
    private FlowchartDiagram? _diagram;

    public void SetDiagram(FlowchartDiagram diagram)
    {
        this._diagram = diagram;
    }

    public void ReleaseDiagram()
    {
        this._diagram = null;
    }

    public virtual bool PrintTo(PhysicalPrinter physicalPrinter)
    {
        throw new NotImplementedException("Can't call this from a unit test");
    }
}