from printing import DiagramMetadata


class DiagramSummary:
    def __init__(self, language):
        self._language = language
        self.contents = ""

    def add_header(self, summary_information):
        self.contents += summary_information + "\n"

    def add_title(self, name, serial_number):
        self.contents += name + "\n" + serial_number + "\n"

    def add_image(self, flowchart):
        self.contents += flowchart.filename()

    def export(self):
        # imagine a lot more detail and complexity here
        return self.contents

class IDiagram:
    def name(self):
        raise NotImplementedError

    def serial_number(self):
        raise NotImplementedError

    def is_disposed(self):
        raise NotImplementedError

    def flowchart_as_pdf(self):
        raise NotImplementedError

    def flowchart_data_as_spreadsheet(self):
        raise NotImplementedError

    def summary_information(self):
        raise NotImplementedError

    def flowchart_thumbnail(self):
        raise NotImplementedError

    # imagine about 200 more methods defined here

class PdfDocument:
    def __init__(self):
        raise NotImplementedError("Can't construct this in a unit test")

    def copy_file(self, from_path, target_path, fail_if_already_exists):
        raise NotImplementedError("Can't call this from a unit test")

class PngDocument:
    def __init__(self, filename):
        self._filename = filename

    def filename(self):
        return self._filename

class PrintableDiagram:
    def __init__(self, diagram):
        self.diagram = diagram

    def get_diagram_metadata(self):
        return DiagramMetadata(self.diagram)

    def print_to_file(self, from_filename, target_filename):
        return self.diagram.flowchart_as_pdf().copy_file(from_filename, target_filename, True)

    def summary_information(self):
        return self.diagram.summary_information()

class SpreadsheetDocument:
    def __init__(self):
        raise NotImplementedError("Can't construct this in a unit test")

    def copy_file(self, info_full_filename, target_filename, overwrite):
        raise NotImplementedError("Can't call this from a unit test")