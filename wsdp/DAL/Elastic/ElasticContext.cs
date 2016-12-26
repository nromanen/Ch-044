using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using System.Configuration;
using System.Data.SqlClient;
using Model.DTO;

namespace DAL.Elastic
{
    public class ElasticContext
    {
        private ElasticClient client;

        private Queue<KeyValuePair<GoodDTO, CRUD>>  queue;

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
            client = new ElasticClient(settings);
            var responce = client.IndexExists(dataindex);
            //Check the database existing in elastic server
            if (!responce.Exists) client.CreateIndex(dataindex);

            queue = new Queue<KeyValuePair<GoodDTO, CRUD>>();
        }

        public ElasticContext() : this("ElasticConnection") { }

        public void AddOperation(GoodDTO item, CRUD marker)
        {
            queue.Enqueue(new KeyValuePair<GoodDTO, CRUD>(item, marker));
        }

        public int Save()
        {
            var count = queue.Count;
            foreach (var item in queue)
            {
                try
                {
                    switch (item.Value)
                    {
                        case CRUD.Remove:
                            Remove(item.Key);
                            break;

                        case CRUD.HardRemove:
                            HardRemove(item.Key);
                            break;

                        case CRUD.Insert:
                            Insert(item.Key);
                            break;

                        case CRUD.Update:
                            Update(item.Key);
                            break;
                    }
                }
                catch
                {
                    count--;
                }
            }
            queue.Clear();
            return count;
        }

        #region select queries

        public IList<GoodDTO> GetByCategoryId(int id)
        {
            var query = new TermQuery() { Field = "category_Id", Value = id };
            var sd = new SearchDescriptor<GoodDTO>().Query(q => query).Size(600);
            var responce = client.Search<GoodDTO>(x => sd);
            return responce
                .Hits
                .Select(x => x.Source)
                .ToList();
        }
        public IList<GoodDTO> GetByWebShopId(int id)
        {
            var query = new TermQuery() { Field = "webShop_Id", Value = id };
            var sd = new SearchDescriptor<GoodDTO>().Query(q => query).Size(600);
            var responce = client.Search<GoodDTO>(x=>sd);
            return responce
                .Hits
                .Select(x => x.Source)
                .ToList();
        }

        public GoodDTO GetByIdUrl(string url)
        {
            return client
                .Search<GoodDTO>(q => q.Query(t => t.Term(x => x.Field("_id").Value(url))))
                .Hits
                .Select(x=>x.Source)
                .FirstOrDefault();
        }

        public IList<GoodDTO> GetAll()
        {
            return client
                .Search<GoodDTO>(q => q.Query(t => t.MatchAll()).Size(1000))
                .Hits
                .Select(x=>x.Source)
                .ToList();
        }

        public IList<GoodDTO> GetByNameHard(string name)
        {
            var query = new TermQuery() { Field = "name", Value = name };
            var sd = new SearchDescriptor<GoodDTO>().Query(q => query).Size(600);
            var responce = client.Search<GoodDTO>(x => sd);
            return responce
                .Hits
                .Select(x => x.Source)
                .ToList();
        }

        public IList<GoodDTO> GetByPrefix(string prefix, int size = 10)
        {
            var sValue = prefix.ToLower();

            var searchResults = client.Search<GoodDTO>(s => s.From(0)
                .Size(size)
                .Query(q =>
                    q.MatchPhrasePrefix(m => m.Field(p => p.Name).Query(sValue))));
            return searchResults.Hits.Select(x => x.Source).ToList();

        } 

        public IList<GoodDTO> GetExact(string value, int size = 500)
        {
            var sValue = value.ToLower();

            var searchResults = client.Search<GoodDTO>(s => s.From(0)
                .Size(size)
                .Query(q =>
                    q.MatchPhrase(m => m.Field(p => p.Name).Query(sValue))));
            return searchResults.Hits.Select(x => x.Source).ToList();
        }

        public IList<GoodDTO> GetSimilar(string value)
        {
            var sValue = value.ToLower();
         
            var searchResults = client.Search<GoodDTO>(s => s.From(0)
                .Size(10)
                .Query(q =>
                    q.Match(m => m.Field(p => p.Name).Query(sValue))));
            return searchResults.Hits.Select(x => x.Source).ToList();
        }

        #endregion
        private void Insert(GoodDTO item)
        {
            client.Index(item, i=>i.Refresh());
        }

        private bool Update(GoodDTO item)
        {
            if (item == null) return false;
            var good = GetByIdUrl(item.UrlLink);

            if (good == null) return false;
            client.Index(item, i => i.Id(item.UrlLink).Refresh());
            return true;
        }

        private void Remove(GoodDTO item)
        {
            if (item == null) return;
            var good = GetByIdUrl(item.UrlLink);
            if (good == null) return;
            good.Status = false;
            Update(good);
        }

        private void HardRemove(GoodDTO item)
        {
            if (item == null) return;
            var good = GetByIdUrl(item.UrlLink);
            if (good == null) return;
            client.Delete<GoodDTO>(good.UrlLink,i=>i.Refresh());
            
        }

        

    }
}
