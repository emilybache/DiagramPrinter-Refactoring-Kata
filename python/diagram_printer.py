import os
import logging

from documents import DiagramSummary, FlowchartDiagram, PrintableDiagram
from physical_printer import DiagramPhysicalPrinter


# This is a class you'd like to get under test so you can change it safely.
class DiagramPrinter:
    SPREADSHEET = "Spreadsheet"
    PDF = "PDF"

    def __init__(self):
        self._logger = logging.getLogger("DiagramPrinter")

    def print_summary(self, diagram, language):
        if diagram is None:
            return False, ""

        summary = DiagramSummary(language)
        summary.add_title(diagram.name(), diagram.serial_number())
        summary.add_header(diagram.summary_information())
        summary.add_image(diagram.flowchart_thumbnail())
        summary_text = summary.export()
        return True, summary_text

    def print_diagram(self, diagram: FlowchartDiagram, folder: str = None, filename: str = None):
        if diagram is None:
            return False

        printable_diagram = PrintableDiagram(diagram)
        return self._print_diagram(printable_diagram, folder, filename)

    def _print_diagram(self, printable_diagram: PrintableDiagram, folder, filename):
        info = printable_diagram.get_diagram_metadata()
        target_filename = self._get_target_filename(folder, filename)

        if info.file_type == self.PDF:
            self._logger.info(f"Printing PDF to file {target_filename}")
            return printable_diagram.print_to_file(info.full_filename, target_filename)

        if info.file_type == self.SPREADSHEET:
            if not target_filename.endswith(".xls"):
                target_filename += ".xls"
            self._logger.info(f"Printing Excel to file {target_filename}")
            return printable_diagram.print_to_file(info.full_filename, target_filename)

        # default case - print to a physical printer
        diagram_physical_printer = DiagramPhysicalPrinter()
        return diagram_physical_printer.do_print(printable_diagram, info, target_filename)

    @staticmethod
    def _get_target_filename(folder, filename: str = None):
        folder = folder or os.path.expanduser("~")
        filename = filename or "tempfile.tmp"
        return os.path.join(folder, filename)


