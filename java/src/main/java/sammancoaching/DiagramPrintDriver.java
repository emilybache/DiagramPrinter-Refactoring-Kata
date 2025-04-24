package sammancoaching;

public class DiagramPrintDriver {
    private FlowchartDiagram diagram;

    public void setDiagram(FlowchartDiagram diagram) {
        this.diagram = diagram;
    }

    public void releaseDiagram() {
        this.diagram = null;
    }

    public boolean printTo(PhysicalPrinter physicalPrinter) {
        throw new UnsupportedOperationException("Can't call this from a unit test");
    }
}
