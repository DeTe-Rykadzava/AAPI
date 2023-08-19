using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Unicode;
using AAPI.Config;
using AAPI.Models;

namespace AAPI.Data.Providers;

public class AnilibriaProvider : IAnimeProvider
{
    public int ProviderId { get; set; }
    public string ProviderName { get; set; }
    public string ProviderIcon { get; set; }

    // provider settings 
    private string host = "https://api.anilibria.tv/v3/";
    private string postershost = "https://anilibria.tv/";
    private string videohost = "https://cache.libria.fun";

    public async Task<List<LastUpdatesDto>> GetLastUpdate()
    {
        var response = await WebClientConfig.WebClient.GetAsync("https://api.anilibria.tv/v3/title/updates?limit=30&playlist_type=array&filter=code,names,franchises[*].releases[*].code,franchises[*].releases[*].names,posters.medium.url,type.full_string,genres,season.year,description,player.host,player.episodes.string,player.list[*].name,player.list[*].preview,player.list[*].hls.sd,player.list[*].hls.hd");

        if (!response.IsSuccessStatusCode)
            throw new Exception("Problem with getting data");

        var lastUpdatesJsonObject = JsonNode.Parse(await response.Content.ReadAsStreamAsync());
        var lastUpdatesList = lastUpdatesJsonObject?["list"]?.AsArray();

        if (lastUpdatesList == null)
            throw new Exception("Problem with parse data");

        var lastUpdates = lastUpdatesList.Deserialize<List<LastUpdatesDto>>();
        
        return lastUpdates;
    }

    public async Task<string> Search()
    {
        return "";
    }
}