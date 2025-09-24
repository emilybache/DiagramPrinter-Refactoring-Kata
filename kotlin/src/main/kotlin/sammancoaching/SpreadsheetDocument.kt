package sammancoaching

/**
 * This is the real production code class.
 * The implementation is omitted here, imagine it is from a third party library
 * which you can't change.
 */
class SpreadsheetDocument constructor() {

    fun copyFile(infoFullFilename: String, targetFilename: String, overwrite: Boolean): Boolean {
        throw RuntimeException("cannot be called from a unit test")
    }
}