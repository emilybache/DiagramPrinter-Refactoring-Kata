#ifndef DIAGRAM_PHYSICAL_PRINTER_H
#define DIAGRAM_PHYSICAL_PRINTER_H

#include <string>
#include <map>
#include "physical_printer.h"
#include "print_queue.h"
#include "printable_diagram.h"
#include "diagram_metadata.h"


class DiagramPhysicalPrinter {
private:
    PhysicalPrinter* _physicalPrinter;
    PrintQueue* _printQueue;

public:
    // Constructor with dependencies
    DiagramPhysicalPrinter(PhysicalPrinter* physicalPrinter, PrintQueue* printQueue);

    // Default constructor
    DiagramPhysicalPrinter();

    // Destructor
    ~DiagramPhysicalPrinter();

    // Print method
    bool DoPrint(const PrintableDiagram& printableDiagram,
                const DiagramMetadata& info,
                const std::string& targetFilename);
};

#endif // DIAGRAM_PHYSICAL_PRINTER_H
