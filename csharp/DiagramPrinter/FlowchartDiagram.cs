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
        throw new NotImplementedException();
    }

    public string SerialNumber()
    {
        throw new NotImplementedException();
    }

    public bool IsDisposed()
    {
        throw new NotImplementedException();
    }

    public PdfDocument FlowchartAsPdf()
    {
        throw new NotImplementedException();
    }

    public SpreadsheetDocument FlowchartDataAsSpreadsheet()
    {
        throw new NotImplementedException();
    }

    public string SummaryInformation()
    {
        throw new NotImplementedException();
    }

    public PngDocument FlowchartThumbnail()
    {
        throw new NotImplementedException();
    }
}