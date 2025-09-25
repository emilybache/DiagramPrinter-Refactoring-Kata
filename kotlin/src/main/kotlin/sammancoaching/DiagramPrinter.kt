package sammancoaching

import java.io.IOException
import java.nio.file.Paths
import java.util.*
import java.util.logging.Logger


/**
 * This is a class you'd like to get under test so you can change it safely.
 */
class DiagramPrinter {
    fun printSummary(diagram: FlowchartDiagram?, language: String?, summaryText: StringBuilder): Boolean {
        if (diagram == null) {
            summaryText.setLength(0) // Clear text
            return false
        }

        val summary: DiagramSummary = DiagramSummary(language)
        summary.addTitle(diagram.name, diagram.serialNumber)
        summary.addHeader(diagram.summaryInformation)
        summary.addImage(diagram.flowchartThumbnail)
        summaryText.append(summary.export())
        return true
    }

    @Throws(IOException::class)
    fun printDiagram(diagram: FlowchartDiagram?, folder: String?, filename: String?): Boolean {
        if (diagram == null) {
            return false
        }

        val info = DiagramMetadata(diagram)
        if (PDF.equals(info.fileType)) {
            val targetFilename = getTargetFilename(folder, filename)
            return diagram.flowchartAsPdf.copyFile(info.fullFilename, targetFilename, true)
        }

        if (SPREADSHEET.equals(info.fileType)) {
            var targetFilename = getTargetFilename(folder, filename)
            if (!targetFilename.endsWith(".xls")) {
                targetFilename += ".xls"
            }
            return diagram.flowchartDataAsSpreadsheet.copyFile(info.fullFilename, targetFilename, true)
        }

        // Default case - print to a physical printer
        return DiagramPhysicalPrinter().doPrint(diagram, info, getTargetFilename(folder, filename))
    }


    fun printReport(
        diagram: FlowchartDiagram?, reportTemplate: String, folder: String?,
        filename: String?, summarize: Boolean
    ): Boolean {
        var diagram: FlowchartDiagram? = diagram
        if (diagram == null) {
            return false
        }

        var report: FlowchartReport = diagram.report()
        val targetFilename = getTargetFilename(folder, filename)
        _logger.info(
            String.format(
                "Creating report for %s to file %s",
                *arrayOf<Any?>(diagram.name, targetFilename)
            )
        )

        if (summarize) {
            diagram = diagram.summary()
            report.close()
            report = diagram.report()
            report.open(true)
            _logger.info("Switched to summary report for " + diagram.name)
        }

        if (!report.isOpen()) {
            _logger.warning("Failed to open report for writing.")
            return false
        }

        val data: FlowchartReportItems = diagram.reportData()

        if (!validateReport(reportTemplate, data)) {
            _logger.warning("Failed to validate report template.")
            return false
        }

        if (summarize) {
            data.add(diagram.summaryInformation)
            report.openWithContents(reportTemplate, data, true)
        } else {
            report.openWithContents(reportTemplate, data, false)
        }

        report.saveToFile(targetFilename)
        _logger.info("Report creation succeeded")
        return true
    }

    fun printPages(diagram: FlowchartDiagram?, builder: PagesBuilder): Boolean {
        if (diagram == null) {
            return false
        }

        val data: FlowchartReportItems = diagram.reportData()
        val pages: MutableList<DiagramPage> = diagram.PagesData()

        val report = DiagramPagesReport()
        val reportPages: MutableList<DiagramReportPage?> = ArrayList<DiagramReportPage?>()

        for (page in pages) {
            val processedPage = builder.processPage(page, data)
            reportPages.add(processedPage)
        }

        return builder.apply(report, Collections.unmodifiableList<DiagramReportPage>(reportPages))
    }

    fun validateReport(template: String, substitutions: FlowchartReportItems): Boolean {
        try {
            createReport(template, substitutions.toArray())
            return true
        } catch (e: IllegalFormatException) {
            _logger.warning("Report template did not match substitutions")
            return false
        }
    }

    private fun createReport(template: String, substitutions: Array<Any>): String {
        val report = String.format(template, *substitutions)
        return report
    }

    companion object {
        const val SPREADSHEET: String = "Spreadsheet"
        const val PDF: String = "PDF"
        private val _logger: Logger = Logger.getLogger(DiagramPrinter::class.java.getName())

        private fun getTargetFilename(folder: String?, filename: String?): String {
            var folder = folder
            var filename = filename
            if (folder == null) {
                folder = System.getProperty("java.io.tmpdir") // Equivalent to Path.GetTempPath()
            }

            if (filename == null) {
                filename = Paths.get(folder, "tempFile").toString() // Simulate temp file creation
            }

            return Paths.get(folder, filename).toString()
        }
    }


}

