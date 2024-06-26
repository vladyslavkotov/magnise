using MagniseTask.Data;
using MagniseTask.Data.Dtos;
using MagniseTask.Data.Helpers;
using MagniseTask.Data.Helpers.Json;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MagniseTask.Services;

public class HistoricalDataRequestService : IHistoricalDataRequestService
{
    private readonly IInstrumentRepository _instrumentRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly HttpClient _httpClient;
    private readonly Settings _settings;

    public HistoricalDataRequestService (IConfiguration configuration, IInstrumentRepository instrumentRepository, IHttpClientFactory httpClientFactory, IAuthenticationService authenticationService)
    {
        _instrumentRepository = instrumentRepository;
        _authenticationService = authenticationService;
        _httpClient = httpClientFactory.CreateClient ();
        _settings = new Settings (configuration);
    }

    public async Task<IEnumerable<CandleDto>> RequestCountBack (string instrumentId, int interval, string periodicity, int barsCount)
    {
        var uri = new Uri (string.Format (_settings.BaseUri + _settings.HistDataCountBackEndpoint, instrumentId, interval, periodicity, barsCount));
        var result = await _getHistCandles (uri);
        return result;
    }

    private async Task<IEnumerable<CandleDto>> _getHistCandles (Uri uri)
    {
        var token = await _authenticationService.GetTokenAsync (_httpClient, _settings);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue ("Bearer", token);

        var response = await _httpClient.GetAsync (uri);
        var candles = new List<CandleDto> ();
        if ( response.IsSuccessStatusCode )
        {
            string responseContent = await response.Content.ReadAsStringAsync ();
            var jsonData = JsonSerializer.Deserialize<Dictionary<string, object>> (responseContent);

            if ( jsonData is not null && jsonData.TryGetValue ("data", out var value) )
            {
                candles = JsonSerializer.Deserialize<List<CandleJsonDto>> (( JsonElement ) value).Select (x => new CandleDto (x.o, x.h, x.l, x.c, x.v, x.t)).ToList ();
            }
            return candles;
        }
        else
        {
            throw new HttpRequestException ($"Failed to fetch data. Status code: {response.StatusCode}");
        }
    }
}