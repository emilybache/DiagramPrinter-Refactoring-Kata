namespace DiagramPrinter;

public class Document
{
    private IDiagram _diagram;

    public void SetDiagram(IDiagram diagram)
    {
        this._diagram = diagram;
    }

    public void ReleaseDiagram()
    {
        this._diagram = null;
    }

    public bool PrintTo(PhysicalPrinter physicalPrinter)
    {
        return true;
    }
}