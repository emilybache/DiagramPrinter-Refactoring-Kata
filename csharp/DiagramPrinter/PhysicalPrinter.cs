namespace DiagramPrinter;

public class PhysicalPrinter
{
    public bool IsAvailable { get; }
    public int JobCount { get; }

    public bool StartDocument(bool isSummary, bool isPdf, string name)
    {
        throw new NotImplementedException();
    }

    public void EndDocument()
    {
        throw new NotImplementedException();
    }
}