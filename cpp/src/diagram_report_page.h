
#ifndef DIAGRAM_REPORT_PAGE_H
#define DIAGRAM_REPORT_PAGE_H

#include "diagram_page.h"

class DiagramReportPage {
public:
    explicit DiagramReportPage(const DiagramPage& page) : m_page(page) {}

private:
    DiagramPage m_page;
};

#endif // DIAGRAM_REPORT_PAGE_H
