namespace DiagramPrinter;

/**
 * This interface is implemented by the concrete class FlowchartDiagram.
 * Imagine it has far more methods than this.
 */
public interface IDiagram : IDiagramBase
{
    string Name();
    string SerialNumber();
    bool IsDisposed();
    PdfDocument FlowchartAsPdf();
    SpreadsheetDocument FlowchartDataAsSpreadsheet();
    string SummaryInformation();
    PngDocument FlowchartThumbnail();
    
    // imagine about 200 more methods defined here
}