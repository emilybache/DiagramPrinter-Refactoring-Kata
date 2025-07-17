import {DiagramPrinter} from "../src/diagram_printer";
import {PagesBuilder} from "../src/reporting";
import {FakeFlowchartReportItems} from "./fakes";

describe('DiagramPrinter', () => {
  let printer: DiagramPrinter;

  beforeEach(() => {
    printer = new DiagramPrinter();
  });

  test('translating empty document fails', () => {
    const [result, _] = printer.printSummary(null, 'swedish');
    expect(result).toBe(false);
  });

  test('printing empty document fails', async () => {
    const result = await printer.printDiagram(null, "", "");
    expect(result).toBe(false);
  });

  test('print report with empty document fails', () => {
    const template = 'Report for FlowchartDiagram %s %s %s';
    const fakeReportItems = new FakeFlowchartReportItems("DiagramName", "Serial Number", "Filename");
    const result = printer.printReport(null, template, "", "", false);
    expect(result).toBe(false);
  });

  test('print pages with empty document fails', () => {
    const result = printer.printPages(null, new PagesBuilder());
    expect(result).toBe(false);
  });
});
