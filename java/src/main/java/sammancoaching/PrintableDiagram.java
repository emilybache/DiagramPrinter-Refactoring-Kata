package sammancoaching;

public class PrintableDiagram {

    private final FlowchartDiagram diagram;

    public PrintableDiagram(FlowchartDiagram diagram) {
        this.diagram = diagram;
    }

    public FlowchartDiagram getDiagram() {
        return diagram;
    }

    public DiagramMetadata getDiagramMetadata() {
        return new DiagramMetadata(diagram);
    }

    public boolean printToFile(String fromFilename, String targetFilename) {
        return diagram.getFlowchartAsPdf().copyFile(fromFilename, targetFilename, true);
    }

    public String getSummaryInformation() {
        return diagram.getSummaryInformation();
    }
}
