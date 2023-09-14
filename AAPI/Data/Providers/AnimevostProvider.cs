using System.Reflection;
using System.Text.Json.Serialization;
using AAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AAPI.Data.Providers;

public class AnimevostProvider : IAnimeProvider
{
    public int ProviderId { get; set; }
    public string ProviderName { get; set; }

    [JsonIgnore]
    public string ProviderIconPath { get; set; }

    public AnimevostProvider(int id, string Name, string iconAssemblyPath)
    {
        ProviderId = id;
        ProviderName = Name;
        ProviderIconPath = iconAssemblyPath;
    }

    public async Task<Stream> GetProviderIcon()
    {
        // Determine path
        var assembly = Assembly.GetExecutingAssembly();
        string resourcePath = assembly.GetManifestResourceNames()
            .Single(str => str.EndsWith(ProviderIconPath));
        
        Stream stream = assembly.GetManifestResourceStream(resourcePath);

        return stream;
    }

    public async Task<List<AnimeTitleDto>> GetLastUpdate()
    {
        return new List<AnimeTitleDto>();
    }

    public async Task<AnimeTitleDto> Search(string search)
    {
        return new AnimeTitleDto();
    }
}