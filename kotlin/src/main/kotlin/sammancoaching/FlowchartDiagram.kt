package sammancoaching

class FlowchartDiagram {
    init {}

    val name: String
        get() {
            throw RuntimeException("Can't call this from a unit test")
        }

    val serialNumber: String
        get() {
            throw RuntimeException("Can't call this from a unit test")
        }

    val isDisposed: Boolean
        get() {
            throw RuntimeException("Can't call this from a unit test")
        }

    val flowchartAsPdf: PdfDocument
        get() {
            throw RuntimeException("Can't call this from a unit test")
        }

    val flowchartDataAsSpreadsheet: SpreadsheetDocument
        get() {
            throw RuntimeException("Can't call this from a unit test")
        }

    val summaryInformation: String
        get() {
            throw RuntimeException("Can't call this from a unit test")
        }

    val flowchartThumbnail: PngDocument
        get() {
            throw RuntimeException("Can't call this from a unit test")
        }

    fun report(): FlowchartReport {
        throw RuntimeException("Can't call this from a unit test")
    }

    fun summary(): FlowchartDiagram {
        throw RuntimeException("Can't call this from a unit test")
    }

    fun ValidationProblems(reportTemplate: String?, data: MutableList<String?>?): FlowchartReportItems? {
        throw RuntimeException("Can't call this from a unit test")
    }

    fun reportData(): FlowchartReportItems {
        return FlowchartReportItems(this.name, this.serialNumber, this.flowchartThumbnail.filename)
    }

    fun PagesData(): MutableList<DiagramPage> {
        // fake implementation
        val list = ArrayList<DiagramPage>()
        list.add(DiagramPage(this.name, "page 1"))
        list.add(DiagramPage(this.name, "page 2"))
        return list
    }
}
