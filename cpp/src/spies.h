
#ifndef SPY_CLASSES_H
#define SPY_CLASSES_H

#include <vector>
#include <string>
#include <map>
#include <filesystem>

#include "physical_printer.h"
#include "printable_diagram.h"
#include "printer_driver_factory.h"
#include "print_queue.h"

/*
 * These classes are used by unit tests and not in production
 */

class SpyPhysicalPrinter : public PhysicalPrinter {
public:
    explicit SpyPhysicalPrinter(std::vector<std::string>& messages) 
        : m_messages(messages) {
        _isAvailable = true;
        _tonerLevels[Toner::Black] = 100;
        _tonerLevels[Toner::Cyan] = 100;
        _tonerLevels[Toner::Magenta] = 100;
        _tonerLevels[Toner::Yellow] = 100;
    }

    bool StartDocument(bool is_summary, bool is_pdf, const std::string& name) override {
        m_messages.push_back("Start Document");
        return true;
    }

    void EndDocument() override {
        m_messages.push_back("End Document");
    }

private:
    std::vector<std::string>& m_messages;
};

class SpyQueue : public PrintQueue {
public:
    SpyQueue(std::vector<std::string>& messages, PhysicalPrinter& physical_printer)
        : PrintQueue(&physical_printer), m_messages(messages) {}

    void Add(const PrintMetadata& data) override {
        PrintQueue::Add(data);
        m_messages.push_back("Added metadata to print queue");
    }

private:
    std::vector<std::string>& m_messages;
};


class DiagramMetadataTestAdapter : public DiagramMetadata {
public:
    DiagramMetadataTestAdapter(const std::string& filename,
                              const std::string& filetype,
                              bool available) {
        this->m_fullFilename = filename;
        this->m_fileType = filetype;
        this->m_fileAvailable = available;
    }
};

class SpyPrintableDiagram : public PrintableDiagram {
public:
    SpyPrintableDiagram(std::vector<std::string>& messages, 
                        const std::string& full_filename,
                        const std::string& file_type,
                        bool file_available)
        : PrintableDiagram(nullptr)
        , m_messages(messages)
        , m_full_filename(full_filename)
        , m_file_type(file_type)
        , m_file_available(file_available)
        , m_spy("") {}

    DiagramMetadata* GetDiagramMetadata() override {
        return new DiagramMetadataTestAdapter(m_full_filename, m_file_type, m_file_available);
    }

    std::string SummaryInformation() const override {
        return "summary information";
    }

    bool PrintToFile(const std::string& toFilename, const std::string& targetFilename) const override {
        bool folder_ok = std::filesystem::path(targetFilename).has_root_path();
        m_messages.push_back("Print to File (Filename=" + toFilename + 
                            ", folderValid=" + (folder_ok ? "true" : "false") + ")");
        return true;
    }

    bool PrintToSpreadsheetFile(const std::string& toFilename, 
                               const std::string& targetFilename) override {
        return PrintToFile(toFilename, targetFilename);
    }

    const std::string& GetSpy() const { return m_spy; }

private:
    std::vector<std::string>& m_messages;
    std::string m_full_filename;
    std::string m_file_type;
    bool m_file_available;
    std::string m_spy;
};



class SpyDiagramPrintDriver : public DiagramPrintDriver {
public:
    explicit SpyDiagramPrintDriver(std::vector<std::string>& messages)
        : m_messages(messages) {}

    bool PrintTo(PhysicalPrinter* physicalPrinter) override {
        m_messages.push_back("DiagramPrintDriver is printing to physical printer");
        return true;
    }

private:
    std::vector<std::string>& m_messages;
};

class PrinterDriverFactoryTestAdapter : public PrinterDriverFactory {
public:
    explicit PrinterDriverFactoryTestAdapter(std::vector<std::string>& messages)
        : PrinterDriverFactory(), m_messages(messages) {
    }

    DiagramPrintDriver* CreateDriverForPrint() override {
        return new SpyDiagramPrintDriver(m_messages);
    }

private:
    std::vector<std::string>& m_messages;
};


#endif // SPY_CLASSES_H
