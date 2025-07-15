namespace DiagramPrinter;

public class PagesBuilder
{
    public DiagramReportPage ProcessPage(DiagramPage page, FlowchartReportItems data)
    {
        page.AddDataFrom(data);
        return new DiagramReportPage(page);
    }

    public bool Apply(DiagramPagesReport report, List<DiagramReportPage> reportPages)
    {
        if (report.IsComplete())
        {
            return false;
        }
        foreach (var page in reportPages)
        {
            report.AddPage(page);
        }

        return true;
    }
}