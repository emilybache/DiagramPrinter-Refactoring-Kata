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
    public void PrintReport_SimpleTemplate_Succeeds()
    {
        var printer = new DiagramPrinter.DiagramPrinter();
        var template = @"Report for FlowchartDiagram {0}";
        var subsitutions = new object[]
        {
            "DiagramName",
            
        };
        
        var result = printer.CreateReport(template, subsitutions);
        
        Assert.That(result, Is.EqualTo("Report for FlowchartDiagram DiagramName"));
    }
}