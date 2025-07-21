
#ifndef PHYSICAL_PRINTER_H
#define PHYSICAL_PRINTER_H

#include <string>
#include <map>
#include <stdexcept>

enum class Toner {
    Black,
    Cyan,
    Magenta,
    Yellow
};

class PhysicalPrinter {
protected:
    bool _isAvailable;
    std::map<Toner, int> _tonerLevels;
    int _jobCount;

public:
    PhysicalPrinter()
        : _isAvailable(false)
        , _jobCount(0)
    {
        // Initialize toner levels
        _tonerLevels[Toner::Black] = 0;
        _tonerLevels[Toner::Cyan] = 0;
        _tonerLevels[Toner::Magenta] = 0;
        _tonerLevels[Toner::Yellow] = 0;
    }

    virtual ~PhysicalPrinter() = default;

    // Getters
    bool IsAvailable() const { return _isAvailable; }
    int GetJobCount() const { return _jobCount; }
    const std::map<Toner, int>& GetTonerLevels() const { return _tonerLevels; }

    // Virtual methods
    virtual bool StartDocument(bool isSummary, bool isPdf, const std::string& name) {
        throw std::runtime_error("Can't call this from a unit test");
    }

    virtual void EndDocument() {
        throw std::runtime_error("Can't call this from a unit test");
    }
};

#endif // PHYSICAL_PRINTER_H
