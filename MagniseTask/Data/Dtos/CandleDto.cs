namespace MagniseTask.Data.Dtos;

public class CandleDto
{
    public double Open { get; set; }
    public double High { get; set; }
    public double Low { get; set; }
    public double Close { get; set; }
    public double Volume { get; set; }
    public string Time { get; set; }

    public CandleDto (double open, double high, double low, double close, double volume, string time)
    {
        Open = open;
        High = high;
        Low = low;
        Close = close;
        Volume = volume;
        Time = time;
    }
}