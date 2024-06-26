using MagniseTask.Data.Helpers;

namespace MagniseTask.Services;

public interface IRealTimeService
{
    public  Task<RealTimeQuoteDto> GetRealTimeQuote (string symbol, string instrumentId);
}