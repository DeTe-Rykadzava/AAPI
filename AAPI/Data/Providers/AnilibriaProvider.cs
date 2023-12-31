using System.Reflection;
using System.Resources;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using AAPI.Config;
using AAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace AAPI.Data.Providers;

public class AnilibriaProvider : IAnimeProvider
{
    public int ProviderId { get; set; }
    public string ProviderName { get; set; }

    [JsonIgnore]
    public string ProviderIconPath { get; set; }

    public AnilibriaProvider(int id, string Name, string iconAssemblyPath)
    {
        ProviderId = id;
        ProviderName = Name;
        ProviderIconPath = iconAssemblyPath;
    }
    

    // provider settings 
    private readonly string _host = "https://api.anilibria.tv/v3";
    private readonly string _postersHost = "https://anilibria.tv";
    private readonly string _videoHost = "https://cache.libria.fun";

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
        var response = await WebClientConfig.WebClient.GetAsync($"{_host}/title/updates?limit=30&playlist_type=array&filter=code,names,franchises[*].releases[*].code,franchises[*].releases[*].names,posters.medium.url,type.full_string,genres,season.year,description,player.host,player.episodes.string,player.list[*].name,player.list[*].preview,player.list[*].hls.sd,player.list[*].hls.hd");

        if (!response.IsSuccessStatusCode)
            throw new Exception("Problem with getting data");

        var lastUpdatesJsonObject = JsonNode.Parse(await response.Content.ReadAsStreamAsync());
        var lastUpdatesList = lastUpdatesJsonObject?["list"]?.AsArray();

        if (lastUpdatesList == null)
            throw new Exception("Problem with parse data");

        var lastUpdates = lastUpdatesList.Deserialize<List<AnimeTitleDto>>();

        if (lastUpdates != null)
        {
            foreach (var lastUpdate in lastUpdates)
            {
                lastUpdate.posters.medium.url = _postersHost + lastUpdate.posters.medium.url;
                foreach (var seria in lastUpdate.player.list)
                {
                    seria.preview = _postersHost + seria.preview;
                    seria.hls.hd = _videoHost + seria.hls.hd;
                    seria.hls.sd = _videoHost + seria.hls.sd;
                }
            }

            return lastUpdates;
        }

        throw new Exception("Do not have data");
    }

    public async Task<AnimeTitleDto> Search(string search)
    {
        return new AnimeTitleDto();
    }
}