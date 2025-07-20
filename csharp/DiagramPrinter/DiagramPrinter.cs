using Microsoft.Extensions.Logging;

namespace DiagramPrinter;

/**
 * This is a class you'd like to get under test so you can change it safely.
 */
public class DiagramPrinter
{
    public const string Spreadsheet = "Spreadsheet";
    public const string Pdf = "PDF";

    private readonly ILogger<DiagramPrinter> _logger = LoggingProvider.CreateLogger<DiagramPrinter>();
    public bool PrintSummary(FlowchartDiagram? diagram, string language, ref string summaryText)
    {
        if (diagram == null)
        {
            summaryText = "";
            return false;
        }

        var summary = new DiagramSummary(language);
        summary.AddTitle(diagram.Name(), diagram.SerialNumber());
        summary.AddHeader(diagram.SummaryInformation());
        summary.AddImage(diagram.FlowchartThumbnail());
        summaryText = summary.Export();
        return true;
    }

    public bool PrintDiagram(FlowchartDiagram? diagram, string? folder = null, string? filename = null)
    {
        if (diagram == null)
        {
            return false;
        }
        var printableDiagram = new PrintableDiagram(diagram);
        return PrintDiagram(printableDiagram, folder, filename);
    }

    public bool PrintDiagram(PrintableDiagram printableDiagram, string? folder, string? filename)
    {
        var info = printableDiagram.GetDiagramMetadata();
        if (info.FileType == Pdf)
        {
            var targetFilename = GetTargetFilename(folder, filename);
            _logger.LogInformation("Printing Pdf to file {targetFilename}", targetFilename);
            var copySuccessful = printableDiagram.PrintToFile(info.FullFilename, targetFilename);
            return copySuccessful;
        }

        if (info.FileType == Spreadsheet)
        {
            var targetFilename = GetTargetFilename(folder, filename);
            if (!targetFilename.EndsWith(".xls"))
                targetFilename += ".xls";
            _logger.LogInformation("Printing Excel to file {targetFilename}", targetFilename);
            var copySuccessful = printableDiagram.PrintToSpreadsheetFile(info.FullFilename, targetFilename);
            return copySuccessful;
        }
        // default case - print to a physical printer
        var diagramPhysicalPrinter = new DiagramPhysicalPrinter();
        return diagramPhysicalPrinter.DoPrint(printableDiagram, info, GetTargetFilename(folder, filename));
    }
    
    public bool PrintReport(FlowchartDiagram? diagram, string reportTemplate, string? folder = null,
        string? filename = null, bool summarize = true)
    {
        if (diagram == null)
        {
            return false;
        }
        
        FlowchartReport report = diagram.Report();
        var targetFilename = GetTargetFilename(folder, filename);
        _logger.LogInformation(message: "Creating report for {name} to file {targetFilename}", diagram.Name(), targetFilename);

        if (summarize)
        {
            diagram = diagram.Summary();
            report.Close();
            report = diagram.Report();
            report.Open(true);
            _logger.LogInformation(message: "Switched to summary report for {name}", diagram.Name());
        }

        if (!report.isOpen())
        {
            _logger.LogError("Failed to open report for writing.");
            return false;
        }

        var data = diagram.ReportData();
        
        if (!ValidateReport(reportTemplate, data))
        {
            _logger.LogError("Failed to validate report template.");
            return false;
        }
        if (summarize)
        {
            data.Add(diagram.SummaryInformation());
            report.OpenWithContents(reportTemplate, data, true);
        }
        else
        {
            report.OpenWithContents(reportTemplate, data, false);
        }

        report.SaveToFile(targetFilename);
        _logger.LogInformation("Report creation succeeded");
        return true;
    }

    public bool PrintPages(FlowchartDiagram? diagram, PagesBuilder builder)
    {
        if (diagram == null)
        {
            return false;
        }
        
        FlowchartReportItems data = diagram.ReportData();
        List<DiagramPage> pages = diagram.PagesData();

        var report = new DiagramPagesReport();
        var reportPages = new List<DiagramReportPage>();

        foreach (var page in pages)
        {
            DiagramReportPage processedPage = builder.ProcessPage(page, data);
            reportPages.Add(processedPage);
        }

        return builder.Apply(report, reportPages);
    }



    private static string GetTargetFilename(string? folder, string? filename)
    {
        if (folder == null)
        {
            folder = Path.GetTempPath();
        }

        if (filename == null)
        {
            filename = Path.GetTempFileName();
        }
        var targetFilename = Path.Join(folder, filename);
        return targetFilename;
    }
    
    
    public bool ValidateReport(string template, FlowchartReportItems substitutions)
    {
        try
        {
            CreateReport(template, substitutions.ToArray());
            return true;
        }
        catch (System.FormatException e)
        {
            _logger.LogError("Report template did not match substitutions");
            return false;
        }
    }

    private string CreateReport(string template, object[] substitutions)
    {
        var report = string.Format(template, substitutions);
        return report;
    }
}