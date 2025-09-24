package sammancoaching

import java.util.IllegalFormatException

class DiagramPage(val name: String, private var pageContents: String) {

    fun getPageContents(): String {
        return pageContents
    }

    fun addDataFrom(data: FlowchartReportItems) {
        try {
            if (pageContents.contains("{0}")) {
                pageContents = String.format(pageContents, *data.toArray())
            }
        } catch (e: IllegalFormatException) {
            // Handle exception silently as in original
        }
    }
}