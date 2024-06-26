namespace MagniseTask.Services;

public interface IRealTimeService
{
    public Task Subscribe (string symbol, string instrumentId);
}