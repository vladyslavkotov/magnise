using MagniseTask.Data.Helpers;

namespace MagniseTask.Services;

public interface IAuthenticationService
{
    public Task<string?> GetTokenAsync(HttpClient httpClient, Settings settings);

}
