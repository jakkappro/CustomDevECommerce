namespace Common.Interfaces;

public interface IXmlFeedParser
{
    T Parse<T>(string url);
}