namespace MagniseTask.Data.Helpers;

public class Price
{
    public double AskPrice { get; set; } = 0;
    public double BidPrice { get; set; } = 0;
    public double LastPrice { get; set; } = 0;

    public double AskVolume { get; set; } = 0;
    public double BidVolume { get; set; } = 0;
    public double LastVolume { get; set; } = 0;

    public DateTime AskUpd { get; set; } = DateTime.MinValue;
    public DateTime BidUpd { get; set; } = DateTime.MinValue;
    public DateTime LastUpd { get; set; } = DateTime.MinValue;

    public void UpdateAsk (double price, double volume, string upd)
    {
        AskPrice = price;
        AskVolume = volume;
        AskUpd = DateTime.Parse (upd, null, System.Globalization.DateTimeStyles.RoundtripKind);
    }

    public void UpdateBid (double price, double volume, string upd)
    {
        BidPrice = price;
        BidVolume = volume;
        BidUpd = DateTime.Parse (upd, null, System.Globalization.DateTimeStyles.RoundtripKind);
    }

    public void UpdateLast (double price, double volume, string upd)
    {
        LastPrice = price;
        LastVolume = volume;
        LastUpd = DateTime.Parse (upd, null, System.Globalization.DateTimeStyles.RoundtripKind);
    }
}