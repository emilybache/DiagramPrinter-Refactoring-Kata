export class FlowchartReportItems {
  constructor(name: string, serialNumber: string, filename: string) {
    // this implementation is deliberately blank - can't use this class in a unit test
  }

  add(summaryInformation: string): void {
    throw new Error("can't call from a unit test");
  }

  toArray(): Array<any> {
    throw new Error("can't call from a unit test");
  }
}

export class DiagramPage {
  private readonly _name: string;
  private _pageContents: string;

  constructor(name: string, pageContents: string) {
    this._name = name;
    this._pageContents = pageContents;
  }

  get name(): string {
    return this._name;
  }

  get pageContents(): string {
    return this._pageContents;
  }

  addDataFrom(data: FlowchartReportItems): void {
    try {
      if (this._pageContents.includes("{0}")) {
        this._pageContents = this._pageContents.replace(/\{(\d+)\}/g, (...args) => {
          const index = parseInt(args[1]);
          const dataArray = data.toArray();
          return dataArray[index]?.toString() ?? '';
        });
      }
    } catch (error) {
      // Silently handle formatting errors
    }
  }
}

export class PagesBuilder {
  processPage(page: DiagramPage, data: FlowchartReportItems): DiagramReportPage {
    page.addDataFrom(data);
    return new DiagramReportPage(page);
  }

  apply(report: DiagramPagesReport, reportPages: DiagramReportPage[]): boolean {
    if (report.isComplete()) {
      return false;
    }

    for (const page of reportPages) {
      report.addPage(page);
    }

    return true;
  }
}

export class DiagramReportPage {
  pages: DiagramPage[];

  constructor(page: DiagramPage) {
    this.pages = [page];
  }
}

export class DiagramPagesReport {
  private _pages: DiagramReportPage[] = [];

  addPage(page: DiagramReportPage): void {
    this._pages.push(page);
  }

  isComplete(): boolean {
    return this._pages.length > 0;
  }
}

export class FlowchartReport {
  close(): void {
    throw new Error("Not supported");
  }

  open(show: boolean): void {
    throw new Error("Not supported");
  }

  isOpen(): boolean {
    throw new Error("Not supported");
  }

  openWithContents(reportTemplate: string, substitutions: FlowchartReportItems, readOnly: boolean): void {
    throw new Error("Not supported");
  }

  saveToFile(targetFilename: string): void {
    throw new Error("Not supported");
  }
}
