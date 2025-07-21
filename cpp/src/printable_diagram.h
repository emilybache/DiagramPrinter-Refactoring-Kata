#ifndef PRINTABLE_DIAGRAM_H
#define PRINTABLE_DIAGRAM_H

#include <string>
#include "flowchart_diagram.h"
#include "diagram_metadata.h"

class PrintableDiagram {
private:
    FlowchartDiagram* _diagram;

public:
    explicit PrintableDiagram(FlowchartDiagram* diagram)
        : _diagram(diagram)
    {
    }

    virtual ~PrintableDiagram() = default;

    // Getter for diagram (equivalent to C# property)
    FlowchartDiagram* GetDiagram() const { return _diagram; }

    virtual DiagramMetadata* GetDiagramMetadata() {
        return new DiagramMetadata(*_diagram);
    }

    virtual bool PrintToFile(const std::string& fromFilename, const std::string& targetFilename) const {
        return _diagram->FlowchartAsPdf()->CopyFile(fromFilename, targetFilename, true);
    }

    virtual bool PrintToSpreadsheetFile(const std::string& fromFilename, const std::string& targetFilename) {
        return _diagram->FlowchartDataAsSpreadsheet()->CopyFile(fromFilename, targetFilename, true);
    }

    virtual std::string SummaryInformation() const {
        return _diagram->SummaryInformation();
    }

    // Delete copy and move operations since we manage a raw pointer
    PrintableDiagram(const PrintableDiagram&) = delete;
    PrintableDiagram& operator=(const PrintableDiagram&) = delete;
    PrintableDiagram(PrintableDiagram&&) = delete;
    PrintableDiagram& operator=(PrintableDiagram&&) = delete;
};

#endif // PRINTABLE_DIAGRAM_H
