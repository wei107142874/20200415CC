using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// ES帮助类
    /// </summary>
    public class ElasticSearchHelp
    {
        public static ElasticClient _client;
       
        /// <summary>
        /// 创建索引
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="document"></param>
        /// <returns></returns>
        public static async Task<IndexResponse> CreateIndex<TDocument>(TDocument document)where TDocument:class
        {
            return await _client.IndexDocumentAsync(document);
        }


        public static void GetClient()
        {
            if (_client != null)
                return;
            var uris = new[]
            {
                new Uri("http://localhost:9200"),
                //new Uri("http://localhost:9201"),
                //new Uri("http://localhost:9202"),
            };

            //集群配置
            var connectionPool = new SniffingConnectionPool(uris);
            var settings = new ConnectionSettings(connectionPool)
                .DefaultIndex("db1");

            _client = new ElasticClient(settings);


            

        }
    }
}
