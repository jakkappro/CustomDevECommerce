using Common.Interfaces;
using System.Net;

namespace Common.Services.XMLFeedParserService;

public class UrlXmlFeedParser : IXmlFeedParser
{
    private readonly HttpClient client;
    private readonly ISerializer serializer;

    public UrlXmlFeedParser(HttpClient client, ISerializer serializer)
    {
        this.client = client;
        this.serializer = serializer;
    }

    public async Task<T> Parse<T>(string url)
    {
        client.BaseAddress = new Uri(url);

        using var response = await client.GetAsync("");

        if (response.StatusCode != HttpStatusCode.OK)
            throw new ArgumentException("Couldn't get feed");

        return serializer.Deserialize<T>(await response.Content.ReadAsStringAsync());
    }
}