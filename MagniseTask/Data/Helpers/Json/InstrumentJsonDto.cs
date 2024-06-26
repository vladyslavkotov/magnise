namespace MagniseTask.Data.Helpers.Json;

public class InstrumentJsonDto
{
    public string id { get; set; }
    public string symbol { get; set; }
    public string kind { get; set; }
    public string description { get; set; }
    public double tickSize { get; set; }
    public string currency { get; set; }
    public string baseCurrency { get; set; }
}