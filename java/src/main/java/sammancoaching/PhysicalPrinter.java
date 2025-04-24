package sammancoaching;

public class PhysicalPrinter {
    protected boolean isAvailable;
    private final int jobCount;

    public PhysicalPrinter() {
        this.jobCount = 0; // Assuming job count is initialized to zero
    }

    public boolean isAvailable() {
        return isAvailable;
    }

    public int getJobCount() {
        return jobCount;
    }

    public boolean startDocument(boolean isSummary, boolean isPdf, String name) {
        throw new UnsupportedOperationException("Can't call this from a unit test");
    }

    public void endDocument() {
        throw new UnsupportedOperationException("Can't call this from a unit test");
    }
}

