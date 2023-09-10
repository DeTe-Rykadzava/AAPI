using AAPI.Models;

namespace AAPI.Data.Providers;

public class AnimevostProvider : IAnimeProvider
{
    public int ProviderId { get; set; }
    public string ProviderName { get; set; }
    public string ProviderIcon { get; set; }
    public async Task<List<AnimeTitleDto>> GetLastUpdate()
    {
        return new List<AnimeTitleDto>();
    }

    public async Task<AnimeTitleDto> Search(string search)
    {
        return new AnimeTitleDto();
    }
}