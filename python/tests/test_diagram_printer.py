import pytest

from diagram_printer import DiagramPrinter
from fakes import FakeFlowchartReportItems
from physical_printer import DiagramPhysicalPrinter
from printing import PrinterDriverFactory
from reporting import PagesBuilder


def test_translating_empty_document_fails():
    printer = DiagramPrinter()
    result, output = printer.print_summary(None, "swedish")
    assert not result


def test_printing_empty_document_fails():
    printer = DiagramPrinter()
    result = printer.print_diagram(None)
    assert not result


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