#ifndef DIAGRAM_PAGES_REPORT_H
#define DIAGRAM_PAGES_REPORT_H

#include <vector>
#include "diagram_report_page.h"

class DiagramPagesReport {
public:
    DiagramPagesReport() = default;

    void AddPage(const DiagramReportPage& page);
    bool IsComplete() const;

private:
    std::vector<DiagramReportPage> pages;
};

#endif // DIAGRAM_PAGES_REPORT_H
