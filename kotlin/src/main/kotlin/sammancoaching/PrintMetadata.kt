package sammancoaching

import java.io.File
import java.io.IOException

class PrintMetadata(private val infoFileType: String) {
    private val tempFile: File

    init {
        tempFile = File.createTempFile("filename", ".$infoFileType")
    }

    fun getFile(): File {
        return tempFile
    }

    fun getFilename(): String {
        return tempFile.name
    }
}