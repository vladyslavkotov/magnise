using Microsoft.EntityFrameworkCore;
using MagniseTask.Data.Models;

namespace MagniseTask.Data;

public class InstrumentRepository : IInstrumentRepository
{
    private readonly InstrumentDbContext _context;

    public InstrumentRepository ( InstrumentDbContext context )
    {
        _context = context;
    }

    public async Task<string?> GetInstrumentIdAsync ( string instrumentSymbol )
    {
        var instrument = await _context.Instruments.SingleOrDefaultAsync ( x => x.Symbol == instrumentSymbol );
        return instrument?.Id;
    }

    public async Task<IEnumerable<Instrument>> GetAllInstruments ()
    {
        return await _context.Instruments.ToListAsync ();
    }
}