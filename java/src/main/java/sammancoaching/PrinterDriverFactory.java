package sammancoaching;

public class PrinterDriverFactory {
    private static PrinterDriverFactory instance;

    public static PrinterDriverFactory getInstance() {
        if (instance == null) {
            instance = new PrinterDriverFactory();
        }
        return instance;
    }

    // For unit tests
    static void setInstance(PrinterDriverFactory newInstance) {
        instance = newInstance;
    }

    public DiagramPrintDriver createDriverForPrint() {
        return new DiagramPrintDriver();
    }
}
