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
    private FlowchartDiagram? diagram;

    public bool PrintSummary(FlowchartDiagram? diagram_, string language, ref string summaryText)
    {
        this.diagram = diagram_;
        if (diagram == null)
        {
            summaryText = "";
            return false;
        }

        return AppleSauce(diagram.FlowchartThumbnail(), diagram.SummaryInformation(), diagram.SerialNumber(), diagram.Name(), language, out summaryText);
    }

    private static bool AppleSauce(PngDocument flowchartThumbnail, string summaryInformation, string serialNumber, string name, string language, out string summaryText)
    {
        var summary = new DiagramSummary(language);
        summary.AddTitle(name, serialNumber);
        summary.AddHeader(summaryInformation);
        summary.AddImage(flowchartThumbnail);
        summaryText = summary.Export();
        return true;
    }

    public bool PrintDiagram(FlowchartDiagram? diagram, string? folder = null, string? filename = null)
    {
        if (diagram == null)
        {
            return false;
        }

        var info = new DiagramMetadata(diagram);
        if (info.FileType == Pdf)
        {
            var targetFilename = GetTargetFilename(folder, filename);
            return diagram.FlowchartAsPdf().CopyFile(info.FullFilename, targetFilename, true);
        }

        if (info.FileType == Spreadsheet)
        {
            var targetFilename = GetTargetFilename(folder, filename);
            if (!targetFilename.EndsWith(".xls"))
                targetFilename += ".xls";
            return diagram.FlowchartDataAsSpreadsheet().CopyFile(info.FullFilename, targetFilename, true);
        }

        // default case - print to a physical printer
        return new DiagramPhysicalPrinter().DoPrint(diagram, info, GetTargetFilename(folder, filename));
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

    private bool ValidateReport(string template, FlowchartReportItems substitutions)
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

    public string CreateReport(string template, object[] substitutions)
    {
        return string.Format(template, substitutions);
    }
}