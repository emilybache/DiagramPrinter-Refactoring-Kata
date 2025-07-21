
#ifndef DIAGRAM_PRINT_DRIVER_H
#define DIAGRAM_PRINT_DRIVER_H

#include <stdexcept>
#include "flowchart_diagram.h"
#include "physical_printer.h"

class DiagramPrintDriver {
private:
    FlowchartDiagram* _diagram{nullptr};

public:
    DiagramPrintDriver() = default;
    virtual ~DiagramPrintDriver() = default;

    void SetDiagram(FlowchartDiagram* diagram) {
        _diagram = diagram;
    }

    void ReleaseDiagram() {
        _diagram = nullptr;
    }

    virtual bool PrintTo(PhysicalPrinter* physicalPrinter) {
        throw std::runtime_error("Can't call this from a unit test");
    }

    // Delete copy and move operations
    DiagramPrintDriver(const DiagramPrintDriver&) = delete;
    DiagramPrintDriver& operator=(const DiagramPrintDriver&) = delete;
    DiagramPrintDriver(DiagramPrintDriver&&) = delete;
    DiagramPrintDriver& operator=(DiagramPrintDriver&&) = delete;
};

#endif // DIAGRAM_PRINT_DRIVER_H
