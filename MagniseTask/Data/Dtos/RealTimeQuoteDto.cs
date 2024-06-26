namespace MagniseTask.Data.Helpers;

public class RealTimeQuoteDto
{
    public double AskPrice { get; set; }
    public double BidPrice { get; set; }
    public double LastPrice { get; set; }

    public double AskVolume { get; set; }
    public double BidVolume { get; set; }
    public double LastVolume { get; set; }

    public string AskUpd { get; set; }
    public string BidUpd { get; set; }
    public string LastUpd { get; set; }

    public RealTimeQuoteDto (double askPrice, double bidPrice, double lastPrice, double askVolume, double bidVolume, double lastVolume, DateTime askUpd, DateTime bidUpd, DateTime lastUpd)
    {
        AskPrice = askPrice;
        BidPrice = bidPrice;
        LastPrice = lastPrice;
        AskVolume = askVolume;
        BidVolume = bidVolume;
        LastVolume = lastVolume;
        AskUpd = askUpd.ToString ();
        BidUpd = bidUpd.ToString ();
        LastUpd = lastUpd.ToString ();
    }
}