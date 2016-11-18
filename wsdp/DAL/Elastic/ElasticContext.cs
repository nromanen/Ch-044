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
        public ElasticClient Client { get; set; }
        public ElasticContext(string connection)
        {
            var dataindex = connection;
            var uri = new Uri("http://localhost:9200/");
            var settings = new ConnectionSettings(uri);
            settings.DefaultIndex(dataindex);
            Client = new ElasticClient(settings);
            var responce = Client.IndexExists(dataindex);
            //Check the database existing in elastic server
            if(!responce.Exists) Client.CreateIndex(dataindex);
        }

        public ElasticContext() : this("connection") { }
    }
}
