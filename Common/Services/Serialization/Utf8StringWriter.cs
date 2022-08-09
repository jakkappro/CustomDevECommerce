using System.Text;

namespace Common.Services.Serialization;

public class Utf8StringWriter : StringWriter
{
    private static Utf8StringWriter? _instance;
    public override Encoding Encoding => new UTF8Encoding(false);

    private Utf8StringWriter()
    {
    }

    public static Utf8StringWriter CreateInstance()
    {
        return _instance ??= new Utf8StringWriter();
    }
}