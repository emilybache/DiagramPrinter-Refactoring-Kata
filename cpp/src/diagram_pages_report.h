#ifndef DIAGRAM_PAGES_REPORT_H
#define DIAGRAM_PAGES_REPORT_H

#include <vector>
#include "diagram_report_page.h"

class DiagramPagesReport {
public:
    DiagramPagesReport() = default;

    void AddPage(const DiagramReportPage& page) {
        m_pages.push_back(page);
    }
    
    [[nodiscard]] bool IsComplete() const {
        return !m_pages.empty();
    }

private:
    std::vector<DiagramReportPage> m_pages;
};


#endif // DIAGRAM_PAGES_REPORT_H
