using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using System.Configuration;


namespace DAL.Elastic
{
    public class ElasticContext
    {
        public ElasticClient Client { get; set; }
        public ElasticContext(string connection, string dataindex)
        {
            string path = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            var uri = new Uri(path);
            var settings = new ConnectionSettings(uri);
            settings.DefaultIndex(dataindex);
            Client = new ElasticClient(settings);
            var responce = Client.IndexExists(dataindex);
            //Check the database existing in elastic server
            if(!responce.Exists) Client.CreateIndex(dataindex);
        }

        public ElasticContext() : this("ElasticConnection", "wsdpgoods") { }
    }
}
