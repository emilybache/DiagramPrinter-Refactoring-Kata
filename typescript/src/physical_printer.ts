import * as fs from 'fs';
import {FlowchartDiagram, IDiagramMetadata} from './documents';
import {PhysicalPrinter, PrinterDriverFactory, PrintMetadata, PrintQueue, Toner} from './printing';
import {Mutex} from 'async-mutex';
import {PrintableDiagram} from "./printableDiagram"; // for thread-safe locking

class DiagramPhysicalPrinter {
  private _physicalPrinter: PhysicalPrinter;
  private _printQueue: PrintQueue;

  constructor(physicalPrinter?: PhysicalPrinter, printQueue?: PrintQueue) {
    this._physicalPrinter = physicalPrinter || new PhysicalPrinter();
    this._printQueue = printQueue || new PrintQueue(this._physicalPrinter);
  }

  async doPrint(
    diagram: PrintableDiagram,
    info: IDiagramMetadata,
    targetFilename: string
  ): Promise<boolean> {
    const printerDriver = PrinterDriverFactory.getInstance().createDriverForPrint();
    printerDriver.setDiagram(diagram.getDiagram() as FlowchartDiagram);

    const data = new PrintMetadata(info.fileType);
    const mutex = new Mutex();
    let success = false;

    try {
      await mutex.acquire();

      const hasEnoughToner = this._physicalPrinter.tonerLevels.get(Toner.Black)! > 0 &&
        this._physicalPrinter.tonerLevels.get(Toner.Cyan)! > 0 &&
        this._physicalPrinter.tonerLevels.get(Toner.Magenta)! > 0 &&
        this._physicalPrinter.tonerLevels.get(Toner.Yellow)! > 0;

      if (!this._physicalPrinter.isAvailable || !hasEnoughToner) {
        console.info("Physical Printer Unavailable");
      } else if (this._physicalPrinter.jobCount < 0) {
        console.info("Physical Printer Unavailable Due to Job Count Inconsistency");
      } else {
        // Print the diagram using the Physical Printer
        this._printQueue.add(data);
        const summaryInformation = diagram.summaryInformation();
        console.info(`Diagram Summary Information ${summaryInformation}`);

        const isSummary = summaryInformation.length > 10;
        if (this._physicalPrinter.startDocument(!isSummary, false, "DiagramPhysicalPrinter")) {
          if (printerDriver.printTo(this._physicalPrinter)) {
            console.info("Physical Printer Successfully printed");
            success = true;
          }
          this._physicalPrinter.endDocument();
        }

        if (success) {
          // save a backup of the printed document as pdf
          if (fs.existsSync(data.filename)) {
            console.info(`Saving backup of printed document as PDF to file ${targetFilename}`);
            diagram.printToFile(data.filename, targetFilename);
          }
        }
      }
    } finally {
      mutex.release();
      printerDriver.releaseDiagram();
    }

    return success;
  }
}

export { DiagramPhysicalPrinter };
