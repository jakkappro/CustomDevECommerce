﻿using Common.Interfaces;
using Microsoft.Extensions.Logging;
using System.Xml;
using System.Xml.Serialization;

namespace Common.Services.Serialization;

public class Utf8SerializerService : ISerializer
{
    private readonly ILogger<Utf8SerializerService> _logger;

    public Utf8SerializerService(ILogger<Utf8SerializerService> logger)
    {
        _logger = logger;
    }

    public string Serialize<T>(T data)
    {
        var x = new XmlSerializer(data?.GetType() ?? throw new ArgumentNullException());

        TextWriter writer = Utf8StringWriter.CreateInstance();
        string s;
        try
        {
            x.Serialize(writer, data);
            s = writer.ToString() ?? throw new InvalidOperationException();
        }
        catch
        {
            _logger.LogError("Error while serializing {data} data type.", typeof(T));
            throw;
        }

        writer.Flush();
        writer.Close();
        return s;
    }

    public T Deserialize<T>(string xml)
    {
        if (string.IsNullOrEmpty(xml))
        {
            _logger.LogError("Error, can't deserialize empty value.");
            return default(T) ?? throw new ArgumentException();
        };

        var serializer = new XmlSerializer(typeof(T));

        var settings = new XmlReaderSettings();

        using var textReader = new StringReader(xml);
        using var xmlReader = XmlReader.Create(textReader, settings);
        T data;
        try
        {
            data = (T)serializer.Deserialize(xmlReader)! ?? throw new InvalidOperationException();
        }
        catch
        {
            _logger.LogError("Error while deserializing {data} data type.", typeof(T));
            throw;
        }
        return data;
    }
}