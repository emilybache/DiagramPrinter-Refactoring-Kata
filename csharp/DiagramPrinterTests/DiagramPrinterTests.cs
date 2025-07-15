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
        Assert.That(output, Is.EqualTo(""));
    }

    [Test]
    public void PrintDiagram_EmptyDocument_Fails()
    {
        var printer = new DiagramPrinter.DiagramPrinter();
        var result = printer.PrintDiagram(null);
        Assert.IsFalse(result);
    }

    [Test]
    public void PrintReport_EmptyDocument_Fails()
    {
        var printer = new DiagramPrinter.DiagramPrinter();
        var template = @"Report for FlowchartDiagram {0} {1} {2}";
        var fakeFlowchartReportItems = new FakeFlowchartReportItems("DiagramName", "Serial Number", "Filename");
        var result = printer.PrintReport(null, template);
        Assert.IsFalse(result);
    }
    
    
    [Test]
    public void PrintPages_EmptyDocument_Fails()
    {
        var printer = new DiagramPrinter.DiagramPrinter();
        var result = printer.PrintPages(null, new PagesBuilder());
        Assert.IsFalse(result);
    }
}