class FlowchartReportItems:
    def __init__(self, name: str, serial_number: str, filename: str):
        # this implementation is deliberately blank - can't use this class in a unit test
        pass

    def add(self, summary_information: str) -> None:
        raise NotImplementedError("can't call from a unit test")

    def to_array(self) -> list:
        raise NotImplementedError("can't call from a unit test")


class DiagramPage:
    def __init__(self, name: str, page_contents: str):
        self._name = name
        self._page_contents = page_contents

    @property
    def name(self) -> str:
        return self._name

    @property
    def page_contents(self) -> str:
        return self._page_contents

    def add_data_from(self, data: FlowchartReportItems) -> None:
        try:
            if "{0}" in self._page_contents:
                self._page_contents = self._page_contents.format(*data.to_array())
        except ValueError:
            # Silently handle formatting errors
            pass


class PagesBuilder:
    def process_page(self, page: DiagramPage, data: FlowchartReportItems) -> 'DiagramReportPage':
        page.add_data_from(data)
        return DiagramReportPage(page)

    def apply(self, report: 'DiagramPagesReport', report_pages: list['DiagramReportPage']) -> bool:
        if report.is_complete():
            return False

        for page in report_pages:
            report.add_page(page)

        return True


class DiagramReportPage:
    def __init__(self, page):
        self.pages = [page]


class DiagramPagesReport:
    def __init__(self):
        self._pages: list['DiagramReportPage'] = []

    def add_page(self, page: 'DiagramReportPage') -> None:
        self._pages.append(page)

    def is_complete(self) -> bool:
        return len(self._pages) > 0
