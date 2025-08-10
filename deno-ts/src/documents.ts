import {
  DiagramPage,
  FlowchartReport,
  FlowchartReportItems,
} from "./reporting.ts";

class DiagramSummary {
  private _language: string;
  contents: string = "";

  constructor(language: string) {
    this._language = language;
  }

  addHeader(summaryInformation: string): void {
    this.contents += summaryInformation + "\n";
  }

  addTitle(name: string, serialNumber: string): void {
    this.contents += name + "\n" + serialNumber + "\n";
  }

  addImage(flowchart: PngDocument): void {
    this.contents += flowchart.filename();
  }

  export(): string {
    // imagine a lot more detail and complexity here
    return this.contents;
  }
}

class FlowchartDiagram {
  name(): string {
    throw new Error("Can't construct this in a unit test");
  }

  serialNumber(): string {
    throw new Error("Can't construct this in a unit test");
  }

  isDisposed(): boolean {
    throw new Error("Can't construct this in a unit test");
  }

  flowchartAsPdf(): PdfDocument {
    throw new Error("Can't construct this in a unit test");
  }

  flowchartDataAsSpreadsheet(): SpreadsheetDocument {
    throw new Error("Can't construct this in a unit test");
  }

  summaryInformation(): string {
    throw new Error("Can't construct this in a unit test");
  }

  flowchartThumbnail(): PngDocument {
    throw new Error("Can't construct this in a unit test");
  }

  report(): FlowchartReport {
    throw new Error("Can't call this from a unit test");
  }

  summary(): FlowchartDiagram {
    throw new Error("Can't call this from a unit test");
  }

  validationProblems(reportTemplate: string, data: string[]): void {
    throw new Error("Can't call this from a unit test");
  }

  reportData(): FlowchartReportItems {
    return new FlowchartReportItems(
      this.name(),
      this.serialNumber(),
      this.flowchartThumbnail().filename(),
    );
  }

  pagesData(): DiagramPage[] {
    // fake implementation
    return [
      new DiagramPage(this.name(), "page 1"),
      new DiagramPage(this.name(), "page 2"),
    ];
  }
  // imagine about 200 more methods defined here
}

class PdfDocument {
  constructor() {
    throw new Error("Can't construct this in a unit test");
  }

  copyFile(
    fromPath: string,
    targetPath: string,
    failIfAlreadyExists: boolean,
  ): boolean {
    throw new Error("Can't call this from a unit test");
  }
}

class PngDocument {
  private _filename: string;

  constructor(filename: string) {
    this._filename = filename;
  }

  filename(): string {
    return this._filename;
  }
}

class SpreadsheetDocument {
  constructor() {
    throw new Error("Can't construct this in a unit test");
  }

  copyFile(
    infoFullFilename: string,
    targetFilename: string,
    overwrite: boolean,
  ): boolean {
    throw new Error("Can't call this from a unit test");
  }
}

export interface IDiagramMetadata {
  fullFilename: string;
  fileType: string;
  fileAvailable: boolean;
}

class DiagramMetadata implements IDiagramMetadata {
  fullFilename: string;
  fileType: string;
  fileAvailable: boolean;

  constructor(idiagram: FlowchartDiagram) {
    this.fullFilename = `${idiagram.name()}_${idiagram.serialNumber()}`;
    this.fileType = idiagram.name().includes("Flowchart")
      ? "PDF"
      : "Spreadsheet";
    this.fileAvailable = !idiagram.isDisposed();
  }
}

export {
  DiagramMetadata,
  DiagramSummary,
  FlowchartDiagram,
  PdfDocument,
  PngDocument,
  SpreadsheetDocument,
};
