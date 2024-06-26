using Microsoft.EntityFrameworkCore;

namespace MagniseTask.Data.Models;

[Index (nameof (Symbol), IsUnique = true)]
public class Instrument
{
    public string Id { get; set; }
    public string Symbol { get; set; }
    public string Kind { get; set; }
    public string Description { get; set; }
    public double TickSize { get; set; }
    public string Currency { get; set; }
    public string BaseCurrency { get; set; }

    // add ctor to prevent partially initialized entities
    public Instrument (string id, string symbol, string kind, string description, double tickSize, string currency, string baseCurrency)
    {
        Id = id;
        Symbol = symbol;
        Kind = kind;
        Description = description;
        TickSize = tickSize;
        Currency = currency;
        BaseCurrency = baseCurrency;
    }
}