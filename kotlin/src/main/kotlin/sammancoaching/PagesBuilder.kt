package sammancoaching

class PagesBuilder {
    fun processPage(page: DiagramPage, data: FlowchartReportItems): DiagramReportPage {
        page.addDataFrom(data)
        return DiagramReportPage(page)
    }

    fun apply(report: DiagramPagesReport, reportPages: List<DiagramReportPage>): Boolean {
        if (report.isComplete()) {
            return false
        }

        for (page in reportPages) {
            report.addPage(page)
        }

        return true
    }
}