using System.Security.Authentication;

namespace DiagramPrinter;

/**
 * This is the production class used for real.
 * The implementation is largely omitted here, imagine it is much bigger and more complex.
 */
public class FlowchartDiagram
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
    
    public FlowchartReport Report()
    {
        throw new NotImplementedException();
    }

    public FlowchartDiagram Summary()
    {
        throw new NotImplementedException();
    }

    public FlowchartReportItems ValidationProblems(string reportTemplate, List<string> data)
    {
        throw new NotImplementedException();
    }

    public FlowchartReportItems ReportData()
    {
        return new FlowchartReportItems(this.Name(), this.SerialNumber(), this.FlowchartThumbnail().Filename());
    }

    public List<DiagramPage> PagesData()
    {
        // fake implementation
        return new List<DiagramPage>()
        {
            new DiagramPage(this.Name(), "page 1"),
            new DiagramPage(this.Name(), "page 2"),
        };
    }
    // Imagine about 200 more methods here
}

public class FlowchartReport
{
    public void Close()
    {
        throw new NotImplementedException();
    }

    public void Open(bool show)
    {
        throw new NotImplementedException();
    }

    public bool isOpen()
    {
        throw new NotImplementedException();
    }

    public void OpenWithContents(string reportTemplate, FlowchartReportItems substitutions, bool readOnly)
    {
        throw new NotImplementedException();
    }

    public void SaveToFile(string targetFilename)
    {
        throw new NotImplementedException();
    }
}