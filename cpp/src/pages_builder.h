
#ifndef PAGES_BUILDER_H
#define PAGES_BUILDER_H

#include <vector>
#include "diagram_page.h"
#include "flowchart_report_items.h"
#include "diagram_report_page.h"
#include "diagram_pages_report.h"

class PagesBuilder {
public:
    DiagramReportPage ProcessPage(const DiagramPage& page, const FlowchartReportItems& data);
    bool Apply(DiagramPagesReport& report, const std::vector<DiagramReportPage>& reportPages);
};

#endif // PAGES_BUILDER_H
