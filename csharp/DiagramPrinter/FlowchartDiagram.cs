using System.Security.Authentication;

namespace DiagramPrinter;

/**
 * This is the production class used for real.
 * The implementation is largely omitted here, imagine it is much bigger and more complex.
 */
public class FlowchartDiagram : IDiagram
{
    public FlowchartDiagram()
    {
        throw new AuthenticationException("you can't construct this in a unit test");
    }

    public string Name()
    {
        throw new NotImplementedException("Can't call this from a unit test");
    }

    public string SerialNumber()
    {
        throw new NotImplementedException("Can't call this from a unit test");
    }

    public bool IsDisposed()
    {
        throw new NotImplementedException("Can't call this from a unit test");
    }

    public PdfDocument FlowchartAsPdf()
    {
        throw new NotImplementedException("Can't call this from a unit test");
    }

    public SpreadsheetDocument FlowchartDataAsSpreadsheet()
    {
        throw new NotImplementedException("Can't call this from a unit test");
    }

    public string SummaryInformation()
    {
        throw new NotImplementedException("Can't call this from a unit test");
    }

    public PngDocument FlowchartThumbnail()
    {
        throw new NotImplementedException("Can't call this from a unit test");
    }
}