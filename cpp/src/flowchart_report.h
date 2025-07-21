
#ifndef FLOWCHART_REPORT_H
#define FLOWCHART_REPORT_H

#include <string>
#include <stdexcept>

class FlowchartReportItems; // Forward declaration

class FlowchartReport {
public:
    void Close() {
        throw std::runtime_error("Not implemented");
    }

    void Open(bool show) {
        throw std::runtime_error("Not implemented");
    }

    bool isOpen() {
        throw std::runtime_error("Not implemented");
    }

    void OpenWithContents(const std::string& reportTemplate, 
                         const FlowchartReportItems *substitutions, 
                         bool readOnly) {
        throw std::runtime_error("Not implemented");
    }

    void SaveToFile(const std::string& targetFilename) {
        throw std::runtime_error("Not implemented");
    }
};

#endif // FLOWCHART_REPORT_H
