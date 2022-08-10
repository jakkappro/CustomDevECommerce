using Common.Interfaces;

namespace Common.Services.ImageDownloadService;

public class ImageDownloader : IDownloader
{
    private readonly HttpClient client;

    public ImageDownloader(HttpClient client)
    {
        this.client = client;
    }

    public async void Download(string url, string destination)
    {
        try
        {
            var fileBytes = await client.GetByteArrayAsync(url);
            if (File.Exists(destination))
                return;

            await using var fs = File.Create(destination);
            await fs.WriteAsync(fileBytes);
            fs.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(
                $"Failed to download image, path {url}, message {e.Message}");
        }
    }
}