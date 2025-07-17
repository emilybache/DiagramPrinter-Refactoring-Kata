from reporting import FlowchartReportItems, DiagramPage


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


#  This is the production code that is big and complex and hard to get into a unit test harness.
#  Imagine it has far more methods than this.
class FlowchartDiagram:
    def name(self):
        raise NotImplementedError("Can't construct this in a unit test")

    def serial_number(self):
        raise NotImplementedError("Can't construct this in a unit test")

    def is_disposed(self):
        raise NotImplementedError("Can't construct this in a unit test")

    def flowchart_as_pdf(self):
        raise NotImplementedError("Can't construct this in a unit test")

    def flowchart_data_as_spreadsheet(self):
        raise NotImplementedError("Can't construct this in a unit test")

    def summary_information(self):
        raise NotImplementedError("Can't construct this in a unit test")

    def flowchart_thumbnail(self):
        raise NotImplementedError("Can't construct this in a unit test")

    def report(self):
        raise RuntimeError("Can't call this from a unit test")

    def summary(self):
        raise RuntimeError("Can't call this from a unit test")

    def validation_problems(self, report_template: str, data: list[str]):
        raise RuntimeError("Can't call this from a unit test")

    def report_data(self):
        return FlowchartReportItems(
            self.name(),
            self.serial_number(),
            self.flowchart_thumbnail().filename
        )

    def pages_data(self) -> list['DiagramPage']:
        # fake implementation
        return [
            DiagramPage(self.name(), "page 1"),
            DiagramPage(self.name(), "page 2")
        ]
    # imagine about 200 more methods defined here


# This is the real production code class.
#  The implementation is omitted here, imagine it is from a third party library
#  which you can't change.
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


# This is the real production code class.
#  The implementation is omitted here, imagine it is from a third party library
#  which you can't change.
class SpreadsheetDocument:
    def __init__(self):
        raise NotImplementedError("Can't construct this in a unit test")

    def copy_file(self, info_full_filename, target_filename, overwrite):
        raise NotImplementedError("Can't call this from a unit test")


class DiagramMetadata:
    def __init__(self, idiagram):
        self.full_filename = f"{idiagram.name()}_{idiagram.serial_number()}"
        self.file_type = "PDF" if "Flowchart" in idiagram.name() else "Spreadsheet"
        self.file_available = not idiagram.is_disposed()
