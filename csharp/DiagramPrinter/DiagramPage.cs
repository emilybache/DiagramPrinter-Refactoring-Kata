namespace DiagramPrinter;

public class DiagramPage
{
    public string Name { get; }
    public string PageContents { get; }

    public DiagramPage(string name, string pageContents)
    {
        Name = name;
        PageContents = pageContents;
    }

    public void AddDataFrom(FlowchartReportItems data)
    {
        try
        {
            if (PageContents.Contains("{0}"))
                string.Format(PageContents, data.ToArray());
        }
        catch (System.FormatException e)
        {
        }
        
    }
}