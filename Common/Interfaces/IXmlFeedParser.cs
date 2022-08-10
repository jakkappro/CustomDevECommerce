namespace Common.Interfaces;

public interface IXmlFeedParser
{
    Task<T> Parse<T>(string url);
}