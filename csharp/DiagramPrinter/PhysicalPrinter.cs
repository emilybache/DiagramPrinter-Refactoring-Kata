namespace DiagramPrinter;

public enum Toner
{
    Black,
    Cyan,
    Magenta,
    Yellow
}

public class PhysicalPrinter
{
    public bool IsAvailable { get; protected set; }
    public int JobCount { get; }

    public Dictionary<Toner, int> TonerLevels { get; protected set; } = new()
    {
        { Toner.Black, 0},
        { Toner.Cyan, 0},
        { Toner.Magenta, 0},
        { Toner.Yellow, 0}
    };

    public virtual bool StartDocument(bool isSummary, bool isPdf, string name)
    {
        throw new NotImplementedException("Can't call this from a unit test");
    }

    public virtual void EndDocument()
    {
        throw new NotImplementedException("Can't call this from a unit test");
    }
}