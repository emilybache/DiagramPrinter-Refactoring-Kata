
#ifndef PNG_DOCUMENT_H
#define PNG_DOCUMENT_H

#include <string>

class PngDocument {
public:
    explicit PngDocument(const std::string& filename)
        : m_filename(filename) {
    }

    std::string Filename() const {
        return m_filename;
    }

private:
    const std::string m_filename;
};

#endif // PNG_DOCUMENT_H
