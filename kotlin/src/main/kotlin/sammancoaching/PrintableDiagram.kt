package sammancoaching

open class PrintableDiagram(private val diagram: FlowchartDiagram) {

    fun getDiagram(): FlowchartDiagram {
        return diagram
    }

    open fun getDiagramMetadata(): DiagramMetadata {
        return DiagramMetadata(diagram)
    }

    open fun printToFile(fromFilename: String, targetFilename: String): Boolean {
        return diagram.flowchartAsPdf.copyFile(fromFilename, targetFilename, true) == true;
    }

    open fun printSpreadsheetToFile(fromFilename: String, targetFilename: String): Boolean {
        return diagram.flowchartDataAsSpreadsheet.copyFile(fromFilename, targetFilename, true) == true
    }

    open fun getSummaryInformation(): String {
        return diagram.summaryInformation.toString()
    }
}