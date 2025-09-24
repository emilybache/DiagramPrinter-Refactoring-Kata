package sammancoaching

/**
 * This is the real production code class.
 * The implementation is omitted here, imagine it is from a third party library
 * which you can't change.
 */
class PdfDocument private constructor() {

    fun copyFile(fromPath: String, targetPath: String, failIfAlreadyExists: Boolean): Boolean {
        throw UnsupportedOperationException("Can't call this from a unit test")
    }
}