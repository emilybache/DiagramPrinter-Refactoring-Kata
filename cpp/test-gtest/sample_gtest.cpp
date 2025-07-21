#include <gtest/gtest.h>

using namespace std;

#include "diagram_physical_printer.h"
#include "diagram_printer.h"
#include "fake_flowchart_report_items.h"
#include "spies.h"

class PrinterDriverFactoryTest : public ::testing::Test {
protected:
    std::vector<std::string> spy;
    SpyPhysicalPrinter* spyPrinter;
    SpyQueue* spyQueue;
    DiagramPhysicalPrinter* printer;
    PrinterDriverFactory* factory;

    PrinterDriverFactoryTest() 
    : spyPrinter(nullptr)
    , spyQueue(nullptr)
    , printer(nullptr)
    , factory(nullptr) {
    }

    void SetUp() override {
        spyPrinter = new SpyPhysicalPrinter(spy);
        spyQueue = new SpyQueue(spy, *spyPrinter);
        printer = new DiagramPhysicalPrinter(spyPrinter, spyQueue);
        factory = new PrinterDriverFactoryTestAdapter(spy);
        PrinterDriverFactory::SetInstance(factory);
    }
    
    void TearDown() override {
        PrinterDriverFactory::ResetInstance();
    }
};

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

TEST(DiagramPrinterTest, PrintDiagramPdfDocumentSucceeds) {
    std::vector<std::string> messages;
    DiagramPrinter printer;
    auto diagram = new SpyPrintableDiagram(
        messages, 
        "filename", 
        DiagramPrinter::Pdf, 
        true
    );

    bool result = printer.PrintDiagram(diagram);

    EXPECT_TRUE(result);
    EXPECT_EQ(messages.size(), 1);
    EXPECT_EQ(messages[0], "Print to File (Filename=filename, folderValid=true)");
}

TEST(DiagramPrinterTest, PrintDiagramSpreadsheetDocumentSucceeds) {
    std::vector<std::string> messages;
    DiagramPrinter printer;
    auto diagram = new SpyPrintableDiagram(
        messages, 
        "filename", 
        DiagramPrinter::Spreadsheet, 
        true
    );

    bool result = printer.PrintDiagram(diagram);

    EXPECT_TRUE(result);
    EXPECT_EQ(messages.size(), 1);
    EXPECT_EQ(messages[0], "Print to File (Filename=filename, folderValid=true)");
}

TEST_F(PrinterDriverFactoryTest, DoPrintPhysicalDocumentSucceeds) {
    SpyPrintableDiagram* diagramWrapper = new SpyPrintableDiagram(spy, "filename", "PDF", true);
    DiagramMetadataTestAdapter* info = new DiagramMetadataTestAdapter("filename", "Physical", true);
    std::string filename = "random output filename";

    bool result = printer->DoPrint(*diagramWrapper, info, filename);
    
    EXPECT_TRUE(result);
    std::vector<std::string> expected = {
        "Added metadata to print queue",
        "Start Document",
        "DiagramPrintDriver is printing to physical printer",
        "End Document"
    };
    EXPECT_EQ(spy, expected);
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