using System;
using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Nest;
using Nest.JsonNetSerializer;
using ShitLeopard.Common.Contracts;
using ShitLeopard.Common.Documents;

namespace ShitLeopard.Common.Providers
{
    public  class ElasticSearchConnectionProvider : IElasticSearchConnectionProvider
    {
        public ElasticSearchConnectionProvider(IConfiguration configuration)
        {
            var uri = new Uri(configuration["ElasticHost"]);
            var pool = new SingleNodeConnectionPool(uri);

            var settings = new ConnectionSettings(pool, sourceSerializer: (builtin, settings) => new JsonNetSerializer(builtin, settings, () => new Newtonsoft.Json.JsonSerializerSettings { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore }))
            .EnableDebugMode()
            .PrettyJson()
            .DefaultMappingFor<EpisodeDocument>(d=> d.IndexName("episodes"))
             .DefaultMappingFor<ShowDocument>(d => d.IndexName("shows"))
              .DefaultMappingFor<SeasonDocument>(d => d.IndexName("seasons"))
             .RequestTimeout(TimeSpan.FromMinutes(2));
            //ConnectionSettings.DefaultMappingFor<TDocument>()
            Client = new ElasticClient(settings);
        }

        public ElasticClient Client { get; }
    }
}