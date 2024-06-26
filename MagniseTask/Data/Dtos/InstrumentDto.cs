namespace MagniseTask.Data.Models;

public class InstrumentDto
{
    public string Symbol { get; set; }
    public string Kind { get; set; }
    public string Description { get; set; }
    public double TickSize { get; set; }
    public string Currency { get; set; }
    public string BaseCurrency { get; set; }

    // add ctor to prevent partially initialized entities
    public InstrumentDto(string symbol, string kind, string description, double tickSize, string currency, string baseCurrency)
    {
        Symbol=symbol;
        Kind=kind;
        Description=description;
        TickSize=tickSize;
        Currency=currency;
        BaseCurrency=baseCurrency;
    }
}