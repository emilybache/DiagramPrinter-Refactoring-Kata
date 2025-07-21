#ifndef DIAGRAM_SUMMARY_H
#define DIAGRAM_SUMMARY_H

#include <string>
#include "png_document.h"

class DiagramSummary {
public:
    explicit DiagramSummary(const std::string& language);

    void AddHeader(const std::string& summaryInformation);
    void AddTitle(const std::string& name, const std::string& serialNumber);
    void AddImage(const PngDocument *flowchart);
    std::string Export();

private:
    const std::string _language;
    std::string contents;
};

#endif // DIAGRAM_SUMMARY_H
