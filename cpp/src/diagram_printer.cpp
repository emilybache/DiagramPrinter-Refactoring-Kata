#include "diagram_printer.h"

#include <iostream>
#include <filesystem>

#include "diagram_physical_printer.h"

// Initialize static constants
const std::string DiagramPrinter::Spreadsheet = "Spreadsheet";
const std::string DiagramPrinter::Pdf = "PDF";


bool DiagramPrinter::PrintSummary(FlowchartDiagram* diagram, const std::string& language, std::string& summaryText)
{
    if (diagram == nullptr)
    {
        summaryText = "";
        return false;
    }

    DiagramSummary summary(language);
    summary.AddTitle(diagram->Name(), diagram->SerialNumber());
    summary.AddHeader(diagram->SummaryInformation());
    summary.AddImage(diagram->FlowchartThumbnail());
    summaryText = summary.Export();
    return true;
}

bool DiagramPrinter::PrintDiagram(FlowchartDiagram* diagram,
                                  const std::string &folder,
                                  const std::string &filename) {
    if (diagram == nullptr) {
        return false;
    }
    DiagramMetadata info(diagram);
    
    if (info.getFileType() == Pdf) {
        std::string targetFilename = GetTargetFilename(folder, filename);
        return diagram->FlowchartAsPdf()->CopyFile(info.getFullFilename(), 
                                                 targetFilename, 
                                                 true);
    }

    if (info.getFileType() == Spreadsheet) {
        std::string targetFilename = GetTargetFilename(folder, filename);
        if (targetFilename.substr(targetFilename.length() - 4) != ".xls") {
            targetFilename += ".xls";
        }
        return diagram->FlowchartDataAsSpreadsheet()->CopyFile(info.getFullFilename(), 
                                                            targetFilename, 
                                                            true);
    }

    // default case - print to a physical printer
    DiagramPhysicalPrinter printer;
    return printer.DoPrint(diagram, &info, GetTargetFilename(folder, filename));

}


bool DiagramPrinter::PrintReport(FlowchartDiagram* diagram,
                               const std::string& reportTemplate,
                               const std::string& folder,
                               const std::string& filename,
                               bool summarize) {
    if (diagram == nullptr) {
        return false;
    }

    FlowchartReport *report = diagram->Report();
    std::string targetFilename = GetTargetFilename(folder, filename);
    std::cout << "Creating report for " << diagram->Name() 
              << " to file " << targetFilename << std::endl;

    if (summarize) {
        diagram = diagram->Summary();
        report->Close();
        report = diagram->Report();
        report->Open(true);
        std::cout << "Switched to summary report for " << diagram->Name() << std::endl;
    }

    if (!report->isOpen()) {
        std::cerr << "Failed to open report for writing." << std::endl;
        return false;
    }

    auto data = diagram->ReportData();

    if (!ValidateReport(reportTemplate, data)) {
        std::cerr << "Failed to validate report template." << std::endl;
        return false;
    }

    if (summarize) {
        data->Add(diagram->SummaryInformation());
        report->OpenWithContents(reportTemplate, data, true);
    } else {
        report->OpenWithContents(reportTemplate, data, false);
    }

    report->SaveToFile(targetFilename);
    std::cout << "Report creation succeeded" << std::endl;
    return true;
}

bool DiagramPrinter::PrintPages(FlowchartDiagram* diagram, PagesBuilder& builder) {
    if (diagram == nullptr) {
        return false;
    }

    FlowchartReportItems *data = diagram->ReportData();
    std::vector<DiagramPage> *pages = diagram->PagesData();

    DiagramPagesReport report;
    std::vector<DiagramReportPage> reportPages;

    for (const auto& page : *pages) {
        DiagramReportPage processedPage = builder.ProcessPage(page, *data);
        reportPages.push_back(processedPage);
    }

    return builder.Apply(report, reportPages);
}

std::string DiagramPrinter::GetTargetFilename(const std::string& folder,
                                            const std::string& filename) {
    namespace fs = std::filesystem;
    
    fs::path folderPath;
    if (folder.empty())
        folderPath = fs::absolute(fs::temp_directory_path());
    else
        folderPath = folder;

    std::string tempName;
    if (filename.empty()) {
        // Create temporary filename
        tempName = "temporaryFilename";
    } else {
        tempName = filename;
    }

    return (folderPath / tempName).string();
}

bool DiagramPrinter::ValidateReport(const std::string& template_str,
                                  const FlowchartReportItems *substitutions) {
    try {
        auto substitutions_array = substitutions->ToArray();
        CreateReport(template_str, &substitutions_array);
        return true;
    } catch (const std::format_error& e) {
        std::cerr << "Report template did not match substitutions" << std::endl;
        return false;
    }
}

std::string DiagramPrinter::CreateReport(const std::string& template_str,
                                         const std::vector<std::string> *substitutions) {
    std::string result = template_str;

    for (size_t i = 0; i < substitutions->size(); ++i) {
        std::string placeholder = "{" + std::to_string(i) + "}";
        size_t pos = template_str.find(placeholder);
        if (pos != std::string::npos) {
            result.replace(pos, placeholder.length(), substitutions->at(i));
        }
    }
    return result;
}

