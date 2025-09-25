package sammancoaching

import org.approvaltests.Approvals
import org.approvaltests.core.Options
import org.approvaltests.scrubbers.RegExScrubber
import org.junit.jupiter.api.Assertions
import org.junit.jupiter.api.Test
import java.io.IOException
import java.net.URI
import java.nio.file.InvalidPathException
import java.nio.file.Paths

class DiagramPrinterTest {
    @Test
    fun translatingEmptyDocumentFails() {
        val printer = DiagramPrinter()
        val output = StringBuilder()
        val result = printer.printSummary(null, "swedish", output)
        Assertions.assertFalse(result)
    }

    @Test
    @Throws(IOException::class)
    fun printingEmptyDocumentFails() {
        val printer = DiagramPrinter()
        val diagram: FlowchartDiagram? = null
        val result = printer.printDiagram(diagram, null, null)
        Assertions.assertFalse(result)
    }


    @Test
    @Throws(IOException::class)
    fun printingPdfDocumentSucceeds() {
        val spy = StringBuilder()
        val printer = DiagramPrinter()
        val diagram = SpyPrintableDiagram(spy, "filename", DiagramPrinter.PDF, true)

        val result = printer.printDiagram(diagram, "temp", "output.pdf")

        Assertions.assertTrue(result)
        Assertions.assertEquals(
            "Print to File (Filename=filename, Output folder=temp/output.pdf)",
            spy.toString().trim { it <= ' ' })
    }

    @Test
    @Throws(IOException::class)
    fun printingSpreadsheetDocumentSucceeds() {
        val spy = StringBuilder()
        val printer = DiagramPrinter()
        val diagram = SpyPrintableDiagram(spy, "filename", DiagramPrinter.SPREADSHEET, true)
        val result = printer.printDiagram(diagram, "temp", "output.xls")

        Assertions.assertTrue(result)
        Assertions.assertEquals(
            "Print to File (Filename=filename, Output folder=temp/output.xls)",
            spy.toString().trim { it <= ' ' })
    }

    @Test
    @Throws(IOException::class)
    fun printingPhysicalDocumentSucceeds() {
        val spy = StringBuilder()
        spy.append("\n")
        val spyPrinter = SpyPhysicalPrinter(spy)
        val spyQueue = SpyQueue(spy, spyPrinter)
        val printer = DiagramPhysicalPrinter(spyPrinter, spyQueue)
        val printerDriverFactory = PrinterDriverFactoryTestAdapter(spy)
        PrinterDriverFactory.setInstance(printerDriverFactory)

        val diagramWrapper = SpyPrintableDiagram(spy, "filename", "PDF", true)
        val info: DiagramMetadata = DiagramMetadataTestAdapter("filename", "Physical", true)
        val filename = "random output filename"

        val result = printer.doPrint(diagramWrapper, info, filename)

        Assertions.assertTrue(result)
        Approvals.verify(spy,
            Options(RegExScrubber("filename(\\d+).Physical", "[temp filename]"))
        )
    }

    @Test
    fun printReport_EmptyDocument_Fails() {
        val printer = DiagramPrinter()
        val template = "Report for FlowchartDiagram %s %s %s"
        val fakeFlowchartReportItems = FakeFlowchartReportItems("DiagramName", "Serial Number", "Filename")
        val result = printer.printReport(null, template, null, null, false)
        Assertions.assertFalse(result)
    }

    @Test
    fun printPages_EmptyDocument_Fails() {
        val printer = DiagramPrinter()
        val result = printer.printPages(null, PagesBuilder())
        Assertions.assertFalse(result)
    }

    @Test
    fun validateReport_MatchingTemplate_Succeeds() {
        val printer = DiagramPrinter()
        val template = "Report for FlowchartDiagram %s %s %s"
        val fakeFlowchartReportItems = FakeFlowchartReportItems("DiagramName", "Serial Number", "Filename")

        val result = printer.validateReport(template, fakeFlowchartReportItems)

        Assertions.assertTrue(result)
    }
}

internal class SpyPhysicalPrinter(private val spy: StringBuilder) : PhysicalPrinter() {
    init {
        this.isAvailable = true
        tonerLevels.put(Toner.Black, 100)
        tonerLevels.put(Toner.Cyan, 100)
        tonerLevels.put(Toner.Magenta, 100)
        tonerLevels.put(Toner.Yellow, 100)
    }

    override fun startDocument(isSummary: Boolean, isPdf: Boolean, name: String): Boolean {
        spy.append("Start Document\n")
        return true
    }

    public override fun endDocument() {
        spy.append("End Document\n")
    }
}

internal class SpyQueue(private val spy: StringBuilder, physicalPrinter: PhysicalPrinter) :
    PrintQueue(physicalPrinter) {
    public override fun add(data: PrintMetadata) {
        super.add(data)
        spy.append("Added metadata to print queue\n")
    }
}

internal class SpyPrintableDiagram (
    private val spy: StringBuilder,
    private val fullFilename: String,
    private val fileType: String,
    private val fileAvailable: Boolean
) : PrintableDiagram(FlowchartDiagram()) {
    public override fun getDiagramMetadata(): DiagramMetadata {
        return DiagramMetadataTestAdapter(fullFilename, fileType, fileAvailable)
    }

    public override fun getSummaryInformation(): String {
        return "summary information"
    }

    override fun printToFile(fromFilename: String, targetFilename: String): Boolean {
        spy.append(
            String.format(
                "Print to File (Filename=%s, Output folder=%s)\n",
                fromFilename, targetFilename
            )
        )
        return true
    }

    override fun printSpreadsheetToFile(fromFilename: String, targetFilename: String): Boolean {
        return printToFile(fromFilename, targetFilename)

    }

    companion object {
        fun isWellFormedUriString(targetFilename: String): Boolean {
            try {
                URI(targetFilename)
                return true
            } catch (e: Exception) {
                return false
            }
        }
    }
}

internal class DiagramMetadataTestAdapter(filename: String, fileType: String, available: Boolean) : DiagramMetadata() {
    init {
        this.fullFilename = filename
        this.fileType = fileType
        this.fileAvailable = available
    }
}

internal class PrinterDriverFactoryTestAdapter(private val spy: StringBuilder) : PrinterDriverFactory() {
    public override fun createDriverForPrint(): DiagramPrintDriver {
        return SpyDiagramPrintDriver(spy)
    }
}

internal class SpyDiagramPrintDriver(private val spy: StringBuilder) : DiagramPrintDriver() {
    override fun printTo(physicalPrinter: PhysicalPrinter): Boolean {
        spy.append("DiagramPrintDriver is printing to physical printer\n")
        return true
    }
}