import * as path from 'path';
import * as os from 'os';
import { DiagramSummary, FlowchartDiagram, DiagramMetadata } from './documents';
import { DiagramPhysicalPrinter } from './physical_printer';
import {DiagramPagesReport, DiagramReportPage, FlowchartReportItems, PagesBuilder} from "./reporting";


class DiagramPrinter {
  private static readonly SPREADSHEET = "Spreadsheet";
  private static readonly PDF = "PDF";

  constructor() {
  }

  printSummary(diagram: FlowchartDiagram | null, language: string): [boolean, string] {
    if (diagram === null) {
      return [false, ""];
    }

    const summary = new DiagramSummary(language);
    summary.addTitle(diagram.name(), diagram.serialNumber());
    summary.addHeader(diagram.summaryInformation());
    summary.addImage(diagram.flowchartThumbnail());
    const summaryText = summary.export();
    return [true, summaryText];
  }

  async printDiagram(diagram: FlowchartDiagram | null, folder?: string, filename?: string): Promise<boolean> {
    if (diagram === null) {
      return false;
    }

    const info = new DiagramMetadata(diagram);
    let targetFilename = DiagramPrinter._getTargetFilename(folder, filename);

    if (info.fileType === DiagramPrinter.PDF) {
      console.info(`Printing PDF to file ${targetFilename}`);
      return diagram.flowchartAsPdf().copyFile(info.fullFilename, targetFilename, true);
    }

    if (info.fileType === DiagramPrinter.SPREADSHEET) {
      if (!targetFilename.endsWith(".xls")) {
        targetFilename += ".xls";
      }
      console.info(`Printing Excel to file ${targetFilename}`);
      return diagram.flowchartDataAsSpreadsheet().copyFile(info.fullFilename, targetFilename, true);
    }

    // default case - print to a physical printer
    const diagramPhysicalPrinter = new DiagramPhysicalPrinter();
    return await diagramPhysicalPrinter.doPrint(diagram, info, targetFilename);
  }

  printReport(
    diagram: FlowchartDiagram | null,
    reportTemplate: string,
    folder: string,
    filename: string,
    summarize: boolean
  ): boolean {
    if (diagram === null) {
      return false;
    }

    let report = diagram.report();
    const targetFilename = DiagramPrinter._getTargetFilename(folder, filename);
    console.info(`Creating report for ${diagram.name()} to file ${targetFilename}`);

    if (summarize) {
      diagram = diagram.summary();
      report.close();
      report = diagram.report();
      report.open(true);
      console.info(`Switched to summary report for ${diagram.name()}`);
    }

    if (!report.isOpen()) {
      console.error("Failed to open report for writing.");
      return false;
    }

    const data = diagram.reportData();

    if (!this._validateReport(reportTemplate, data)) {
      console.error("Failed to validate report template.");
      return false;
    }

    if (summarize) {
      data.add(diagram.summaryInformation());
      report.openWithContents(reportTemplate, data, true);
    } else {
      report.openWithContents(reportTemplate, data, false);
    }

    report.saveToFile(targetFilename);
    console.info("Report creation succeeded");
    return true;
  }

  printPages(diagram: FlowchartDiagram | null, builder: PagesBuilder): boolean {
    if (diagram === null) {
      return false;
    }

    const data = diagram.reportData();
    const pages = diagram.pagesData();

    const report = new DiagramPagesReport();
    const reportPages: DiagramReportPage[] = [];

    for (const page of pages) {
      const processedPage = builder.processPage(page, data);
      reportPages.push(processedPage);
    }

    return builder.apply(report, reportPages);
  }

  private static _getTargetFilename(folder?: string, filename?: string): string {
    const targetFolder = folder || os.homedir();
    const targetFilename = filename || "tempfile.tmp";
    return path.join(targetFolder, targetFilename);
  }

  private _validateReport(template: string, substitutions: FlowchartReportItems): boolean {
    try {
      this.createReport(template, substitutions.toArray());
      return true;
    } catch (error) {
      console.error("Report template did not match substitutions");
      return false;
    }
  }

  createReport(template: string, substitutions: any[]): string {
    return template.replace(/\{(\d+)\}/g, (match, index) => {
      return substitutions[parseInt(index)]?.toString() ?? match;
    });
  }
}

export { DiagramPrinter };
