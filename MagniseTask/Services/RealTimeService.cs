using MagniseTask.Data;
using MagniseTask.Data.Helpers;
using MagniseTask.Data.Helpers.Json;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace MagniseTask.Services;

public class RealTimeService : IRealTimeService
{
    private readonly IInstrumentRepository _instrumentRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly HttpClient _httpClient;
    private readonly ClientWebSocket _webSocketClient;
    private readonly Settings _settings;
    private int _subscriptionCount = 0;
    private Dictionary<string, Price> _subscriptions;

    public RealTimeService (IConfiguration configuration, IInstrumentRepository instrumentRepository, IHttpClientFactory httpClientFactory, IAuthenticationService authenticationService)
    {
        _instrumentRepository = instrumentRepository;
        _authenticationService = authenticationService;
        _httpClient = httpClientFactory.CreateClient ();
        _webSocketClient = new ClientWebSocket ();
        _settings = new Settings (configuration);
        _subscriptions = new Dictionary<string, Price> ();
    }

    public async Task Subscribe (string symbol, string instrumentId)
    {
        var token = await _authenticationService.GetTokenAsync (_httpClient, _settings);
        var uri = new Uri (_settings.WebSocketsUri + token);
        await _webSocketClient.ConnectAsync (uri, CancellationToken.None);

        await _requestSubscription (instrumentId);
        await _receive (instrumentId);
    }

    private async Task _cancel ()
    {
        await _webSocketClient.CloseAsync (WebSocketCloseStatus.NormalClosure, "Closing connection", CancellationToken.None);
    }

    private async Task _requestSubscription (string instrumentId)
    {
        if ( _webSocketClient.State == WebSocketState.Open )
        {
            var jsonData = JsonSerializer.Serialize (new
            {
                Type = "l1-subscription",
                Id = ( ++_subscriptionCount ).ToString (),
                InstrumentId = instrumentId,
                Provider = "simulation",
                Subscribe = true,
                Kinds = new List<string> { "ask", "bid", "last" }
            });
            var buffer = Encoding.UTF8.GetBytes (jsonData);
            await _webSocketClient.SendAsync (buffer, WebSocketMessageType.Text, true, CancellationToken.None);
            _subscriptions.Add (instrumentId, new Price ());
        }
        else
        {
            throw new HttpRequestException ("Cannot open connection");
        }
    }

    private async Task _receive (string instrumentId)
    {
        while ( _webSocketClient.State == WebSocketState.Open )
        {
            var buffer = new ArraySegment<byte> (new byte[4096]);
            var result = await _webSocketClient.ReceiveAsync (buffer, CancellationToken.None);

            if ( result.MessageType == WebSocketMessageType.Close )
            {
                await _cancel ();
                break;
            }

            var data = Encoding.UTF8.GetString (buffer.Array, 0, result.Count);
            _updatePrice (instrumentId, data);
        }
    }

    private void _updatePrice (string instrumentId, string data)
    {
        var jsonData = JsonSerializer.Deserialize<Dictionary<string, object>> (data);
        if ( jsonData is not null )
        {
            if ( jsonData.TryGetValue ("ask", out var askValue) )
            {
                if ( _subscriptions.TryGetValue (instrumentId, out var price) )
                {
                    var askUpdate = JsonSerializer.Deserialize<PriceJsonDto> (( JsonElement ) askValue);
                    if ( askUpdate != null )
                    {
                        price.UpdateAsk (askUpdate.price, askUpdate.volume, askUpdate.timestamp);
                    }
                }
            }
            else if ( jsonData.TryGetValue ("bid", out var bidValue) )
            {
                if ( _subscriptions.TryGetValue (instrumentId, out var price) )
                {
                    var bidUpdate = JsonSerializer.Deserialize<PriceJsonDto> (( JsonElement ) bidValue);
                    if ( bidUpdate != null )
                    {
                        price.UpdateBid (bidUpdate.price, bidUpdate.volume, bidUpdate.timestamp);
                    }
                }
            }
            else if ( jsonData.TryGetValue ("last", out var lastValue) )
            {
                if ( _subscriptions.TryGetValue (instrumentId, out var price) )
                {
                    var lastUpdate = JsonSerializer.Deserialize<PriceJsonDto> (( JsonElement ) lastValue);
                    if ( lastUpdate != null )
                    {
                        price.UpdateLast (lastUpdate.price, lastUpdate.volume, lastUpdate.timestamp);
                    }
                }
            }
        }
    }
}