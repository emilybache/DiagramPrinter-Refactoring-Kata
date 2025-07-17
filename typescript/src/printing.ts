import { FlowchartDiagram } from './documents';
import * as path from 'path';
import * as fs from 'fs';

enum Toner {
  Black,
  Cyan,
  Magenta,
  Yellow
}

class PhysicalPrinter {
  private _isAvailable: boolean = false;
  private _jobCount: number = 0;
  private _tonerLevels: Map<Toner, number>;

  constructor() {
    this._tonerLevels = new Map([
      [Toner.Black, 0],
      [Toner.Cyan, 0],
      [Toner.Magenta, 0],
      [Toner.Yellow, 0]
    ]);
  }

  get tonerLevels(): Map<Toner, number> {
    return this._tonerLevels;
  }

  get isAvailable(): boolean {
    return this._isAvailable;
  }

  get jobCount(): number {
    return this._jobCount;
  }

  startDocument(isSummary: boolean, isPdf: boolean, name: string): boolean {
    throw new Error("Can't call this from a unit test");
  }

  endDocument(): void {
    throw new Error("Can't call this from a unit test");
  }
}

class PrintQueue {
  private _printer: PhysicalPrinter;
  private _items: any[] = [];

  constructor(physicalPrinter: PhysicalPrinter) {
    this._printer = physicalPrinter;
  }

  add(data: any): void {
    this._items.push(data);
  }
}

class PrintMetadata {
  private _infoFileType: string;
  filename: string;

  constructor(infoFileType: string) {
    this._infoFileType = infoFileType;
    this.filename = this._generateTempFilename(infoFileType);
  }

  private _generateTempFilename(infoFileType: string): string {
    const tempFilename = path.join(path.dirname(__filename), "tempfile");
    return `${tempFilename}.${infoFileType}`;
  }
}

class DiagramPrintDriver {
  private _diagram: FlowchartDiagram | null = null;

  setDiagram(diagram: FlowchartDiagram): void {
    this._diagram = diagram;
  }

  releaseDiagram(): void {
    this._diagram = null;
  }

  printTo(physicalPrinter: PhysicalPrinter): boolean {
    throw new Error("Can't call this from a unit test");
  }
}

class PrinterDriverFactory {
  private static _instance: PrinterDriverFactory | null = null;

  static getInstance(): PrinterDriverFactory {
    if (PrinterDriverFactory._instance === null) {
      PrinterDriverFactory._instance = new PrinterDriverFactory();
    }
    return PrinterDriverFactory._instance;
  }

  // For unit tests
  static setInstance(instance: PrinterDriverFactory): void {
    PrinterDriverFactory._instance = instance;
  }

  createDriverForPrint(): DiagramPrintDriver {
    return new DiagramPrintDriver();
  }
}

export {
  Toner,
  PhysicalPrinter,
  PrintQueue,
  PrintMetadata,
  DiagramPrintDriver,
  PrinterDriverFactory
};
