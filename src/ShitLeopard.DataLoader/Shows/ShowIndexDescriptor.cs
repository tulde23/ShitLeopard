using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using ShitLeopard.Common.Documents;

namespace ShitLeopard.DataLoader.Shows
{
    public   class ShowIndexDescriptor : BulkIndexDescriptor<ShowDocument>
    {
        public ShowIndexDescriptor( BulkIndexDescriptor<ShowDocument> bulkIndexDescriptor)
        {

        }
        public CreateIndexDescriptor Get(string indexName)
        {
            return new CreateIndexDescriptor(indexName).Settings(s => s
             .Analysis(ay => ay.Analyzers(aa => aa.Custom("email", lc => lc.Tokenizer("keyword").Filters("lowercase"))))
                   .RequestsCacheEnabled(true)
                  .RefreshInterval(5000)
                  .NumberOfShards(2)
                  .NumberOfReplicas(2)).Map<ShowDocument>(x => x.AutoMap());
                                                
        } 
    }
}
