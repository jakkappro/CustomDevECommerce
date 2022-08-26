using System.Globalization;
using Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Common.Services.IdGenerator;

public class IdGenerator : IIdGenerator
{
    private const string Prefix = "3";
    private readonly IConfiguration _configuration;
    private readonly ILogger<IdGenerator> _logger;

    public IdGenerator(ILogger<IdGenerator> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public string GetNextId()
    {
        var id = Prefix;
        var validformats = new[] { "yyMMdd" };

        var provider = new CultureInfo("en-US");

        try
        {
            var file = File.ReadAllLines(_configuration["IdStorageFile"]);
            if (file.Length != 2)
            {
                _logger.LogError("Data in file were changed.");
                throw new ArgumentException();
            }

            var currentDate = DateTime.Now;
            var fileDate = DateTime.ParseExact(file[0], validformats, provider);
            id += currentDate.ToString("yyMMdd");

            if (currentDate.Date > fileDate.Date)
            {
                id += "001";
                File.WriteAllLines(_configuration["IdStorageFile"], new[] { currentDate.ToString("yyMMdd"), "001" });
                return id;
            }

            var i = (int.Parse(file[1]) + 1).ToString("000");
            id += i;
            File.WriteAllLines(_configuration["IdStorageFile"], new[] { currentDate.ToString("yyMMdd"), i });
            return id;
        }
        catch
        {
            _logger.LogError("Couldn't load data from file.");
            throw;
        }
    }
}