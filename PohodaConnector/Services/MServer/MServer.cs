using PohodaConnector.Interfaces;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Xml;

namespace PohodaConnector.Services.MServer;

public class MServer : IAccountingSoftware
{
    private readonly string _serverName;
    private readonly string _pathToServer;
    private readonly string _username;
    private readonly string _password;
    private readonly short _retryDelay;
    private readonly HttpClient _httpClient;

    public MServer(string serverName, string pathToServer, string serverUrl, string username, string password,
        short retryDelay)
    {
        _serverName = serverName;
        _pathToServer = pathToServer;
        _username = username;
        _password = password;
        _retryDelay = retryDelay;
        _httpClient = new HttpClient { BaseAddress = new Uri(serverUrl) };
    }

    public async void StartServer()
    {
        if (await IsConnectionAvailable(1))
            return;

        var mServerStartCommand = $"cd \"{_pathToServer}\" & pohoda.exe /http start {_serverName}";

        ExecuteCliCommand(mServerStartCommand);
    }

    public async Task<bool> IsConnectionAvailable(int numberOfRetries)
    {
        _httpClient.DefaultRequestHeaders.Add("STW-Authorization", CreateAuthHeader());
        _httpClient.DefaultRequestHeaders.Add("Accept", "text/xml");

        var responseCode = HttpStatusCode.BadRequest;

        while (responseCode != HttpStatusCode.OK && numberOfRetries >= 0)
        {
            numberOfRetries--;

            try
            {
                using var response = await _httpClient.GetAsync("/status");
                responseCode = response.StatusCode;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Console.WriteLine("Error: Couldn't connect to mServer \nResponseCode" + response.StatusCode);
                    continue;
                }

                var xmlDoc = new XmlDocument();
                var responseString = await response.Content.ReadAsStringAsync();
                xmlDoc.LoadXml(responseString);
                var actualServerName = xmlDoc.GetElementsByTagName("name")[0]?.InnerText;

                if (actualServerName != null && !actualServerName.Equals(_serverName)) return false;

                Console.WriteLine("mServer is running on " + _serverName);
                return true;
            }
            catch (HttpRequestException)
            {
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Couldn't connect to server.");
                return false;
            }

            await Task.Delay(_retryDelay);
        }

        Console.WriteLine("Couldn't connect to the server");

        return false;
    }

    public async Task<string> SendRequest(string body)
    {
        var response = await _httpClient.PostAsync("/xml", new ByteArrayContent(Encoding.UTF8.GetBytes(body)));
        return await response.Content.ReadAsStringAsync();
    }

    public async void StopServer()
    {
        _httpClient.Dispose();
        var mServerStopCommand = $"cd \"{_pathToServer}\" & pohoda.exe /http stop {_serverName}";
        ExecuteCliCommand(mServerStopCommand);

        if (await IsConnectionAvailable(1)) Console.WriteLine("Couldn't stop mServer");
    }

    private static void ExecuteCliCommand(string command)
    {
        var cmd = new Process();
        cmd.StartInfo.FileName = "cmd.exe";
        cmd.StartInfo.RedirectStandardInput = true;
        cmd.StartInfo.RedirectStandardOutput = false;
        cmd.StartInfo.CreateNoWindow = true;
        cmd.StartInfo.UseShellExecute = false;
        cmd.Start();

        cmd.StandardInput.WriteLine(command);
        cmd.StandardInput.Flush();
        cmd.StandardInput.Close();
        cmd.WaitForExit();
    }

    private string CreateAuthHeader()
    {
        return "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_username}:{_password}"));
    }
}