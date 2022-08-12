using System.Text;

namespace Common.Services.Serialization;

public class Utf8StringWriter : StringWriter
{
    public override Encoding Encoding => new UTF8Encoding(false);
}