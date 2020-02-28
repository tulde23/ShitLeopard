using System.Threading.Tasks;
using ShitLeopard.Common.Models;

namespace ShitLeopard.Common.Contracts
{
    /// <summary>
    ///
    /// </summary>
    public interface IRequestProfileService
    {
        /// <summary>
        /// Saves the asynchronous.
        /// </summary>
        /// <param name="requestProfileModel">The request profile model.</param>
        /// <returns></returns>
        Task SaveAsync(RequestProfileModel requestProfileModel);
    }
}