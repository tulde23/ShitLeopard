using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MongoDB
{
    public static class MongoQueryableFullTextExtensions
    {
        public static IMongoQueryable<T> WhereText<T>(this IMongoQueryable<T> query, string search)
        {
            var filter = Builders<T>.Filter.Text(search, new TextSearchOptions { CaseSensitive = false } );
            return query.Where(_ => filter.Inject());
        }
    }
}
