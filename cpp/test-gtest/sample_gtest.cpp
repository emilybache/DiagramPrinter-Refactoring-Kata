#include <gtest/gtest.h>

using namespace std;

#include "diagram_physical_printer.h"
#include "diagram_printer.h"
#include "fake_flowchart_report_items.h"

TEST(DiagramPrinterTest, PrintSummaryWithEmptyDocumentFails) {
    DiagramPrinter printer;
    std::string output;
    
    bool result = printer.PrintSummary(nullptr, "swedish", output);
    
    EXPECT_FALSE(result);
    EXPECT_TRUE(output.empty());
}

TEST(DiagramPrinterTest, PrintDiagramWithEmptyDocumentFails) {
    DiagramPrinter printer;
    FlowchartDiagram *diagram = nullptr;

    bool result = printer.PrintDiagram(diagram, "folder", "filename");

    EXPECT_FALSE(result);
}

TEST(DiagramPrinterTest, PrintReportWithEmptyDocumentFails) {
    DiagramPrinter printer;
    std::string templ = "Report for FlowchartDiagram {0} {1} {2}";
    auto fakeFlowchartReportItems = FakeFlowchartReportItems("DiagramName", "Serial Number", "Filename");

    bool result = printer.PrintReport(nullptr, templ);

    EXPECT_FALSE(result);
}

TEST(DiagramPrinterTest, PrintPagesWithEmptyDocumentFails) {
    DiagramPrinter printer;
    PagesBuilder builder;

    bool result = printer.PrintPages(nullptr, builder);

    EXPECT_FALSE(result);
}

TEST(DiagramPrinterTest, ValidateReportWithMatchingTemplateSucceeds) {
    DiagramPrinter printer;
    std::string templ = "Report for FlowchartDiagram {0} {1} {2}";
    auto fakeFlowchartReportItems = FakeFlowchartReportItems("DiagramName", "Serial Number", "Filename");

    bool result = printer.ValidateReport(templ, &fakeFlowchartReportItems);

    EXPECT_TRUE(result);
}