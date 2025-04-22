namespace DiagramPrinter;

public class PrintQueue
{
    private readonly List<PrintMetadata> _items;
    private readonly PhysicalPrinter _printer;

    public PrintQueue(PhysicalPrinter physicalPrinter)
    {
        this._printer = physicalPrinter;
        this._items = new List<PrintMetadata>();
    }

    public virtual void Add(PrintMetadata data)
    {
        this._items.Add(data);
    }
}