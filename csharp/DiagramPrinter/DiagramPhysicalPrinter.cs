namespace DiagramPrinter;

using Microsoft.Extensions.Logging;

/**
 * This is a class you'd like to get under test so you can change it safely.
 */
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

    public bool DoPrint(IDiagram diagram, DiagramMetadata info, string targetFilename)
    {
        var printerDriver = PrinterDriverFactory.Instance.CreateDriverForPrint();
        printerDriver.SetDiagram(diagram);

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
                _logger.LogInformation("Physical Printer Unavailable Due to Job Count Inconsistency");
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
                    if (printerDriver.PrintTo(_physicalPrinter))
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
                    _logger.LogInformation("Saving backup of printed document as PDF to file {targetFilename}",
                        targetFilename);
                    diagram.FlowchartAsPdf().CopyFile(data.Filename, targetFilename, true);
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
            printerDriver.ReleaseDiagram();
        }

        return success;
    }
}