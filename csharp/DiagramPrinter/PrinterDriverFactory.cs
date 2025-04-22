using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DiagramPrinterTests")]

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
        // for unit tests
        internal set => _instance = value;
    }

    public virtual DiagramPrintDriver CreateDriverForPrint()
    {
        return new DiagramPrintDriver();
    }
}