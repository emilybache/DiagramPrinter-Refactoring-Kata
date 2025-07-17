package sammancoaching;


import java.util.IllegalFormatException;

public class DiagramPage {
    private final String name;
    private String pageContents;

    public DiagramPage(String name, String pageContents) {
        this.name = name;
        this.pageContents = pageContents;
    }

    public String getName() {
        return name;
    }

    public String getPageContents() {
        return pageContents;
    }

    public void addDataFrom(FlowchartReportItems data) {
        try {
            if (pageContents.contains("{0}")) {
                pageContents = String.format(pageContents, data.toArray());
            }
        } catch (IllegalFormatException e) {

        }
    }
}
