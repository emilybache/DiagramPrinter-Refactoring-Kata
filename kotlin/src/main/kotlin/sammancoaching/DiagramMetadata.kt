package sammancoaching

open class DiagramMetadata {
    var fullFilename: String
    var fileType: String
    protected var fileAvailable: Boolean

    constructor(diagram: FlowchartDiagram) {
        this.fullFilename = diagram.name + "_" + diagram.serialNumber
        this.fileType = if (diagram.name.contains("Flowchart")) "PDF" else "Spreadsheet"
        this.fileAvailable = !(diagram.isDisposed)
    }

    protected constructor() {
        // Enable subclassing for testing purposes
        this.fullFilename = ""
        this.fileType = ""
        this.fileAvailable = false
    }
}