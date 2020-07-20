using Elasticsearch.Net;
using Nest;
using System;

namespace Poc_Elastic_Search.Api.ElasticSearch.Client
{
    public class ElasticSearchClient
    {
        public ElasticClient Connect { get; }

        public ElasticSearchClient()
		{

			var node = new Uri("http://192.168.99.100:9200");
			var connectionPool = new SingleNodeConnectionPool(node);
			var connectionSettings = new ConnectionSettings(node)
			.DefaultIndex("people")
			.DefaultFieldNameInferrer(p => p)
			.OnRequestCompleted(info =>
			{
			// info.Uri is /_search/ without the default index
			// my ES instance throws an error on the .kibana index (@timestamp field not mapped because I sort on @timestamp)
		});

			Connect = new ElasticClient(connectionSettings);
		}
	}
}
