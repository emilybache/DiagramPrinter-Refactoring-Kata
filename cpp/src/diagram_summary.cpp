#include "diagram_summary.h"

DiagramSummary::DiagramSummary(const std::string& language)
    : _language(language)
    , contents("")
{
}

void DiagramSummary::AddHeader(const std::string& summaryInformation)
{
    contents += summaryInformation;
    contents += "\n";
}

void DiagramSummary::AddTitle(const std::string& name, const std::string& serialNumber)
{
    contents += name;
    contents += "\n";
    contents += serialNumber;
    contents += "\n";
}

void DiagramSummary::AddImage(const PngDocument *flowchart)
{
    contents += flowchart->Filename();
}

std::string DiagramSummary::Export()
{
    // imagine a lot more detail and complexity here
    return contents;
}
