namespace DiagramPrinter;

public class DiagramSummary
{
    private readonly string _language;
    private string contents = "";

    public DiagramSummary(string language)
    {
        _language = language;
    }

    public void AddHeader(string summaryInformation)
    {
        contents += summaryInformation;
        contents += "\n";
    }

    public void AddTitle(string name, string serialNumber)
    {
        contents += name;
        contents += "\n";
        contents += serialNumber;
        contents += "\n";
    }

    public void AddImage(PngDocument flowchart)
    {
        contents += flowchart.Filename();
    }

    public string Export()
    {
        // imagine a lot more detail and complexity here
        return contents;
    }
}