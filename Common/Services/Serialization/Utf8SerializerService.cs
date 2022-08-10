using Common.Interfaces;
using System.Xml;
using System.Xml.Serialization;

namespace Common.Services.Serialization;

public class Utf8SerializerService : ISerializer
{
    public string Serialize<T>(T data)
    {
        var x = new XmlSerializer(data.GetType());

        TextWriter writer = Utf8StringWriter.CreateInstance();
        x.Serialize(writer, data);
        var s = writer.ToString() ?? throw new InvalidOperationException();
        writer.Flush();
        writer.Close();
        return s;
    }

    public T Deserialize<T>(string xml)
    {
        if (string.IsNullOrEmpty(xml)) return default(T) ?? throw new InvalidOperationException();

        var serializer = new XmlSerializer(typeof(T));

        var settings = new XmlReaderSettings();

        using var textReader = new StringReader(xml);
        using var xmlReader = XmlReader.Create(textReader, settings);
        return (T)serializer.Deserialize(xmlReader) ?? throw new InvalidOperationException();
    }
}