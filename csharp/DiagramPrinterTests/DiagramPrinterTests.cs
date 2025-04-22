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
        var printer = new DiagramPrinter.DiagramPrinter();
        var diagramWrapper = new TestableDiagramWrapper("filename", "PDF", true);
        var result = printer.PrintDiagram2(diagramWrapper);
        Assert.IsTrue(result);
        Assert.AreEqual("(Filename=filename, folderValid=True)", diagramWrapper.Spy);
    }
    [Test]
    public void PrintingPhysicalDoc()
    {
        var spyPrinter = new SpyPhysicalPrinter();
        var spyQueue = new SpyQueue(spyPrinter);
        var printer = new DiagramPhysicalPrinter(spyPrinter, spyQueue);
        IDiagram diagram = null;
        
        var diagramWrapper = new TestableDiagramWrapper("filename", "PDF", true);
        DiagramMetadata info = new DiagramMetadataTestAdapter("filename", "Physical", true);
        string filename = "random output filename";
        var result = printer.DoPrint(diagramWrapper, info, filename);
        Assert.IsTrue(result);
    }

}

public class SpyPhysicalPrinter : PhysicalPrinter
{
    public string Spy { get; private set; } = "";

    public SpyPhysicalPrinter()
    {
        IsAvailable = true;
    }

    public override bool StartDocument(bool isSummary, bool isPdf, string name)
    {
        Spy += "Start Document";
        return true;
    }

    public override void EndDocument()
    {
        Spy += "End Document";

    }
}

public class SpyQueue : PrintQueue
{
    public string Spy { get; private set; } = "";

    public SpyQueue(PhysicalPrinter physicalPrinter) : base(physicalPrinter)
    {
        
    }
}

public class TestableDiagramWrapper : DiagramWrapper
{
    private readonly string _fullFilename;
    private readonly string _fileType;
    private readonly bool _fileAvailable;
    public string Spy { get; private set; } = "";

    public TestableDiagramWrapper(string fullFilename, string fileType, bool fileAvailable) : base(null)
    {
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
        Spy += $"(Filename={toFilename}, folderValid={folderOk})";
        return true;
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