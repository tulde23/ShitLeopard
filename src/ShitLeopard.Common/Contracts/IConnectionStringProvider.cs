namespace ShitLeopard.Common.Contracts
{
    public interface IConnectionStringProvider
    {
        string GetConnectionString();

        string GetString(string key);
    }
}