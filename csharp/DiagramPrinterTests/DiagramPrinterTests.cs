using DiagramPrinter;

namespace DiagramPrinterTests;

public class DiagramPrinterTests
{
    
    [Test]
    public void TranslatingEmptyDocumentFails()
    {
        var printer = new DiagramPrinter.DiagramPrinter();
        var output = "";
        var result = printer.PrintSummary(null, "swedish", ref output);
        Assert.IsFalse(result);
    }
    
    [Test]
    public void PrintingEmptyDocumentFails()
    {
        var printer = new DiagramPrinter.DiagramPrinter();
        var result = printer.PrintDiagram(null);
        Assert.IsFalse(result);
    }

    [Test]
    public void PrintingPhysicalDoc()
    {
        var printer = new DiagramPhysicalPrinter();
        IDiagram diagram = null;
        DiagramMetadata info = new DiagramMetadataTestAdapter("filename", "Physical", true);
        string filename = "random output filename";
        var result = printer.DoPrint(diagram, info, filename);
        Assert.IsFalse(result);
    }

}

public class DiagramMetadataTestAdapter : DiagramMetadata
{
    public DiagramMetadataTestAdapter(string filename, string filetype, bool available)
    {
        FullFilename = filename;
        FileType = filetype;
        FileAvailable = available;
    }
    
    
}