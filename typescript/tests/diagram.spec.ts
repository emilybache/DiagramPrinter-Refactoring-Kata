import {DiagramPrinter} from "../src/diagram_printer";
import {PagesBuilder} from "../src/reporting";
import {FakeFlowchartReportItems} from "./fakes";
import {
  SpyPhysicalPrinter,
  SpyPrintableDiagram,
  SpyQueue,
  StubDiagramMetadata,
  StubPrinterDriverFactory
} from './spies';
import {DiagramPhysicalPrinter} from "../src/physical_printer";
import {PrinterDriverFactory} from "../src/printing";


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

  test('printing PDF document succeeds', async () => {
    const spy: string[] = [];
    const printer = new DiagramPrinter();
    const diagram = new SpyPrintableDiagram(spy, "filename", DiagramPrinter.PDF, true);

    const result = await printer.printDiagramInternal(diagram, "temp", "output.pdf");

    expect(result).toBe(true);
    expect(spy[0]).toBe("Print to File (Filename=filename, folderValid=true)");
  });

  test('printing spreadsheet document succeeds', async () => {
    const spy: string[] = [];
    const printer = new DiagramPrinter();
    const diagram = new SpyPrintableDiagram(spy, "filename", DiagramPrinter.SPREADSHEET, true);

    const result = await printer.printDiagramInternal(diagram, "temp", "output.xls");

    expect(result).toBe(true);
    expect(spy[0]).toBe("Print to File (Filename=filename, folderValid=true)");
  });

  test('printing physical document succeeds', async () => {
    const spy: string[] = [];
    const spyPrinter = new SpyPhysicalPrinter(spy);
    const spyQueue = new SpyQueue(spy, spyPrinter);
    const printer = new DiagramPhysicalPrinter(spyPrinter, spyQueue);

    const printerDriverFactory = new StubPrinterDriverFactory(spy);
    PrinterDriverFactory.setInstance(printerDriverFactory);

    const diagramWrapper = new SpyPrintableDiagram(spy, "filename", "Physical", true);
    const info = new StubDiagramMetadata("filename", "Physical", true);
    const filename = "random output filename";

    const result = await printer.doPrint(diagramWrapper, info, filename);

    expect(result).toBe(true);
    const expectedOutput = [
      "Added metadata to print queue",
      "Start Document",
      "DiagramPrintDriver is printing to physical printer",
      "End Document"
    ];
    expect(spy).toEqual(expectedOutput);
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

  test('validateReport succeeds with matching template', () => {
    const template = 'Report for FlowchartDiagram %s %s %s';
    const fakeFlowchartReportItems = new FakeFlowchartReportItems(
      'DiagramName',
      'Serial Number',
      'Filename'
    );

    const result = printer['validateReport'](template, fakeFlowchartReportItems);

    expect(result).toBe(true);
  });

});
