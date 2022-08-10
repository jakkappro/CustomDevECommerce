using Common.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Common.Services.XMLFeedParserService;

public class UrlXmlFeedParser : IXmlFeedParser
{
    private readonly HttpClient client;
    private readonly ISerializer serializer;
    private readonly ILogger<UrlXmlFeedParser> _logger;

    public UrlXmlFeedParser(HttpClient client, ISerializer serializer, ILogger<UrlXmlFeedParser> logger)
    {
        this.client = client;
        this.serializer = serializer;
        _logger = logger;
    }

    public async Task<T> Parse<T>(string url)
    {
        client.BaseAddress = new Uri(url);

        using var response = await client.GetAsync("");

        if (response.StatusCode == HttpStatusCode.OK)
            return serializer.Deserialize<T>(await response.Content.ReadAsStringAsync());

        _logger.LogError("Couldn't get xml feed from {url}. Response code {code}.", url, response.StatusCode);
        throw new ArgumentException();

    }
}