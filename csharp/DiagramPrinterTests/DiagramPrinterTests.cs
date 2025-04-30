using System.Text;
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
    public void PrintingPdfDocumentSucceeds()
    {
        var spy = new StringBuilder();
        var printer = new DiagramPrinter.DiagramPrinter();
        var diagram = new SpyPrintableDiagram(spy, "filename", DiagramPrinter.DiagramPrinter.Pdf, true);
        
        var result = printer.PrintDiagram(diagram, "temp", "output.pdf");
        
        Assert.IsTrue(result);
        Assert.AreEqual("Print to File (Filename=filename, folderValid=True)", spy.ToString().Trim());
    }
    
    
    [Test]
    public void PrintingSpreadsheetDocumentSucceeds()
    {
        var spy = new StringBuilder();
        var printer = new DiagramPrinter.DiagramPrinter();
        var diagram = new SpyPrintableDiagram(spy, "filename", DiagramPrinter.DiagramPrinter.Spreadsheet, true);
        
        var result = printer.PrintDiagram(diagram, "temp", "output.xls");
        
        Assert.IsTrue(result);
        Assert.AreEqual("Print to File (Filename=filename, folderValid=True)", spy.ToString().Trim());
    }
    
    [Test]
    public void PrintingPhysicalDocumentSucceeds()
    {
        var spy = new StringBuilder();
        spy.AppendLine();
        var spyPrinter = new SpyPhysicalPrinter(spy);
        var spyQueue = new SpyQueue(spy, spyPrinter);
        var printer = new DiagramPhysicalPrinter(spyPrinter, spyQueue);
        var printerDriverFactory = new PrinterDriverFactoryTestAdapter(spy);
        PrinterDriverFactory.Instance = printerDriverFactory;
        FlowchartDiagram diagram = null;
        var diagramWrapper = new SpyPrintableDiagram(spy, "filename", "PDF", true);
        DiagramMetadata info = new DiagramMetadataTestAdapter("filename", "Physical", true);
        string filename = "random output filename";
        
        var result = printer.DoPrint(diagramWrapper, info, filename);
        
        Assert.IsTrue(result);
        Assert.AreEqual(@"
Added metadata to print queue
Start Document
DiagramPrintDriver is printing to physical printer
End Document
", spy.ToString());
    }

}

public class SpyPhysicalPrinter : PhysicalPrinter
{
    private readonly StringBuilder _stringBuilder;

    public SpyPhysicalPrinter(StringBuilder stringBuilder)
    {
        _stringBuilder = stringBuilder;
        IsAvailable = true;
        TonerLevels[Toner.Black] = 100;
        TonerLevels[Toner.Cyan] = 100;
        TonerLevels[Toner.Magenta] = 100;
        TonerLevels[Toner.Yellow] = 100;
    }

    public override bool StartDocument(bool isSummary, bool isPdf, string name)
    {
        _stringBuilder.AppendLine("Start Document");
        return true;
    }

    public override void EndDocument()
    {
        _stringBuilder.AppendLine("End Document");
    }
}

public class SpyQueue : PrintQueue
{
    private readonly StringBuilder _stringBuilder;

    public SpyQueue(StringBuilder stringBuilder, PhysicalPrinter physicalPrinter) : base(physicalPrinter)
    {
        _stringBuilder = stringBuilder;
    }

    public override void Add(PrintMetadata data)
    {
        base.Add(data);
        _stringBuilder.AppendLine("Added metadata to print queue");
    }
}

public class SpyPrintableDiagram : PrintableDiagram
{
    private readonly StringBuilder _stringBuilder;
    private readonly string _fullFilename;
    private readonly string _fileType;
    private readonly bool _fileAvailable;
    public string Spy { get; private set; } = "";

    public SpyPrintableDiagram(StringBuilder stringBuilder, string fullFilename, string fileType, bool fileAvailable) : base(null)
    {
        _stringBuilder = stringBuilder;
        _fullFilename = fullFilename;
        _fileType = fileType;
        _fileAvailable = fileAvailable;
    }

    public override DiagramMetadata GetDiagramMetadata()
    {
        return new DiagramMetadataTestAdapter(_fullFilename, _fileType, _fileAvailable);
    }

    public override string SummaryInformation()
    {
        return "summary information";
    }

    public override bool PrintToFile(string toFilename, string targetFilename)
    {
        var folderOk = Uri.IsWellFormedUriString(targetFilename, UriKind.RelativeOrAbsolute);
        _stringBuilder.AppendLine($"Print to File (Filename={toFilename}, folderValid={folderOk})");
        return true;
    }

    public override bool PrintToSpreadsheetFile(string fromFilename, string targetFilename)
    {
        return PrintToFile(fromFilename, targetFilename);
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

public class PrinterDriverFactoryTestAdapter : PrinterDriverFactory
{
    private readonly StringBuilder _stringBuilder;

    public PrinterDriverFactoryTestAdapter(StringBuilder stringBuilder)
    {
        _stringBuilder = stringBuilder;
    }

    public override DiagramPrintDriver CreateDriverForPrint()
    {
        return new SpyDiagramPrintDriver(_stringBuilder);
    }
}

public class SpyDiagramPrintDriver : DiagramPrintDriver
{
    private readonly StringBuilder _stringBuilder;

    public SpyDiagramPrintDriver(StringBuilder stringBuilder)
    {
        _stringBuilder = stringBuilder;
    }

    public override bool PrintTo(PhysicalPrinter physicalPrinter)
    {
        _stringBuilder.AppendLine("DiagramPrintDriver is printing to physical printer");
        return true;
    }
}