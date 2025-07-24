import pytest

from diagram_printer import DiagramPrinter
from tests.fakes import FakeFlowchartReportItems
from physical_printer import DiagramPhysicalPrinter
from printing import PrinterDriverFactory
from reporting import PagesBuilder
from tests.spies import SpyPhysicalPrinter, SpyQueue, SpyPrintableDiagram, StubDiagramMetadata, \
    StubPrinterDriverFactory


@pytest.fixture
def spy():
    return []

def test_translating_empty_document_fails():
    printer = DiagramPrinter()
    result, output = printer.print_summary(None, "swedish")
    assert not result


def test_printing_empty_document_fails():
    printer = DiagramPrinter()
    result = printer.print_diagram(None)
    assert not result

def test_printing_pdf_document_succeeds(spy):
    printer = DiagramPrinter()
    diagram = SpyPrintableDiagram(spy, "filename", DiagramPrinter.PDF, True)

    result = printer._print_diagram(diagram, "temp", "output.pdf")

    assert result
    assert spy[0].strip() == "Print to File (Filename=filename, folderValid=True)"

def test_printing_spreadsheet_document_succeeds(spy):
    printer = DiagramPrinter()
    diagram = SpyPrintableDiagram(spy, "filename", DiagramPrinter.SPREADSHEET, True)

    result = printer._print_diagram(diagram, "temp", "output.xls")

    assert result
    assert spy[0].strip() == "Print to File (Filename=filename, folderValid=True)"

def test_printing_physical_document_succeeds(spy):
    spy_printer = SpyPhysicalPrinter(spy)
    spy_queue = SpyQueue(spy, spy_printer)
    printer = DiagramPhysicalPrinter(spy_printer, spy_queue)
    printer_driver_factory = StubPrinterDriverFactory(spy)
    PrinterDriverFactory._instance = printer_driver_factory
    diagram_wrapper = SpyPrintableDiagram(spy, "filename", "PDF", True)
    info = StubDiagramMetadata("filename", "Physical", True)
    filename = "random_output_filename"

    result = printer.do_print(diagram_wrapper, info, filename)

    assert result
    expected_output = ["Added metadata to print queue",
                       "Start Document",
                       "DiagramPrintDriver is printing to physical printer",
                       "End Document"]
    assert spy == expected_output


def test_print_report_empty_document_fails():
    printer = DiagramPrinter()
    template = "Report for FlowchartDiagram %s %s %s"
    fake_flowchart_report_items = FakeFlowchartReportItems("DiagramName", "Serial Number", "Filename")
    result = printer.print_report(None, template, None, None, False)
    assert not result


def test_print_pages_empty_document_fails():
    printer = DiagramPrinter()
    result = printer.print_pages(None, PagesBuilder())
    assert not result


def test_validate_report_matching_template_succeeds():
    printer = DiagramPrinter()
    template = "Report for FlowchartDiagram %s %s %s"
    fake_flowchart_report_items = FakeFlowchartReportItems("DiagramName", "Serial Number", "Filename")

    result = printer.validate_report(template, fake_flowchart_report_items)

    assert result