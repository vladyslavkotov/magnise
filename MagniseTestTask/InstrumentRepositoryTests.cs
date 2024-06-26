using MagniseTask.Data;

namespace MagniseTaskTests;

public class InstrumentRepositoryTests
{
    [Theory]
    [InlineData("AUD/CAD", "ad9e5345-4c3b-41fc-9437-1d253f62db52")]
    [InlineData ("AUD/CHF", "e394fa5b-bba1-45fe-b7b9-7e7c0124425b")]
    public async Task GetInstrumentIdAsyncTest (string instrumentSymbol, string expectedInstrumentId)
    {
        using var context = new InstrumentDbContext ();
        var repository = new InstrumentRepository(context);
        var result = await repository.GetInstrumentIdAsync (instrumentSymbol);
        Assert.Equal (expectedInstrumentId, result);
    }
}