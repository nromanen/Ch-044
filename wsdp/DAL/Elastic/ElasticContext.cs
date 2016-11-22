using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using System.Configuration;
using System.Data.SqlClient;


namespace DAL.Elastic
{
    public class ElasticContext
    {
        public ElasticClient Client { get; set; }
        public ElasticContext(string connection)
        {
            //get data for elastic connection
            var connStr = ConfigurationManager.ConnectionStrings[connection].ConnectionString; 
            var builder = new SqlConnectionStringBuilder(connStr);
            var dataindex = builder.InitialCatalog;
            var path = builder.DataSource;
            var uri = new Uri(path);

            var settings = new ConnectionSettings(uri);
            settings.DefaultIndex(dataindex);
            Client = new ElasticClient(settings);
            var responce = Client.IndexExists(dataindex);
            //Check the database existing in elastic server
            if(!responce.Exists) Client.CreateIndex(dataindex);
        }

        public ElasticContext() : this("ElasticConnection") { }
    }
}
