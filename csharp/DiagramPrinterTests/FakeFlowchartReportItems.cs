using DiagramPrinter;

namespace DiagramPrinterTests;

/**
 * Use this class in your unit tests to replace a real FlowchartReportItems class
 */
public class FakeFlowchartReportItems : FlowchartReportItems
{
    private List<string> _data = new List<string>();
    
    public FakeFlowchartReportItems(string name, string serialNumber, string filename) : base(name, serialNumber, filename)
    {
        _data = new List<string>
        {
            name,
            serialNumber,
            filename
        };
    }

    public override void Add(string summaryInformation)
    {
        _data.Add(summaryInformation);
    }

    public override object[] ToArray()
    {
        return _data.ToArray();
    }   
}