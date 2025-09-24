package sammancoaching

import java.io.IOException
import java.util.concurrent.locks.ReentrantLock
import java.util.logging.Logger


/**
 * This is a class you'd like to get under test so you can change it safely.
 */
class DiagramPhysicalPrinter {
    private val physicalPrinter: PhysicalPrinter
    private val printQueue: PrintQueue

    constructor(physicalPrinter: PhysicalPrinter, printQueue: PrintQueue) {
        this.physicalPrinter = physicalPrinter
        this.printQueue = printQueue
    }

    constructor() {
        this.physicalPrinter = PhysicalPrinter()
        this.printQueue = PrintQueue(this.physicalPrinter)
    }

    @Throws(IOException::class)
    fun doPrint(diagram: PrintableDiagram, info: DiagramMetadata, targetFilename: String): Boolean {
        val factory = PrinterDriverFactory.getInstance()
        val printerDriver = factory.createDriverForPrint()
        diagram.getDiagram()?.let { printerDriver.setDiagram(it) }

        val data = PrintMetadata(info.fileType)
        var success = false

        try {
            mutex.lock()

            if (!physicalPrinter.isAvailable || !(physicalPrinter.getTonerLevelPercentage(Toner.Black) > 0
                        && physicalPrinter.getTonerLevelPercentage(Toner.Cyan) > 0
                        && physicalPrinter.getTonerLevelPercentage(Toner.Magenta) > 0
                        && physicalPrinter.getTonerLevelPercentage(Toner.Yellow) > 0
                        )
            ) {
                logger.info("Physical Printer Unavailable")
            } else if (physicalPrinter.getJobCount() < 0) {
                logger.info("Physical Printer Unavailable Due to Job Count Inconsistency")
            } else {
                // Print the diagram using the Physical Printer
                printQueue.add(data)
                val summaryInformation = diagram.getSummaryInformation()
                logger.info("Diagram Summary Information: " + summaryInformation)
                val isSummary = summaryInformation.length > 10

                if (physicalPrinter.startDocument(!isSummary, false, "DiagramPhysicalPrinter")) {
                    if (printerDriver.printTo(physicalPrinter)) {
                        logger.info("Physical Printer Successfully printed")
                        success = true
                    }

                    physicalPrinter.endDocument()
                }
            }

            if (success) {
                // Save a backup of the printed document as PDF
                val file = data.getFile()
                if (file.exists()) {
                    logger.info("Saving backup of printed document as PDF to file " + targetFilename)
                    diagram.printToFile(data.getFilename(), targetFilename)
                }
            }
        } catch (e: Exception) {
            logger.severe("Failed to print document: " + e.message)
            success = false
        } finally {
            mutex.unlock()
            printerDriver.releaseDiagram()
        }

        return success
    }

    companion object {
        private val logger: Logger = Logger.getLogger(DiagramPhysicalPrinter::class.java.getName())
        private val mutex = ReentrantLock()
    }
}

