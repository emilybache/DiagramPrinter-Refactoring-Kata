
#ifndef FLOWCHART_DIAGRAM_H
#define FLOWCHART_DIAGRAM_H

#include <string>
#include <vector>
#include <stdexcept>
#include "pdf_document.h"
#include "spreadsheet_document.h"
#include "png_document.h"
#include "flowchart_report.h"
#include "flowchart_report_items.h"
#include "diagram_page.h"

/**
 * This is the production class used for real.
 * The implementation is largely omitted here, imagine it is much bigger and more complex.
 */
class FlowchartDiagram {
public:
    FlowchartDiagram() {
        throw std::runtime_error("you can't construct this in a unit test");
    }

    std::string Name() {
        throw std::runtime_error("Can't call this from a unit test");
    }

    std::string SerialNumber() {
        throw std::runtime_error("Can't call this from a unit test");
    }

    bool IsDisposed() {
        throw std::runtime_error("Can't call this from a unit test");
    }

    PdfDocument *FlowchartAsPdf() {
        throw std::runtime_error("Can't call this from a unit test");
    }

    SpreadsheetDocument *FlowchartDataAsSpreadsheet() {
        throw std::runtime_error("Can't call this from a unit test");
    }

    std::string SummaryInformation() {
        throw std::runtime_error("Can't call this from a unit test");
    }

    PngDocument *FlowchartThumbnail() {
        throw std::runtime_error("Can't call this from a unit test");
    }

    FlowchartReport *Report() {
        throw std::runtime_error("Not implemented");
    }

    FlowchartDiagram *Summary() {
        throw std::runtime_error("Not implemented");
    }

    FlowchartReportItems *ValidationProblems(const std::string& reportTemplate, 
                                          const std::vector<std::string>& data) {
        throw std::runtime_error("Not implemented");
    }

    FlowchartReportItems *ReportData() {
        return new FlowchartReportItems(this->Name(), this->SerialNumber(), 
                                  this->FlowchartThumbnail()->Filename());
    }

    std::vector<DiagramPage> *PagesData() {
        // fake implementation
        return new std::vector<DiagramPage>{
            DiagramPage(this->Name(), "page 1"),
            DiagramPage(this->Name(), "page 2")
        };
    }

    // Imagine about 200 more methods here
};

#endif // FLOWCHART_DIAGRAM_H
