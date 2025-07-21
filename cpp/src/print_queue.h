
#ifndef PRINT_QUEUE_H
#define PRINT_QUEUE_H

#include <vector>
#include "physical_printer.h"
#include "print_metadata.h"

class PrintQueue {
private:
    std::vector<PrintMetadata> _items;
    PhysicalPrinter* _printer;

public:
    explicit PrintQueue(PhysicalPrinter* physicalPrinter)
        : _printer(physicalPrinter)
    {
    }

    virtual ~PrintQueue() = default;

    virtual void Add(const PrintMetadata& data) {
        _items.push_back(data);
    }

    // Delete copy and move operations since we manage a raw pointer
    PrintQueue(const PrintQueue&) = delete;
    PrintQueue& operator=(const PrintQueue&) = delete;
    PrintQueue(PrintQueue&&) = delete;
    PrintQueue& operator=(PrintQueue&&) = delete;
};

#endif // PRINT_QUEUE_H
