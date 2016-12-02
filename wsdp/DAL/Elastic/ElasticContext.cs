﻿using System;
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

        public IList<GoodDTO> GetByIdUrl(string url)
        {
            return client
                .Search<GoodDTO>(q => q.Query(t => t.Term(x => x.Field("_id")
                .Value(url))))
                .Hits
                .Select(x=>x.Source)
                .ToList();
        }

        public IList<GoodDTO> GetAll()
        {
            return client
                .Search<GoodDTO>(q => q.Query(t => t.MatchAll()))
                .Hits
                .Select(x=>x.Source)
                .ToList();
        } 
        private void Insert(GoodDTO item)
        {
            client.Index(item);
        }

        private bool Update(GoodDTO item)
        {
            if (item == null) return false;
            var list = GetByIdUrl(item.UrlLink);

            if (!list.Any()) return false;
            client.Index(item, i => i.Id(item.UrlLink));
            return true;
        }

        private void Remove(GoodDTO item)
        {
            if (item == null) return;
            var list = GetByIdUrl(item.UrlLink);

            if (!list.Any()) return;
            foreach (var good in list)
            {
                good.Status = false;
                Update(good);
            }
        }

        private void HardRemove(GoodDTO item)
        {
            if (item == null) return;
            var list = GetByIdUrl(item.UrlLink);

            if (!list.Any()) return;
            foreach (var good in list)
            {
                client.Delete<GoodDTO>(good.UrlLink);
            }
        }


    }
}
