from documents import PrintableDiagram
from printing import PhysicalPrinter, PrintQueue, DiagramMetadata, PrinterDriverFactory, DiagramPrintDriver


class SpyPhysicalPrinter(PhysicalPrinter):
    def __init__(self, spy):
        super().__init__()
        self._spy = spy
        self._is_available = True

    def start_document(self, is_summary, is_pdf, name):
        self._spy.append("Start Document")
        return True

    def end_document(self):
        self._spy.append("End Document")


class SpyQueue(PrintQueue):
    def __init__(self, spy, physical_printer):
        super().__init__(physical_printer)
        self._spy = spy

    def add(self, data):
        super().add(data)
        self._spy.append("Added metadata to print queue")


class SpyPrintableDiagram(PrintableDiagram):
    def __init__(self, spy, full_filename, file_type, file_available):
        super().__init__(None)
        self._spy = spy
        self._full_filename = full_filename
        self._file_type = file_type
        self._file_available = file_available

    def get_diagram_metadata(self):
        return StubDiagramMetadata(self._full_filename, self._file_type, self._file_available)

    def summary_information(self):
        return "summary information"

    def print_to_file(self, to_filename, target_filename):
        folder_ok = bool(target_filename)
        self._spy.append(f"Print to File (Filename={to_filename}, folderValid={folder_ok})")
        return True


class StubDiagramMetadata:
    def __init__(self, filename, filetype, available):
        self.full_filename = filename
        self.file_type = filetype
        self.file_available = available


class StubPrinterDriverFactory:
    def __init__(self, spy):
        self._spy = spy

    def create_driver_for_print(self):
        return SpyDiagramPrintDriver(self._spy)


class SpyDiagramPrintDriver(DiagramPrintDriver):
    def __init__(self, spy):
        super().__init__()
        self._spy = spy

    def print_to(self, physical_printer):
        self._spy.append("DiagramPrintDriver is printing to physical printer")
        return True
