using System.Text;
using DiagramPrinter;

namespace DiagramPrinterTests;

public class DiagramPrinterTests
{
    [Test]
    public void PrintSummary_EmptyDocument_Fails()
    {
        var printer = new DiagramPrinter.DiagramPrinter();
        var output = "";
        var result = printer.PrintSummary(null, "swedish", ref output);
        Assert.IsFalse(result);
    }

    [Test]
    public void PrintDiagram_EmptyDocument_Fails()
    {
        var printer = new DiagramPrinter.DiagramPrinter();
        var result = printer.PrintDiagram(null);
        Assert.IsFalse(result);
    }
}