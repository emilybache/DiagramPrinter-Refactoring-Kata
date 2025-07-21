#ifndef FLOWCHART_REPORT_ITEMS_H
#define FLOWCHART_REPORT_ITEMS_H

#include <string>
#include <vector>
#include <stdexcept>

class FlowchartReportItems {
public:
    FlowchartReportItems(const std::string& name, 
                        const std::string& serialNumber, 
                        const std::string& filename) {
        // this implementation is deliberately blank - can't use this class in a unit test
    }

    virtual void Add(const std::string& summaryInformation) {
        throw std::runtime_error("can't call from a unit test");
    }

    [[nodiscard]] virtual std::vector<std::string> ToArray() const {
        throw std::runtime_error("can't call from a unit test");
    }

    // Virtual destructor for proper cleanup in inheritance
    virtual ~FlowchartReportItems() = default;
};

#endif // FLOWCHART_REPORT_ITEMS_H
