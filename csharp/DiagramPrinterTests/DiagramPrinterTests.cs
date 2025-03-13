using DiagramPrinter;

namespace DiagramPrinterTests;

public class DiagramPrinterTests
{

    [Test]
    public void PrintingEmptyDocumentFails()
    {
        var printer = new DiagramPrinter.DiagramPrinter();
        var result = printer.PrintDiagram(null);
        Assert.IsFalse(result);
    }    
    
    [Test]
    public void TranslatingEmptyDocumentFails()
    {
        var printer = new DiagramPrinter.DiagramPrinter();
        var output = "";
        var result = printer.PrintSummary(null, "swedish", ref output);
        Assert.IsFalse(result);
    }
}