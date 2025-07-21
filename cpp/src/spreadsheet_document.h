#ifndef SPREADSHEET_DOCUMENT_H
#define SPREADSHEET_DOCUMENT_H

#include <string>
#include <stdexcept>

/**
 * This is the real production code class.
 * The implementation is omitted here, imagine it is from a third party library
 * which you can't change.
 */
class SpreadsheetDocument final {
public:
    bool CopyFile(const std::string& infoFullFilename, 
                 const std::string& targetFilename, 
                 bool overwrite) {
        throw std::runtime_error("Can't call this from a unit test");
    }

private:
    // can't construct it in a unit test
    SpreadsheetDocument() = default;

    // Delete copy and move operations to ensure this class can't be copied or moved
    SpreadsheetDocument(const SpreadsheetDocument&) = delete;
    SpreadsheetDocument& operator=(const SpreadsheetDocument&) = delete;
    SpreadsheetDocument(SpreadsheetDocument&&) = delete;
    SpreadsheetDocument& operator=(SpreadsheetDocument&&) = delete;
};

#endif // SPREADSHEET_DOCUMENT_H
