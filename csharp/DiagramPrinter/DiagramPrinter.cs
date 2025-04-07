namespace DiagramPrinter;

/**
 * This is the class you'd like to get under test so you can change it safely.
 */
public class DiagramPrinter
{
    public bool PrintSummary(IDiagram? diagram, string language, ref string summaryText)
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

    public bool PrintDiagram(IDiagram? diagram, string? folder = null, string? filename = null)
    {
        if (diagram == null)
        {
            return false;
        }

        var info = new DiagramMetadata(diagram);
        if (info.FileType == "PDF")
        {
            var targetFilename = GetTargetFilename(folder, filename);
            return diagram.FlowchartAsPdf().CopyFile(info.FullFilename, targetFilename, true);
        }
        
        if (info.FileType == "Spreadsheet")
        {
            var targetFilename = GetTargetFilename(folder, filename);
            if (!targetFilename.EndsWith(".xls"))
                targetFilename += ".xls";
            return diagram.FlowchartDataAsSpreadsheet().CopyFile(info.FullFilename, targetFilename, true);
        }
        // default case - print to a physical printer
        return new DiagramPhysicalPrinter().DoPrint(diagram, info, GetTargetFilename(folder, filename));
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
}