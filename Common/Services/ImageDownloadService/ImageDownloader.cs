using Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace Common.Services.ImageDownloadService;

public class ImageDownloader : IDownloader
{
    private readonly HttpClient _client;
    private readonly ILogger<ImageDownloader> _logger;

    public ImageDownloader(HttpClient client, ILogger<ImageDownloader> logger)
    {
        this._client = client;
        _logger = logger;
    }

    public async Task Download(string url, string destination)
    {
        if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(destination))
        {
            return;
        }

        try
        {
            _logger.LogDebug("Downloading picture with url {url} and destination {destination}", url, destination);

            var fileBytes = await _client.GetByteArrayAsync(url);
            if (File.Exists(destination))
                return;

            await using var fs = File.Create(destination);
            await fs.WriteAsync(fileBytes);
            fs.Close();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error downloading image, url: {url}", url);
            throw;
        }
    }
}