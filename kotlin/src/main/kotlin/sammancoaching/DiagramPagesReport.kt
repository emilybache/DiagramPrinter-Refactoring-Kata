package sammancoaching

class DiagramPagesReport {
    private val pages: MutableList<DiagramReportPage> = mutableListOf()

    fun addPage(page: DiagramReportPage) {
        pages.add(page)
    }

    fun isComplete(): Boolean {
        return pages.isNotEmpty()
    }
}