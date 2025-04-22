namespace DiagramPrinter;
using Microsoft.Extensions.Logging;
public class DiagramPhysicalPrinter
{
    private readonly PhysicalPrinter _physicalPrinter;
    private readonly PrintQueue _printQueue;
    private readonly ILogger<DiagramPhysicalPrinter> _logger = LoggingProvider.CreateLogger<DiagramPhysicalPrinter>();

    public DiagramPhysicalPrinter(PhysicalPrinter physicalPrinter, PrintQueue printQueue)
    {
        this._physicalPrinter = physicalPrinter;
        this._printQueue = printQueue;
    }

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
        var success = false;
        try
        {
            mutex.WaitOne();
            
            if (!_physicalPrinter.IsAvailable)
            {
                _logger.LogInformation("Physical Printer Unavailable");
                success = false;
            }
            else if (_physicalPrinter.JobCount < 0)
            {
                _logger.LogInformation("Physical Printer Has Negative Job Count - Printer Is Unavailable");
                success = false;
            }
            else
            {
                // Print the diagram using the Physical Printer
                _printQueue.Add(data);
                var summaryInformation = diagram.SummaryInformation();
                _logger.LogInformation("Diagram Summary Information {summaryInformation}", summaryInformation);
                var isSummary = summaryInformation.Length > 10;
                if (_physicalPrinter.StartDocument(!isSummary, false, "DiagramPhysicalPrinter"))
                {
                    if (printable.PrintTo(_physicalPrinter))
                    {
                        _logger.LogInformation("Physical Printer Successfully printed");
                        success = true;
                    }

                    _physicalPrinter.EndDocument();
                }
            }

            if (success)
            {
                // save a backup of the printed document as pdf
                if (File.Exists(data.Filename))
                {
                    _logger.LogInformation("Saving backup of printed document as PDF to file {targetFilename}", targetFilename);
                    diagram.PrintToFile(data.Filename, targetFilename);
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to print document");
            success = false;
        }
        finally
        {
            mutex.ReleaseMutex();
            printable.ReleaseDiagram();
        }

        return success;
    }
}