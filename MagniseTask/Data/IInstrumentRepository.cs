using MagniseTask.Data.Models;

namespace MagniseTask.Data;

public interface IInstrumentRepository
{
    public Task<string?> GetInstrumentIdAsync(string instrumentSymbol);

    public Task<IEnumerable<Instrument>> GetAllInstruments();
}