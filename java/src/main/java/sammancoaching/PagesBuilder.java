
package sammancoaching;

import java.util.List;

public class PagesBuilder {
    public DiagramReportPage processPage(DiagramPage page, FlowchartReportItems data) {
        page.addDataFrom(data);
        return new DiagramReportPage(page);
    }

    public boolean apply(DiagramPagesReport report, List<DiagramReportPage> reportPages) {
        if (report.isComplete()) {
            return false;
        }

        for (DiagramReportPage page : reportPages) {
            report.addPage(page);
        }

        return true;
    }
}
