namespace PohodaConnector.Interfaces;

public interface IAccountingSoftware
{
    void StartServer();
    void StopServer();
    Task<bool> IsConnectionAvailable(int numberOfRetries);
    Task<string> SendRequest(string request);
}