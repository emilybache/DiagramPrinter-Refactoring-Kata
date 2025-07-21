#include "ApprovalTests.hpp"
#include "diagram_physical_printer.h"
#include "catch2/catch.hpp"

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


TEST_CASE("DiagramPrinter Tests") {
    SECTION("PrintSummary with empty document fails") {
        DiagramPrinter printer;
        std::string output;
        
        bool result = printer.PrintSummary(nullptr, "swedish", output);
        
        REQUIRE_FALSE(result);
        REQUIRE(output.empty());
    }

    SECTION("PrintDiagram") {
        SECTION("PrintDiagram with empty document fails") {
            DiagramPrinter printer;
            FlowchartDiagram *diagram = nullptr;
        
            bool result = printer.PrintDiagram(diagram, "folder", "filename");
        
            REQUIRE_FALSE(result);
        }

        SECTION("PrintDiagram PDF document succeeds") {
            std::vector<std::string> messages;
            DiagramPrinter printer;
            auto diagram = new SpyPrintableDiagram(
                messages, 
                "filename", 
                DiagramPrinter::Pdf, 
                true
            );
    
            bool result = printer.PrintDiagram(diagram);
    
            REQUIRE(result);
            REQUIRE(messages.size() == 1);
            REQUIRE(messages[0] == "Print to File (Filename=filename, folderValid=true)");
        }

        SECTION("PrintDiagram Spreadsheet document succeeds") {
            std::vector<std::string> messages;
            DiagramPrinter printer;
            auto diagram = new SpyPrintableDiagram(
                messages, 
                "filename", 
                DiagramPrinter::Spreadsheet, 
                true
            );
    
            bool result = printer.PrintDiagram(diagram);
    
            REQUIRE(result);
            REQUIRE(messages.size() == 1);
            REQUIRE(messages[0] == "Print to File (Filename=filename, folderValid=true)");
        }

        SECTION("DoPrint Physical Document Succeeds") {
            PrinterDriverFactoryTest* testFixture = new PrinterDriverFactoryTest();
            SpyPrintableDiagram* diagramWrapper = new SpyPrintableDiagram(testFixture->spy, "filename", "PDF", true);
            DiagramMetadataTestAdapter* info = new DiagramMetadataTestAdapter("filename", "Physical", true);
            std::string filename = "random output filename";
    
            bool result = testFixture->printer->DoPrint(*diagramWrapper, info, filename);
            
            REQUIRE(result);
            auto expected = std::vector<std::string> {
                "Added metadata to print queue",
                "Start Document",
                "DiagramPrintDriver is printing to physical printer",
                "End Document"};
            REQUIRE(testFixture->spy == expected);
        }

    }
    
    SECTION("PrintReport with empty document fails") {
        DiagramPrinter printer;
        std::string templ = "Report for FlowchartDiagram {0} {1} {2}";
        auto fakeFlowchartReportItems = FakeFlowchartReportItems("DiagramName", "Serial Number", "Filename");
    
        bool result = printer.PrintReport(nullptr, templ);
    
        REQUIRE_FALSE(result);
    }

    SECTION("PrintPages with empty document fails") {
        DiagramPrinter printer;
        PagesBuilder builder;
    
        bool result = printer.PrintPages(nullptr, builder);
    
        REQUIRE_FALSE(result);
    }

    SECTION("ValidateReport with matching template succeeds") {
        DiagramPrinter printer;
        std::string templ = "Report for FlowchartDiagram {0} {1} {2}";
        auto fakeFlowchartReportItems = FakeFlowchartReportItems("DiagramName", "Serial Number", "Filename");
    
        bool result = printer.ValidateReport(templ, &fakeFlowchartReportItems);
    
        REQUIRE(result);
    }

}



