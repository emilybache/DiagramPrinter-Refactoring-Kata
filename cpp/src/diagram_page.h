#ifndef DIAGRAM_PAGE_H
#define DIAGRAM_PAGE_H

#include <string>
#include <format>

#include "flowchart_report_items.h"


class DiagramPage {
public:
    DiagramPage(const std::string& name, const std::string& pageContents)
        : m_name(name), m_pageContents(pageContents) {
    }

    const std::string& Name() const { return m_name; }
    std::string PageContents() const { return m_pageContents; }

    void AddDataFrom(const FlowchartReportItems& data) {
        try {
            if (m_pageContents.find("{0}") != std::string::npos) {
                std::vector<std::string> values = data.ToArray();
                auto value = format_dynamic(m_pageContents, values);
            }
        }
        catch (const std::format_error& e) {
        }
    }

    std::string format_dynamic(std::string fmt, const std::vector<std::string>& values) {
        for (size_t i = 0; i < values.size(); ++i) {
            std::string placeholder = "{" + std::to_string(i) + "}";
            size_t pos = fmt.find(placeholder);
            if (pos != std::string::npos) {
                fmt.replace(pos, placeholder.length(), values[i]);
            }
        }
        return fmt;
    }


private:
    const std::string m_name;
    const std::string m_pageContents;
};

#endif // DIAGRAM_PAGE_H
