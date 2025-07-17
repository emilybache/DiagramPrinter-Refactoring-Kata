import os
import logging

from documents import DiagramSummary, FlowchartDiagram, DiagramMetadata
from physical_printer import DiagramPhysicalPrinter
from reporting import DiagramPagesReport, FlowchartReportItems, PagesBuilder


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

        info = DiagramMetadata(diagram)
        target_filename = self._get_target_filename(folder, filename)

        if info.file_type == self.PDF:
            self._logger.info(f"Printing PDF to file {target_filename}")
            return diagram.flowchart_as_pdf().copy_file(info.full_filename, target_filename, True)

        if info.file_type == self.SPREADSHEET:
            if not target_filename.endswith(".xls"):
                target_filename += ".xls"
            self._logger.info(f"Printing Excel to file {target_filename}")
            return diagram.flowchart_data_as_spreadsheet().copy_file(info.full_filename, target_filename, True)

        # default case - print to a physical printer
        diagram_physical_printer = DiagramPhysicalPrinter()
        return diagram_physical_printer.do_print(diagram, info, target_filename)

    def print_report(self, diagram: FlowchartDiagram, report_template: str, folder: str,
                     filename: str, summarize: bool) -> bool:
        if diagram is None:
            return False

        report = diagram.report()
        target_filename = self._get_target_filename(folder, filename)
        self._logger.info(f"Creating report for {diagram.name()} to file {target_filename}")

        if summarize:
            diagram = diagram.summary()
            report.close()
            report = diagram.report()
            report.open(True)
            self._logger.info(f"Switched to summary report for {diagram.name()}")

        if not report.is_open():
            self._logger.warning("Failed to open report for writing.")
            return False

        data = diagram.report_data()

        if not self._validate_report(report_template, data):
            self._logger.warning("Failed to validate report template.")
            return False

        if summarize:
            data.add(diagram.summary_information())
            report.open_with_contents(report_template, data, True)
        else:
            report.open_with_contents(report_template, data, False)

        report.save_to_file(target_filename)
        self._logger.info("Report creation succeeded")
        return True

    def print_pages(self, diagram: FlowchartDiagram, builder: 'PagesBuilder') -> bool:
        if diagram is None:
            return False

        data = diagram.report_data()
        pages = diagram.pages_data()

        report = DiagramPagesReport()
        report_pages = []

        for page in pages:
            processed_page = builder.process_page(page, data)
            report_pages.append(processed_page)

        return builder.apply(report, report_pages)

    @staticmethod
    def _get_target_filename(folder, filename):
        folder = folder or os.path.expanduser("~")
        filename = filename or "tempfile.tmp"
        return os.path.join(folder, filename)

    def _validate_report(self, template: str, substitutions: 'FlowchartReportItems') -> bool:
        try:
            self.create_report(template, substitutions.to_array())
            return True
        except ValueError as e:
            self._logger.warning("Report template did not match substitutions")
            return False

    def create_report(self, template: str, substitutions: list) -> str:
        return template.format(*substitutions)
