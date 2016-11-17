using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace DAL.Elastic
{
    public class ElasticContext
    {
        public ElasticContext(string connection)
        {
            var dataindex = connection;
            var uri = new Uri("http://localhost:9200/");
            var settings = new ConnectionSettings(uri);
            settings.DefaultIndex(dataindex);
            var client = new ElasticClient(settings);
            var responce = client.IndexExists(dataindex);
            if(!responce.Exists) client.CreateIndex(dataindex);
        }

        public ElasticContext() : this("WSDPProducts") { }
    }
}
