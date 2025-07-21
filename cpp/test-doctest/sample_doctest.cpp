#include "ApprovalTests.hpp"
#include "doctest/doctest.h"

#include "diagram_physical_printer.h"
#include "diagram_printer.h"
#include "fake_flowchart_report_items.h"


TEST_SUITE("DiagramPrinter Tests") {
    TEST_CASE("PrintSummary with empty document fails") {
        DiagramPrinter printer;
        std::string output;

        bool result = printer.PrintSummary(nullptr, "swedish", output);

        CHECK_FALSE(result);
        CHECK(output.empty());
    }

    TEST_CASE("PrintDiagram with empty document fails") {
        DiagramPrinter printer;
        FlowchartDiagram *diagram = nullptr;

        bool result = printer.PrintDiagram(diagram, "folder", "filename");

        CHECK_FALSE(result);
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
