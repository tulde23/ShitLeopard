using System;
using System.Diagnostics;
using MongoDB.Bson;
using MongoDB.Entities;
using Newtonsoft.Json;
using OpenTelemetry;

namespace ShitLeopard.Api
{
    public static class Activities
    {
        public static ActivitySource GlobalActivitySource { get; } = new ActivitySource(
       "shitleopard.io");
    }

    /**
     *  .AddOtlpExporter(opt => {
                          opt.Endpoint = new System.Uri("ingest.lightstep.com:443");
                          opt.Headers = JsonConvert.SerializeObject(new Dictionary<string, string>()
                            {
                                { "lightstep-access-token", "C+YfA59w2n2xprCNiKOcPGX10aEZ6e1WfvWAWYuIGGn6iX5TOpQ6b6PoSFudHrzNxfjgi892ntGIjBmtFdjel0Mbd7cwQCxX8a1ELPPc"}
                            });
                          //opt.Credentials = new SslCredentials();
                      })
     * */

    public class ShitleopardExporter : OpenTelemetry.BaseExporter<Activity>
    {
        public ShitleopardExporter()
        {
       
        }
        public override ExportResult Export(in Batch<Activity> batch)
        {
            var db = DB.Database("ShitLeopard");
            var collection = db.GetCollection<BsonDocument>("OpenTelemetry");
            foreach (var item in batch)
            {
             
                var json = $"{JsonConvert.SerializeObject(item, Formatting.Indented)}";
       

                collection.InsertOne(BsonDocument.Parse(json));
            }

            return ExportResult.Success;
        }
    }
}