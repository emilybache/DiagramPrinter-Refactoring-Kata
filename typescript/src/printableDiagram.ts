import {DiagramMetadata, FlowchartDiagram, IDiagramMetadata} from './documents';

export class PrintableDiagram {
  private readonly _diagram: FlowchartDiagram;

  constructor(diagram: FlowchartDiagram) {
    this._diagram = diagram;
  }

  getDiagram(): FlowchartDiagram {
    return this._diagram;
  }

  getDiagramMetadata(): IDiagramMetadata {
    return new DiagramMetadata(this._diagram);
  }

  printToFile(fromFilename: string, targetFilename: string): boolean {
    return this._diagram.flowchartAsPdf().copyFile(fromFilename, targetFilename, true);
  }

  printToSpreadsheetFile(fromFilename: string, targetFilename: string): boolean {
    return this._diagram.flowchartDataAsSpreadsheet().copyFile(fromFilename, targetFilename, true);
  }

  summaryInformation(): string {
    return this._diagram.summaryInformation();
  }
}
