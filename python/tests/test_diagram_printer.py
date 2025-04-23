import pytest

from diagram_printer import DiagramPrinter


def test_translating_empty_document_fails():
    printer = DiagramPrinter()
    result, output = printer.print_summary(None, "swedish")
    assert not result


def test_printing_empty_document_fails():
    printer = DiagramPrinter()
    result = printer.print_diagram(None)
    assert not result
