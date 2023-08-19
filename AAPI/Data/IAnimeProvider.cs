using AAPI.Models;

namespace AAPI.Data;

public interface IAnimeProvider
{
    public int ProviderId { get; set; }
    public string ProviderName { get; set; }
    public string ProviderIcon { get; set; }
    public Task<List<LastUpdatesDto>> GetLastUpdate();
    public Task<string> Search();
}