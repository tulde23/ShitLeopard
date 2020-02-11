using System.Collections.Generic;
using System.Threading.Tasks;
using ShitLeopard.Api.Models;

namespace ShitLeopard.Api.Contracts
{
    public interface ISeasonService
    {
        Task<IEnumerable<SeasonModel>> ListAsync();

        /// <summary>
        /// Gets the season asynchronous.
        /// </summary>
        /// <param name="seasonId">The season identifier.</param>
        /// <returns></returns>
        Task<SeasonModel> GetSeasonAsync(long seasonId);
    }
}