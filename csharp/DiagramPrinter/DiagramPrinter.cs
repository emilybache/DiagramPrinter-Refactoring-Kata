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

        var diagramWrapper = new DiagramWrapper(diagram);

        return PrintDiagram2(diagramWrapper, folder, filename);
    }

    public bool PrintDiagram2(DiagramWrapper diagramWrapper, string? folder = null, string? filename = null)
    {
        var info = diagramWrapper.GetDiagramMetadata();
        if (info.FileType == "PDF")
        {
            var targetFilename = GetTargetFilename(folder, filename);
            var copySuccessful = diagramWrapper.PrintToFile(info.FullFilename, targetFilename);
            return copySuccessful;
        }

        if (info.FileType == "Spreadsheet")
        {
            var targetFilename = GetTargetFilename(folder, filename);
            if (!targetFilename.EndsWith(".xls"))
                targetFilename += ".xls";
            var copySuccessful = diagramWrapper.PrintToFile(info.FullFilename, targetFilename);
            return copySuccessful;
        }
        // default case - print to a physical printer
        return new DiagramPhysicalPrinter().DoPrint(diagramWrapper, info, GetTargetFilename(folder, filename));
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