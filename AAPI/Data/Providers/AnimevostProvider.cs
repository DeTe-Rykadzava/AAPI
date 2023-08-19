using AAPI.Models;

namespace AAPI.Data.Providers;

public class AnimevostProvider : IAnimeProvider
{
    public int ProviderId { get; set; }
    public string ProviderName { get; set; }
    public string ProviderIcon { get; set; }
    public async Task<List<LastUpdatesDto>> GetLastUpdate()
    {
        return new List<LastUpdatesDto>();
    }

    public async Task<string> Search()
    {
        return "";
    }
}