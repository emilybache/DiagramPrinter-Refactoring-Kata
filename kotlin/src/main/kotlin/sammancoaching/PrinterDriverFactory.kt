package sammancoaching

open class PrinterDriverFactory() {
    companion object {
        @Volatile
        private var instance: PrinterDriverFactory? = null

        fun getInstance(): PrinterDriverFactory {
            return instance ?: synchronized(this) {
                instance ?: PrinterDriverFactory().also { instance = it }
            }
        }

        // For unit tests
        internal fun setInstance(newInstance: PrinterDriverFactory) {
            instance = newInstance
        }
    }

    open fun createDriverForPrint(): DiagramPrintDriver {
        return DiagramPrintDriver()
    }
}