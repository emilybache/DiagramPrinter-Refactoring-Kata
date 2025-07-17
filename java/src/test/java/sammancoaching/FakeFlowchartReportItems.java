
package sammancoaching;

import java.util.ArrayList;
import java.util.List;
import java.util.Arrays;

/**
 * Use this class in your unit tests to replace a real FlowchartReportItems class
 */
public class FakeFlowchartReportItems extends FlowchartReportItems {
    private List<String> _data = new ArrayList<>();

    public FakeFlowchartReportItems(String name, String serialNumber, String filename) {
        super(name, serialNumber, filename);
        _data = new ArrayList<>(Arrays.asList(name, serialNumber, filename));
    }

    @Override
    public void add(String summaryInformation) {
        _data.add(summaryInformation);
    }

    @Override
    public Object[] toArray() {
        return _data.toArray();
    }
}
