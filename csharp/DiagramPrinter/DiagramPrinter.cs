using Microsoft.Extensions.Logging;

namespace DiagramPrinter;

public class FlowchartDiagramAdapter(FlowchartDiagram? diagram)
{
    public FlowchartDiagram? Diagram { get; } = diagram;

    public bool DiagramIsAvailable()
    {
        return Diagram != null;
    }
}

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
        return PrintSummary(new FlowchartDiagramAdapter(diagram), language, out summaryText);
    }

    /*
* Copy this checklist into the code and make a commit with message: [intention] - Discover Adapter Interface
* Identify every call to 'wrapper.Awkward'. Go through each one and decide which checklist to use. Either 'Discover Adapter Method' or 'Propagate Adapter'. Add the relevant checklist as a comment.
* Follow the new checklists for each call to 'wrapper.Awkward'. 
     */
    public static bool PrintSummary(FlowchartDiagramAdapter flowchartDiagramAdapter, string language, out string summaryText)
    {
        if (!flowchartDiagramAdapter.DiagramIsAvailable())
        {
            summaryText = "";
            return false;
        }

        // Use this checklist for code that uses wrapper.Awkward that you don't need to include in your unit tests.
        // * 'Extract method' for the code using 'wrapper.Awkward'. Preferably take the whole line of code into the new method. Make sure the wrapper class is one of the arguments.
        // * If the new method is not static, 'Introduce Parameter' for all the member variables it uses. Then make it static.
        // * 'Make method non static' and select the wrapper class as the instance to move it to.
        // * Delete unnecessary comments and make a commit [underway] - Discover Adapter Interface
        var summary = new DiagramSummary(language);
        summary.AddTitle(flowchartDiagramAdapter.Diagram.Name(), flowchartDiagramAdapter.Diagram.SerialNumber());
        summary.AddHeader(flowchartDiagramAdapter.Diagram.SummaryInformation());
        summary.AddImage(flowchartDiagramAdapter.Diagram.FlowchartThumbnail());
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
        _logger.LogInformation(message: "Creating report for {name} to file {targetFilename}", diagram.Name(),
            targetFilename);

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