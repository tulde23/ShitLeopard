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
                var query = "select top 1 *   from Quote  order by NewId(), Popularity";
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
                var existing = await context.Quote.SingleOrDefaultAsync(x => x.Id == quoteModel.Id);
                if (existing == null)
                {
                    context.Add(quoteModel);
                }
                else
                {
                    existing.Popularity = quoteModel.Popularity;
                }
                await context.SaveChangesAsync();
            }
        }
    }
}