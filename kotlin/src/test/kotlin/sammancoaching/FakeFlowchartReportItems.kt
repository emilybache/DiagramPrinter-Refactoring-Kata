package sammancoaching

import java.util.ArrayList
import java.util.Arrays

/**
 * Use this class in your unit tests to replace a real FlowchartReportItems class
 */
class FakeFlowchartReportItems(name: String, serialNumber: String, filename: String) :
    FlowchartReportItems(name, serialNumber, filename) {
    private var _data: MutableList<String?> = ArrayList<String?>()

    init {
        _data = ArrayList<String?>(Arrays.asList<String?>(name, serialNumber, filename))
    }

    override fun add(summaryInformation: String) {
        _data.add(summaryInformation)
    }

    override fun toArray(): Array<Any> {
        return _data.toTypedArray() as Array<Any>
    }
}