using System.Net;

namespace AAPI.Config;

public static class WebClientConfig
{
    private static readonly HttpClientHandler _clientHandler = new()
    {
        Proxy = new WebProxy()
        {
            UseDefaultCredentials = false,
            Address = new Uri("http://38.170.124.7:9398"),
            Credentials = new NetworkCredential("SzsY2w","VU5CL3"),
        },
        UseProxy = true
    };

    public static HttpClient WebClient = new HttpClient(_clientHandler);
}