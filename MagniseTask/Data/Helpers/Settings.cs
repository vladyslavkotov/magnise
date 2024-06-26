namespace MagniseTask.Data.Helpers;

public class Settings
{
    public string BaseUri { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string GetTokenEndpoint { get; set; }
    public string HistDataCountBackEndpoint { get; set; }
    public string HistDataDateRangeEndpoint { get; set; }
    public string HistDataTimeBackEndpoint { get; set; }
    public string WebSocketsUri { get; set; }

    public Settings(IConfiguration configuration)
    {
        BaseUri = configuration.GetValue<string>("ApiSettings:BaseUri");
        Username = configuration.GetValue<string>("ApiSettings:Username");
        Password = configuration.GetValue<string>("ApiSettings:Password");
        GetTokenEndpoint = configuration.GetValue<string>("ApiSettings:GetTokenEndpoint");
        HistDataCountBackEndpoint = configuration.GetValue<string>("ApiSettings:HistDataCountBackEndpoint");
        HistDataDateRangeEndpoint = configuration.GetValue<string>("ApiSettings:HistDataDateRangeEndpoint");
        HistDataTimeBackEndpoint = configuration.GetValue<string>("ApiSettings:HistDataTimeBackEndpoint");
        WebSocketsUri = configuration.GetValue<string>( "ApiSettings:WebSocketsUri" );
    }
}