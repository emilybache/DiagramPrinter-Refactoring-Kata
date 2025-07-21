
#include "pages_builder.h"

DiagramReportPage PagesBuilder::ProcessPage(const DiagramPage& page, const FlowchartReportItems& data) {
    DiagramPage modifiedPage = page; // Create a copy
    modifiedPage.AddDataFrom(data);
    return DiagramReportPage(modifiedPage);
}

bool PagesBuilder::Apply(DiagramPagesReport& report, const std::vector<DiagramReportPage>& reportPages) {
    if (report.IsComplete()) {
        return false;
    }
    
    for (const auto& page : reportPages) {
        report.AddPage(page);
    }
    
    return true;
}
