import os

from documents import FlowchartDiagram


class PhysicalPrinter:
    def __init__(self):
        self._is_available = False
        self._job_count = 0

    @property
    def is_available(self):
        return self._is_available

    @property
    def job_count(self):
        return self._job_count

    def start_document(self, is_summary, is_pdf, name):
        raise NotImplementedError("Can't call this from a unit test")

    def end_document(self):
        raise NotImplementedError("Can't call this from a unit test")

class PrintQueue:
    def __init__(self, physical_printer : PhysicalPrinter):
        self._printer = physical_printer
        self._items = []

    def add(self, data):
        self._items.append(data)

class PrintMetadata:
    def __init__(self, info_file_type : str):
        self._info_file_type = info_file_type
        self.filename = self._generate_temp_filename(info_file_type)

    def _generate_temp_filename(self, info_file_type) -> str:
        temp_filename = os.path.join(os.path.dirname(os.path.abspath(__file__)), "tempfile")
        return f"{temp_filename}.{info_file_type}"

class DiagramPrintDriver:
    def __init__(self):
        self._diagram = None

    def set_diagram(self, diagram : FlowchartDiagram):
        self._diagram = diagram

    def release_diagram(self):
        self._diagram = None

    def print_to(self, physical_printer : PhysicalPrinter) -> bool:
        raise NotImplementedError("Can't call this from a unit test")

class PrinterDriverFactory:
    _instance = None

    @staticmethod
    def get_instance():
        if PrinterDriverFactory._instance is None:
            PrinterDriverFactory._instance = PrinterDriverFactory()
        return PrinterDriverFactory._instance

    # For unit tests
    @staticmethod
    def set_instance(instance):
        PrinterDriverFactory._instance = instance

    def create_driver_for_print(self) -> DiagramPrintDriver:
        return DiagramPrintDriver()

