
#ifndef DIAGRAM_METADATA_H
#define DIAGRAM_METADATA_H

#include <string>
#include "flowchart_diagram.h"

class DiagramMetadata {
public:
    // Constructor
    explicit DiagramMetadata(FlowchartDiagram& diagram);
    
    // Getters
    std::string getFullFilename() const { return m_fullFilename; }
    std::string getFileType() const { return m_fileType; }
    bool isFileAvailable() const { return m_fileAvailable; }

protected:
    // Protected constructor for testing purposes
    DiagramMetadata() = default;

private:
    std::string m_fullFilename;
    std::string m_fileType;
    bool m_fileAvailable;
};

#endif // DIAGRAM_METADATA_H
