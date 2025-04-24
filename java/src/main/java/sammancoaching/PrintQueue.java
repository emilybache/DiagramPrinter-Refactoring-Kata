package sammancoaching;

import java.util.ArrayList;
import java.util.List;

public class PrintQueue {
    private List<PrintMetadata> _items = new ArrayList<PrintMetadata>();
    private PhysicalPrinter _printer;

    public PrintQueue(PhysicalPrinter physicalPrinter) {
        this._printer = physicalPrinter;
    }

    public void add(PrintMetadata data) {
        this._items.add(data);
    }
}
