import {DiagramPrintDriver, PhysicalPrinter, PrintQueue, Toner} from "../src/printing";
import {PrintableDiagram} from "../src/printableDiagram";
import {IDiagramMetadata} from "../src/documents";

export class SpyPhysicalPrinter extends PhysicalPrinter {
  private _spy: string[];

  constructor(spy: string[]) {
    super();
    this._spy = spy;
    this._isAvailable = true;
    this._tonerLevels = new Map([
      [Toner.Black, 100],
      [Toner.Cyan, 100],
      [Toner.Magenta, 100],
      [Toner.Yellow, 100]
    ]);
  }

  startDocument(is_summary: boolean, is_pdf: boolean, name: string): boolean {
    this._spy.push("Start Document");
    return true;
  }

  endDocument(): void {
    this._spy.push("End Document");
  }
}

export class SpyQueue extends PrintQueue {
  private _spy: string[];

  constructor(spy: string[], physical_printer: PhysicalPrinter) {
    super(physical_printer);
    this._spy = spy;
  }

  add(data: any): void {
    // Call parent class add method if it existed
    this._spy.push("Added metadata to print queue");
  }
}

export class SpyPrintableDiagram extends PrintableDiagram {
  private _spy: string[];
  private _full_filename: string;
  private _file_type: string;
  private _file_available: boolean;

  constructor(spy: string[], full_filename: string, file_type: string, file_available: boolean) {
    // @ts-ignore
    super(null);
    this._spy = spy;
    this._full_filename = full_filename;
    this._file_type = file_type;
    this._file_available = file_available;
  }

  getDiagramMetadata(): IDiagramMetadata {
    return new StubDiagramMetadata(this._full_filename, this._file_type, this._file_available);
  }

  summaryInformation(): string {
    return "summary information";
  }

  printToFile(to_filename: string, target_filename: string): boolean {
    const folder_ok = Boolean(target_filename);
    this._spy.push(`Print to File (Filename=${to_filename}, folderValid=${folder_ok})`);
    return true;
  }

  printToSpreadsheetFile(to_filename: string, target_filename: string): boolean {
    return this.printToFile(to_filename, target_filename);
  }
}

export class StubDiagramMetadata implements IDiagramMetadata {
  fullFilename: string;
  fileType: string;
  fileAvailable: boolean;

  constructor(filename: string, filetype: string, available: boolean) {
    this.fullFilename = filename;
    this.fileType = filetype;
    this.fileAvailable = available;
  }
}

export class StubPrinterDriverFactory {
  private _spy: string[];

  constructor(spy: string[]) {
    this._spy = spy;
  }

  createDriverForPrint(): DiagramPrintDriver {
    return new SpyDiagramPrintDriver(this._spy);
  }
}

export class SpyDiagramPrintDriver extends DiagramPrintDriver {
  private _spy: string[];

  constructor(spy: string[]) {
    super();
    this._spy = spy;
  }

  printTo(physical_printer: PhysicalPrinter): boolean {
    this._spy.push("DiagramPrintDriver is printing to physical printer");
    return true;
  }
}
