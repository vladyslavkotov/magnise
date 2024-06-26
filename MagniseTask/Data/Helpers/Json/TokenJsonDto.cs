namespace MagniseTask.Data.Helpers.Json;

public class TokenJsonDto
{
    public string access_token { get; set; }
    public double expires_in { get; set; }
    public double refresh_expires_in { get; set; }
    public string refresh_token { get; set; }
    public string token_type { get; set; }
}