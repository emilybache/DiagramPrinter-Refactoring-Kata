#ifndef FAKE_FLOWCHART_REPORT_ITEMS_H
#define FAKE_FLOWCHART_REPORT_ITEMS_H

#include "flowchart_report_items.h"
#include <vector>
#include <string>

/**
 * Use this class in your unit tests to replace a real FlowchartReportItems class
 */
class FakeFlowchartReportItems : public FlowchartReportItems {
public:
    FakeFlowchartReportItems(const std::string& name, 
                            const std::string& serialNumber, 
                            const std::string& filename)
        : FlowchartReportItems(name, serialNumber, filename) {
        m_data.push_back(name);
        m_data.push_back(serialNumber);
        m_data.push_back(filename);
    }

    void Add(const std::string& summaryInformation) override {
        m_data.push_back(summaryInformation);
    }

    [[nodiscard]] std::vector<std::string> ToArray() const override {
        return m_data;
    }

private:
    std::vector<std::string> m_data;
};

#endif // FAKE_FLOWCHART_REPORT_ITEMS_H
