import os
import threading
import logging

from documents import DiagramSummary, PrintableDiagram
from printing import *

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

    def print_diagram(self, diagram, folder=None, filename=None):
        if diagram is None:
            return False

        printable_diagram = PrintableDiagram(diagram)
        return self._print_diagram(printable_diagram, folder, filename)

    def _print_diagram(self, printable_diagram, folder, filename):
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

        diagram_physical_printer = DiagramPhysicalPrinter()
        return diagram_physical_printer.do_print(printable_diagram, info, target_filename)

    @staticmethod
    def _get_target_filename(folder, filename):
        folder = folder or os.path.expanduser("~")
        filename = filename or "tempfile.tmp"
        return os.path.join(folder, filename)

class DiagramPhysicalPrinter:
    def __init__(self, physical_printer=None, print_queue=None):
        self._physical_printer = physical_printer or PhysicalPrinter()
        self._print_queue = print_queue or PrintQueue(self._physical_printer)
        self._logger = logging.getLogger("DiagramPhysicalPrinter")

    def do_print(self, printable_diagram, info, target_filename):
        printer_driver = PrinterDriverFactory.get_instance().create_driver_for_print()
        printer_driver.set_diagram(printable_diagram.diagram)

        data = PrintMetadata(info.file_type)
        mutex = threading.Lock()
        success = False

        with mutex:
            if not self._physical_printer.is_available:
                self._logger.info("Physical Printer Unavailable")
            elif self._physical_printer.job_count < 0:
                self._logger.info("Physical Printer Unavailable Due to Job Count Inconsistency")
            else:
                self._print_queue.add(data)
                summary_information = printable_diagram.summary_information()
                self._logger.info(f"Diagram Summary Information {summary_information}")

                is_summary = len(summary_information) > 10
                if self._physical_printer.start_document(not is_summary, False, "DiagramPhysicalPrinter"):
                    if printer_driver.print_to(self._physical_printer):
                        self._logger.info("Physical Printer Successfully printed")
                        success = True
                    self._physical_printer.end_document()

                if success and os.path.exists(data.filename):
                    self._logger.info(f"Saving backup of printed document as PDF to file {target_filename}")
                    printable_diagram.print_to_file(data.filename, target_filename)

        printer_driver.release_diagram()
        return success
