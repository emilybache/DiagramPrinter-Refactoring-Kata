namespace DiagramPrinter;

public class PrinterFactory
{
    private static PrinterFactory _instance;

    public static PrinterFactory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PrinterFactory();
            }
            return _instance;
        }
        private set => _instance = value;
    }

    public Document CreateItemForPrint()
    {
        return new Document();
    }
}