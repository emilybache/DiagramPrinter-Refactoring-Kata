namespace DiagramPrinter;

public class PrinterDriverFactory
{
    private static PrinterDriverFactory _instance;

    public static PrinterDriverFactory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PrinterDriverFactory();
            }
            return _instance;
        }
        private set => _instance = value;
    }

    public Document CreateDriverForPrint()
    {
        return new Document();
    }
}