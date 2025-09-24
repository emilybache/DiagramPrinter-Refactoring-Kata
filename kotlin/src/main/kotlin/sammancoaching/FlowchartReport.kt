package sammancoaching

class FlowchartReport {
    fun close() {
        throw UnsupportedOperationException()
    }

    fun open(show: Boolean) {
        throw UnsupportedOperationException()
    }

    fun isOpen(): Boolean {
        throw UnsupportedOperationException()
    }

    fun openWithContents(reportTemplate: String, substitutions: FlowchartReportItems, readOnly: Boolean) {
        throw UnsupportedOperationException()
    }

    fun saveToFile(targetFilename: String) {
        throw UnsupportedOperationException()
    }
}