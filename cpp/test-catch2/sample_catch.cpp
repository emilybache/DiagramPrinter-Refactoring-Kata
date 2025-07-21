#include "ApprovalTests.hpp"
#include "diagram_physical_printer.h"
#include "catch2/catch.hpp"

#include "diagram_printer.h"
#include "fake_flowchart_report_items.h"


TEST_CASE("DiagramPrinter Tests") {
    SECTION("PrintSummary with empty document fails") {
        DiagramPrinter printer;
        std::string output;

        bool result = printer.PrintSummary(nullptr, "swedish", output);

        REQUIRE_FALSE(result);
        REQUIRE(output.empty());
    }

    SECTION("PrintDiagram with empty document fails") {
        DiagramPrinter printer;
        FlowchartDiagram *diagram = nullptr;

        bool result = printer.PrintDiagram(diagram, "folder", "filename");

        REQUIRE_FALSE(result);
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
