package sammancoaching;

import java.util.ArrayList;
import java.util.List;

public class DiagramPagesReport {
    private List<DiagramReportPage> pages;

    public DiagramPagesReport() {
        this.pages = new ArrayList<>();
    }

    public void addPage(DiagramReportPage page) {
        this.pages.add(page);
    }

    public boolean isComplete() {
        return !pages.isEmpty();
    }
}