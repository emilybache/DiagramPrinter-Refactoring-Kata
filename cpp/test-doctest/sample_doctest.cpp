#include "ApprovalTests.hpp"
#include "doctest/doctest.h"

#include "diagram_physical_printer.h"
#include "diagram_printer.h"
#include "fake_flowchart_report_items.h"
#include "spies.h"

class PrinterDriverFactoryTest {
public:
    std::vector<std::string> spy;
    SpyPhysicalPrinter* spyPrinter;
    SpyQueue* spyQueue;
    DiagramPhysicalPrinter* printer;
    PrinterDriverFactory* factory;
    
    PrinterDriverFactoryTest() {
        spyPrinter = new SpyPhysicalPrinter(spy);
        spyQueue = new SpyQueue(spy, *spyPrinter);
        printer = new DiagramPhysicalPrinter(spyPrinter, spyQueue);
        factory = new PrinterDriverFactoryTestAdapter(spy);
        PrinterDriverFactory::SetInstance(factory);
    }
    
    ~PrinterDriverFactoryTest() {
        delete printer;
        delete spyQueue;
        delete spyPrinter;
        PrinterDriverFactory::ResetInstance();
    }
};

TEST_SUITE("DiagramPrinter Tests") {
    TEST_CASE("PrintSummary with empty document fails") {
        DiagramPrinter printer;
        std::string output;
        
        bool result = printer.PrintSummary(nullptr, "swedish", output);
        
        CHECK_FALSE(result);
        CHECK(output.empty());
    }

    TEST_SUITE("PrintDiagram") {
        TEST_CASE("PrintDiagram with empty document fails") {
            DiagramPrinter printer;
            FlowchartDiagram *diagram = nullptr;
        
            bool result = printer.PrintDiagram(diagram, "folder", "filename");
        
            CHECK_FALSE(result);
        }

        TEST_CASE("PrintDiagram PDF document succeeds") {
            std::vector<std::string> messages;
            DiagramPrinter printer;
            auto diagram = new SpyPrintableDiagram(
                messages, 
                "filename", 
                DiagramPrinter::Pdf, 
                true
            );
    
            bool result = printer.PrintDiagram(diagram);
    
            CHECK(result);
            CHECK_EQ(messages.size(), 1);
            CHECK_EQ(messages[0], "Print to File (Filename=filename, folderValid=true)");
        }

        TEST_CASE("PrintDiagram Spreadsheet document succeeds") {
            std::vector<std::string> messages;
            DiagramPrinter printer;
            auto diagram = new SpyPrintableDiagram(
                messages, 
                "filename", 
                DiagramPrinter::Spreadsheet, 
                true
            );
    
            bool result = printer.PrintDiagram(diagram);
    
            CHECK(result);
            CHECK_EQ(messages.size(), 1);
            CHECK_EQ(messages[0], "Print to File (Filename=filename, folderValid=true)");
        }

        TEST_CASE("DoPrint Physical Document Succeeds") {
            PrinterDriverFactoryTest* testFixture = new PrinterDriverFactoryTest();
            SpyPrintableDiagram* diagramWrapper = new SpyPrintableDiagram(testFixture->spy, "filename", "PDF", true);
            DiagramMetadataTestAdapter* info = new DiagramMetadataTestAdapter("filename", "Physical", true);
            std::string filename = "random output filename";
    
            bool result = testFixture->printer->DoPrint(*diagramWrapper, info, filename);
            
            CHECK(result);
            std::vector<std::string> expected = {
                "Added metadata to print queue",
                "Start Document",
                "DiagramPrintDriver is printing to physical printer",
                "End Document"
            };
            CHECK_EQ(testFixture->spy, expected);
        }
    }
    
    TEST_CASE("PrintReport with empty document fails") {
        DiagramPrinter printer;
        std::string templ = "Report for FlowchartDiagram {0} {1} {2}";
        auto fakeFlowchartReportItems = FakeFlowchartReportItems("DiagramName", "Serial Number", "Filename");
    
        bool result = printer.PrintReport(nullptr, templ);
    
        CHECK_FALSE(result);
    }

    TEST_CASE("PrintPages with empty document fails") {
        DiagramPrinter printer;
        PagesBuilder builder;
    
        bool result = printer.PrintPages(nullptr, builder);
    
        CHECK_FALSE(result);
    }

    TEST_CASE("ValidateReport with matching template succeeds") {
        DiagramPrinter printer;
        std::string templ = "Report for FlowchartDiagram {0} {1} {2}";
        auto fakeFlowchartReportItems = FakeFlowchartReportItems("DiagramName", "Serial Number", "Filename");
    
        bool result = printer.ValidateReport(templ, &fakeFlowchartReportItems);
    
        CHECK(result);
    }
}


