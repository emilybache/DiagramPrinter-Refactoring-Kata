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
    
    public bool PrintReport(FlowchartDiagram diagram, string reportTemplate, string? folder = null,
        string? filename = null, bool summarize = true)
    {
        FlowchartReport iRep = diagram.Report();
        var targetFilename = GetTargetFilename(folder, filename);
        _logger.LogInformation(message: "Creating report for {name} to file {targetFilename}", diagram.Name(), targetFilename);

        if (summarize)
        {
            diagram = diagram.Summary();
            iRep.Close();
            iRep = diagram.Report();
            iRep.Open(true);
            _logger.LogInformation(message: "Switched to summary report for {name}", diagram.Name());
        }

        if (!iRep.isOpen())
        {
            _logger.LogError("Failed to open report for writing.");
            return false;
        }

        var data = new List<string>
        {
            diagram.Name(),
            diagram.SerialNumber(),
            diagram.FlowchartThumbnail().Filename()
        };
        
        if (!ValidateReport(reportTemplate, data))
        {
            _logger.LogError("Failed to validate report template.");
            FlowchartReportItems errors = diagram.ValidationProblems(reportTemplate, data);
            foreach (var err in errors.AllErrors())
            {
                _logger.LogInformation(message: "error: {err}", err);
            }
            return false;
        }
        if (summarize)
        {
            data.Add(diagram.SummaryInformation());
            iRep.OpenWithContents(reportTemplate, data, true);
        }
        else
        {
            iRep.OpenWithContents(reportTemplate, data, false);
        }

        iRep.SaveToFile(targetFilename);
        _logger.LogInformation("Report creation succeeded");
        return true;
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

    private bool ValidateReport(string template, List<string> substitutions)
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