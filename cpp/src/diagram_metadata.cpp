#include "diagram_metadata.h"
#include <algorithm>

DiagramMetadata::DiagramMetadata(FlowchartDiagram& diagram) {
    m_fullFilename = diagram.Name() + "_" + diagram.SerialNumber();
    
    // Check if "Flowchart" is in the name
    std::string name = diagram.Name();
    if (name.find("Flowchart") != std::string::npos) {
        m_fileType = "PDF";
    } else {
        m_fileType = "Spreadsheet";
    }
    
    m_fileAvailable = !diagram.IsDisposed();
}
