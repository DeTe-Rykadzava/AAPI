using AAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AAPI.Data;

public interface IAnimeProvider
{
    public int ProviderId { get; set; }
    public string ProviderName { get; set; }
    public string ProviderIconPath { get; set; }
    public Task<Stream> GetProviderIcon();
    public Task<List<AnimeTitleDto>> GetLastUpdate();
    public Task<AnimeTitleDto> Search(string search);
}