
#ifndef PRINTER_DRIVER_FACTORY_H
#define PRINTER_DRIVER_FACTORY_H

#include "diagram_print_driver.h"

// Forward declaration for test class
class DiagramPrinterTests;

class PrinterDriverFactory {
protected:
    static inline PrinterDriverFactory* _instance;
    
    // Private constructor for singleton
    PrinterDriverFactory() = default;

public:
    static PrinterDriverFactory& GetInstance() {
        if (_instance == nullptr) {
            _instance = new PrinterDriverFactory();
        }
        return *_instance;
    }

    virtual DiagramPrintDriver* CreateDriverForPrint() {
        return new DiagramPrintDriver();
    }

    virtual ~PrinterDriverFactory() = default;

    // Delete copy and move operations
    PrinterDriverFactory(const PrinterDriverFactory&) = delete;
    PrinterDriverFactory& operator=(const PrinterDriverFactory&) = delete;
    PrinterDriverFactory(PrinterDriverFactory&&) = delete;
    PrinterDriverFactory& operator=(PrinterDriverFactory&&) = delete;

private:
    // Allow test class to modify the instance
    friend class PrinterDriverFactoryTest;
    static void SetInstance(PrinterDriverFactory* instance) {
        delete _instance;  // Clean up existing instance
        _instance = instance;
    }
    static void ResetInstance() {
        delete _instance;
        _instance = nullptr;
    }
};

#endif // PRINTER_DRIVER_FACTORY_H
