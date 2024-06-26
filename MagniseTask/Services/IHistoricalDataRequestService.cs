using MagniseTask.Data.Dtos;

namespace MagniseTask.Services;

public interface IHistoricalDataRequestService
{
    public Task<IEnumerable<CandleDto>> RequestCountBack ( string instrumentId, int interval, string periodicity, int barsCount );
}
