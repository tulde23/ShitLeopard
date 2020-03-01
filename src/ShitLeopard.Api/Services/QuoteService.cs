using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShitLeopard.Api.Contracts;
using ShitLeopard.Api.Models;
using ShitLeopard.DataLayer.Entities;

namespace ShitLeopard.Api.Services
{
    public class QuoteService : BaseService, IQuoteService
    {
        public QuoteService(ILoggerFactory loggerFactory, Func<ShitLeopardContext> contextProvider, IMapper mapper) : base(loggerFactory, contextProvider, mapper)
        {
        }

        public async Task DeleteQuoteAsync(long quoteId)
        {
            using (var context = ContextProvider())
            {
                var quote = await context.Quote.FirstOrDefaultAsync(x => x.Id == quoteId);
                context.Quote.Remove(quote);
                await context.SaveChangesAsync();
            }
        }

        public async Task<QuoteModel> GetRandomQuoteAsync()
        {
            using (var context = ContextProvider())
            {
                var query = @"
select top 1 Q.*, E.Title EpisodeTitle, 's'+cast(e.SeasonId as varchar(10)) +'e'+cast( e.OffsetId as varchar(10)) EpisodeId from quote Q  inner join ScriptLine on Q.ScriptLineId = ScriptLine.Id
inner join Script S on S.Id = ScriptLine.ScriptId
inner join Episode E on e.Id = S.EpisodeId
ORDER BY 
 NewId(), Popularity

"
;
                using (var c = context.Database.GetDbConnection())
                {
                    c.Open();
                    return (await c.QueryAsync<QuoteModel>(query)).FirstOrDefault();
                }
            }
        }

        public async Task SaveQuoteAsync(QuoteModel quoteModel)
        {
            using (var context = ContextProvider())
            {
                var existing = await context.Quote.SingleOrDefaultAsync(x => x.ScriptLineId == quoteModel.ScriptLineId);
                if (existing == null)
                {
                    var sl = await context.ScriptLine.AsNoTracking().SingleOrDefaultAsync(x => x.Id == quoteModel.ScriptLineId);
                    context.Add(new Quote
                    {
                        ScriptLineId = quoteModel.ScriptLineId,
                        Popularity = 1,
                        Body = sl.Body
                    });
                }
                else
                {
                    var sl = await context.ScriptLine.AsNoTracking().SingleOrDefaultAsync(x => x.Id == quoteModel.ScriptLineId);
                    existing.Body = sl.Body;
                    existing.Popularity++;
                }
                await context.SaveChangesAsync();
            }
        }
    }
}