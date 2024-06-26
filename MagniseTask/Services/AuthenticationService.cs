using MagniseTask.Data.Helpers;
using MagniseTask.Data.Helpers.Json;
using System.Text.Json;

namespace MagniseTask.Services;

public class AuthenticationService : IAuthenticationService
{
    private string? _token = null;
    private DateTime _tokenExpiration;

    public async Task<string?> GetTokenAsync (HttpClient httpClient, Settings settings)
    {
        if ( string.IsNullOrEmpty (_token) || DateTime.UtcNow >= _tokenExpiration )
        {
            await RefreshTokenAsync (httpClient, settings);
        }

        return _token;
    }

    private async Task RefreshTokenAsync (HttpClient httpClient, Settings settings)
    {
        var values = new Dictionary<string, string>
          {
            { "grant_type", "password" },
            { "client_id", "app-cli" },
            { "username", settings.Username },
            { "password", settings.Password }
          };

        var content = new FormUrlEncodedContent (values);
        var uri = new Uri (settings.BaseUri + settings.GetTokenEndpoint);
        var response = await httpClient.PostAsync (uri, content);

        if ( response.IsSuccessStatusCode )
        {
            var responseString = await response.Content.ReadAsStringAsync ();
            var tokenInfo = JsonSerializer.Deserialize<TokenJsonDto> (responseString);
            if ( tokenInfo is not null )
            {
                _token = tokenInfo.access_token;
                _tokenExpiration = DateTime.UtcNow.AddSeconds (tokenInfo.expires_in - 1);
            }
        }
        else
        {
            throw new HttpRequestException ($"Failed to fetch data. Status code: {response.StatusCode}");
        }
    }
}