package sammancoaching

open class FlowchartReportItems(name: String, serialNumber: String, filename: String) {
    // this implementation is deliberately blank - can't use this class in a unit test

    open fun add(summaryInformation: String) {
        throw UnsupportedOperationException("can't call from a unit test")
    }

    open fun toArray(): Array<Any> {
        throw UnsupportedOperationException("can't call from a unit test")
    }
}