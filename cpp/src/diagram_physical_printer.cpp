#include "diagram_physical_printer.h"
#include "physical_printer.h"

#include <filesystem>
#include <iostream>
#include <mutex>

#include "printer_driver_factory.h"
#include "diagram_printer.h"

DiagramPhysicalPrinter::DiagramPhysicalPrinter(PhysicalPrinter* physicalPrinter,
                                               PrintQueue* printQueue)
    : _physicalPrinter(physicalPrinter)
    , _printQueue(printQueue)
{
}

DiagramPhysicalPrinter::DiagramPhysicalPrinter()
{
    _physicalPrinter = new PhysicalPrinter();
    _printQueue = new PrintQueue(_physicalPrinter);
}

DiagramPhysicalPrinter::~DiagramPhysicalPrinter()
{
    if (_physicalPrinter) {
        delete _physicalPrinter;
        _physicalPrinter = nullptr;
    }
    if (_printQueue) {
        delete _printQueue;
        _printQueue = nullptr;
    }
}

bool DiagramPhysicalPrinter::DoPrint(const PrintableDiagram& printableDiagram,
                                   const DiagramMetadata& info,
                                   const std::string& targetFilename)
{
    DiagramPrintDriver* printerDriver = PrinterDriverFactory::GetInstance().CreateDriverForPrint();
    printerDriver->SetDiagram(printableDiagram.GetDiagram());

    PrintMetadata data(info.getFileType());
    std::mutex _mutex;
    bool success = false;

    try {
        _mutex.lock();

        const auto& tonerLevels = _physicalPrinter->GetTonerLevels();

        if (!_physicalPrinter->IsAvailable() || !(tonerLevels.at(Toner::Black) > 0 &&
                                                  tonerLevels.at(Toner::Cyan) > 0 &&
                                                  tonerLevels.at(Toner::Magenta) > 0 &&
                                                  tonerLevels.at(Toner::Yellow) > 0)) {
            std::cout << "Physical Printer Unavailable" << std::endl;
            success = false;
        }
        else if (_physicalPrinter->GetJobCount() < 0) {
            std::cout << "Physical Printer Unavailable Due to Job Count Inconsistency" << std::endl;
            success = false;
        }
        else {
            // Print the diagram using the Physical Printer
            _printQueue->Add(data);
            std::string summaryInformation = printableDiagram.SummaryInformation();
            std::cout << "Diagram Summary Information: " << summaryInformation << std::endl;
            bool isSummary = summaryInformation.length() > 10;

            if (_physicalPrinter->StartDocument(!isSummary, false, "DiagramPhysicalPrinter")) {
                if (printerDriver->PrintTo(_physicalPrinter)) {
                    std::cout << "Physical Printer Successfully printed" << std::endl;
                    success = true;
                }
                _physicalPrinter->EndDocument();
            }
        }

        if (success) {
            // save a backup of the printed document as pdf
            if (std::filesystem::exists(data.GetFilename())) {
                std::cout << "Saving backup of printed document as PDF to file: " 
                         << targetFilename << std::endl;
                printableDiagram.PrintToFile(data.GetFilename(), targetFilename);
            }
        }
    }
    catch (const std::exception& e) {
        std::cerr << "Failed to print document: " << e.what() << std::endl;
        success = false;
    }

    _mutex.unlock();
    
    // Clean up the printer driver
    printerDriver->ReleaseDiagram();
    delete printerDriver;

    return success;
}