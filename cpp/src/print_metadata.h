#ifndef PRINT_METADATA_H
#define PRINT_METADATA_H

#include <string>
#include <filesystem>

class PrintMetadata {
private:
    std::string _infoFileType;
    std::string _filename;

public:
    explicit PrintMetadata(const std::string& infoFileType)
        : _infoFileType(infoFileType)
    {
        // Create temporary file with unique name
        _filename = std::filesystem::temp_directory_path() / "temp_filename";
        
        // Change extension to match the info file type
        _filename = _filename.substr(0, _filename.find_last_of('.') + 1) + _infoFileType;
    }

    const std::string& GetFilename() const { return _filename; }
};

#endif // PRINT_METADATA_H
