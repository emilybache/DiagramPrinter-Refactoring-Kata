import {FlowchartReportItems} from "../src/reporting";

/**
 * Use this class in your unit tests to replace a real FlowchartReportItems class
 */
export class FakeFlowchartReportItems extends FlowchartReportItems {
  private _data: string[] = [];

  constructor(name: string, serialNumber: string, filename: string) {
    super(name, serialNumber, filename);
    this._data = [name, serialNumber, filename];
  }

  override add(summaryInformation: string): void {
    this._data.push(summaryInformation);
  }

  override toArray(): Array<any> {
    return [...this._data];
  }
}
