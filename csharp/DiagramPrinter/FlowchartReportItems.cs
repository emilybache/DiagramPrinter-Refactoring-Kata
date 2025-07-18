
namespace DiagramPrinter;

public class FlowchartReportItems
{
    public FlowchartReportItems(string name, string serialNumber, string filename)
    {
        // this implementation is deliberately blank - can't use this class in a unit test
    }

    public virtual void Add(string summaryInformation)
    {
        throw new NotImplementedException("can't call from a unit test");
    }

    public virtual object[] ToArray()
    {
        throw new NotImplementedException("can't call from a unit test");
    }
}