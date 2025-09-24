package sammancoaching

class DiagramSummary(private val language: String?) {
    private var contents = ""

    fun addHeader(summaryInformation: String?) {
        contents += summaryInformation + "\n"
    }

    fun addTitle(name: String?, serialNumber: String?) {
        contents += name + "\n" + serialNumber + "\n"
    }

    fun addImage(flowchart: PngDocument) {
        contents += flowchart.filename
    }

    fun export(): String {
        // Imagine a lot more detail and complexity here
        return contents
    }
}
