package sammancoaching

enum class Toner {
    Black, Cyan, Magenta, Yellow
}

open class PhysicalPrinter {
    var isAvailable: Boolean = false
    private val jobCount: Int = 0 // Assuming job count is initialized to zero
    protected val tonerLevels: MutableMap<Toner, Int> = mutableMapOf()


    fun getTonerLevelPercentage(toner: Toner): Int {
        return tonerLevels[toner] ?: 0
    }

    fun getJobCount(): Int {
        return jobCount
    }

    open fun startDocument(isSummary: Boolean, isPdf: Boolean, name: String): Boolean {
        throw UnsupportedOperationException("Can't call this from a unit test")
    }

    open fun endDocument() {
        throw UnsupportedOperationException("Can't call this from a unit test")
    }
}