using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShitLeopard.DataLayer.Entities
{
    /// <summary>
    /// Scalar queries.  Queries that produce a single numeric value.
    /// </summary>
    public static class ScalarQueries
    {
        /// <summary>
        /// Counts the occurences of single word.
        /// </summary>
        /// <param name="shitLeopardContext">The shit leopard context.</param>
        /// <param name="word">The word.</param>
        /// <param name="seasonId">The season identifier.</param>
        /// <param name="episodeId">The episode identifier.</param>
        /// <returns></returns>
        public static async Task<long> CountOccurencesOfSingleWord(this ShitLeopardContext shitLeopardContext, string word, int? seasonId=null, int? episodeId=null)
        {
            //how many time does fuck appear in season 1
            var query = from sw in shitLeopardContext.ScriptWord
                        join sl in shitLeopardContext.ScriptLine on sw.ScriptLineId equals sl.Id
                        join s in shitLeopardContext.Script on sl.ScriptId equals s.Id
                        join e in shitLeopardContext.Episode on s.EpisodeId equals e.Id
                        select new
                        {
                            sw.Word,
                            e.SeasonId,
                            EpisodeId = e.OffsetId
                        };

            query = query.Where(x => x.Word == word);
            if (seasonId != null)
            {
                query = query.Where(x => x.SeasonId == seasonId);
            }
            if (episodeId != null)
            {
                query = query.Where(x => x.EpisodeId == episodeId);
            }

            return await query.LongCountAsync();
        }
    }
}