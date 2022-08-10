namespace Common.Interfaces;

public interface IDownloader
{
    void Download(string url, string destination);
}