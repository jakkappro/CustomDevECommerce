namespace Common.Interfaces;

public interface IDownloader
{
    Task Download(string url, string destination);
}