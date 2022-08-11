using Common.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Common.Services.XMLFeedParserService;

public class UrlXmlFeedParser : IXmlFeedParser
{
    private readonly HttpClient _client;
    private readonly ISerializer _serializer;
    private readonly ILogger<UrlXmlFeedParser> _logger;
    private string _data;

    public UrlXmlFeedParser(HttpClient client, ISerializer serializer, ILogger<UrlXmlFeedParser> logger)
    {
        _client = client;
        _serializer = serializer;
        _logger = logger;
        _data = "";
    }

    public T Parse<T>(string url)
    {
        _client.BaseAddress = new Uri(url);
        _logger.LogInformation("Parsing xml feed from: {url}", url);
        GetData().Wait();

        return _serializer.Deserialize<T>(_data);
    }

    private async Task GetData()
    {
        try
        {
            var response = await _client.GetAsync("");

            _logger.LogInformation("Got feed bitch");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var r = await response.Content.ReadAsStringAsync();
                response.Dispose();
                _data = r;
                return;
            }

            response.Dispose();
            _logger.LogError("Couldn't get xml feed from {url}. Response code {code}.", _client.BaseAddress?.ToString(), response.StatusCode);
            throw new ArgumentException();
        }
        catch
        {
            _logger.LogError("Something went wrong.");
            throw;
        }
    }
}