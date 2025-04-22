namespace DiagramPrinter;

public class PhysicalPrinter
{
    public bool IsAvailable { get; protected set; }
    public int JobCount { get; }

    public virtual bool StartDocument(bool isSummary, bool isPdf, string name)
    {
        throw new NotImplementedException("Can't call this from a unit test");
    }

    public virtual void EndDocument()
    {
        throw new NotImplementedException("Can't call this from a unit test");
    }
}