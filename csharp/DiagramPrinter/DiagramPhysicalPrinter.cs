namespace DiagramPrinter;

public class DiagramPhysicalPrinter
{
    private readonly PhysicalPrinter _physicalPrinter;
    private readonly PrintQueue _printQueue;

    public DiagramPhysicalPrinter()
    {
        this._physicalPrinter = new PhysicalPrinter();
        this._printQueue = new PrintQueue(this._physicalPrinter);
    }

    public bool DoPrint(DiagramWrapper diagram, DiagramMetadata info, string targetFilename)
    {
        var printable = PrinterFactory.Instance.CreateItemForPrint();
        printable.SetDiagram(diagram.Diagram);

        var data = new PrintMetadata(info.FileType);
        var mutex = new Mutex(false, "PhysicalPrinterMutex");
        try
        {
            mutex.WaitOne();

            if (PrintDocumentWithSynchronization(data, diagram.Diagram, printable))
            {
                // save a backup of the printed document as pdf
                if (File.Exists(data.Filename))
                {
                    diagram.PrintToFile(data.Filename, targetFilename); 
                    diagram.Diagram.FlowchartAsPdf().CopyFile(data.Filename, targetFilename, true);
                }
            }
        }
        catch (Exception e)
        {
            return false;
        }
        finally
        {
            mutex.ReleaseMutex();
            printable.ReleaseDiagram();
        }
        
        return false;
    }

    private bool PrintDocumentWithSynchronization(PrintMetadata data, IDiagram diagram, Document document)
    {
        var success = false;
        var isSummary = diagram.SummaryInformation().Length > 10;
        try
        {
            success = PrintDocument(data, isSummary, document);
        }
        catch (Exception e)
        {
            success = false;
        }

        return success;
    }

    private bool PrintDocument(PrintMetadata data, bool isSummary, Document document)
    {
        if (!_physicalPrinter.IsAvailable)
        {
            return false;
        }

        if (_physicalPrinter.JobCount < 0)
        {
            return false;
        }

        var printSuccess = false;
        _printQueue.Add(data);
        if (_physicalPrinter.StartDocument(!isSummary, false, "DiagramPhysicalPrinter"))
        {
            if (document.PrintTo(_physicalPrinter))
            {
                printSuccess = true;
            }

            _physicalPrinter.EndDocument();
        }

        return printSuccess;
    }
}