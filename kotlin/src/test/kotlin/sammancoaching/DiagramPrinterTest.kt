package sammancoaching

import org.junit.jupiter.api.Assertions
import org.junit.jupiter.api.Test
import java.io.IOException

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


