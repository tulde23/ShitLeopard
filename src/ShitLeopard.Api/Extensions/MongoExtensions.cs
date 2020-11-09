using MongoDB.Driver;
using ShitLeopard.Common.Documents;

namespace ShitLeopard.Common.Contracts
{
    public static class MongoExtensions
    {
        public static IMongoCollection<EpisodeDocument> GetEpisodesCollection(this IMongoProvider provider)
        {
            return provider.GetMongoCollection<EpisodeDocument>();
        }

        public static IMongoCollection<LineDocument> GetLinesCollection(this IMongoProvider provider)
        {
            return provider.GetMongoCollection<LineDocument>();
        }

        public static IMongoCollection<WordDocument> GetWordsCollection(this IMongoProvider provider)
        {
            return provider.GetMongoCollection<WordDocument>();
        }

        public static IMongoCollection<CharacterDocument> GetCharactersCollection(this IMongoProvider provider)
        {
            return provider.GetMongoCollection<CharacterDocument>();
        }
    }
}