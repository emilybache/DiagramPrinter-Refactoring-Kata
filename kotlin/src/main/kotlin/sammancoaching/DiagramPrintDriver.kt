package sammancoaching

open class DiagramPrintDriver {
    private var diagram: FlowchartDiagram? = null

    fun setDiagram(diagram: FlowchartDiagram) {
        this.diagram = diagram
    }

    fun releaseDiagram() {
        this.diagram = null
    }

    open fun printTo(physicalPrinter: PhysicalPrinter): Boolean {
        throw UnsupportedOperationException("Can't call this from a unit test")
    }
}
