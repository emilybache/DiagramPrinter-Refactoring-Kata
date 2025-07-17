from reporting import FlowchartReportItems


class FakeFlowchartReportItems(FlowchartReportItems):
    """
    Use this class in your unit tests to replace a real FlowchartReportItems class
    """
    def __init__(self, name: str, serial_number: str, filename: str):
        super().__init__(name, serial_number, filename)
        self._data: list[str] = [name, serial_number, filename]

    def add(self, summary_information: str) -> None:
        self._data.append(summary_information)

    def to_array(self) -> list:
        return self._data.copy()
