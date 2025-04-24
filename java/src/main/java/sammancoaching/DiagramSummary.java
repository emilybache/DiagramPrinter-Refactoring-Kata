package sammancoaching;

public class DiagramSummary {
    private final String language;
    private String contents = "";

    public DiagramSummary(String language) {
        this.language = language;
    }

    public void addHeader(String summaryInformation) {
        contents += summaryInformation + "\n";
    }

    public void addTitle(String name, String serialNumber) {
        contents += name + "\n" + serialNumber + "\n";
    }

    public void addImage(PngDocument flowchart) {
        contents += flowchart.getFilename();
    }

    public String export() {
        // Imagine a lot more detail and complexity here
        return contents;
    }
}
