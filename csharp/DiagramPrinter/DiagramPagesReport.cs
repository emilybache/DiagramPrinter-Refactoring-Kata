namespace DiagramPrinter;

public class DiagramPagesReport
{
    private List<DiagramReportPage> pages = new List<DiagramReportPage>();

    public void AddPage(DiagramReportPage page)
    {
        this.pages.Add(page);
    }

    public bool IsComplete()
    {
        return pages.Count != 0;
    }
}