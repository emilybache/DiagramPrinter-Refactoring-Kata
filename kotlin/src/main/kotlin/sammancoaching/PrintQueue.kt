package sammancoaching

open class PrintQueue(private val printer: PhysicalPrinter) {
    private val items: MutableList<PrintMetadata> = mutableListOf()

    open fun add(data: PrintMetadata) {
        items.add(data)
    }
}