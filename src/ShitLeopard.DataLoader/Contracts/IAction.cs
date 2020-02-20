using System.Threading.Tasks;

namespace ShitLeopard.DataLoader.Contracts
{
    public interface IAction
    {
        Task RunAsync();
    }
}