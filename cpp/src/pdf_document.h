
#ifndef PDF_DOCUMENT_H
#define PDF_DOCUMENT_H

#include <string>
#include <stdexcept>

/**
 * This is the real production code class.
 * The implementation is omitted here, imagine it is from a third party library
 * which you can't change.
 */
class PdfDocument final {
public:
    bool CopyFile(const std::string& fromPath, 
                 const std::string& targetPath, 
                 bool failIfAlreadyExists) {
        throw std::runtime_error("Can't call this from a unit test");
    }

private:
    // you can't construct this in a unit test
    PdfDocument() = default;

    // Delete copy and move operations to ensure this class can't be copied or moved
    PdfDocument(const PdfDocument&) = delete;
    PdfDocument& operator=(const PdfDocument&) = delete;
    PdfDocument(PdfDocument&&) = delete;
    PdfDocument& operator=(PdfDocument&&) = delete;
};

#endif // PDF_DOCUMENT_H
