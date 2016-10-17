using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ByEntity.Model;

namespace ByEntity.Repositories
{
    public class ProducerRepository : BaseRepository<Producer>
    {
        public ProducerRepository(GoodContext db) : base(db) { }
        private GoodContext Db { get { return (GoodContext)Context; } }

        public Producer GetByName(string name)
        {
            return Db.Producers.FirstOrDefault(x => x.Name == name);

        }

        public Producer GetByUnique(string name, string country)
        {
            return Db.Producers.Where(x=>x.Name == x.Name).FirstOrDefault(x =>x.Country == country);
            //return Db.Producers.FirstOrDefault(x => { return x.Name == name && x.Country == country; });

        }

        
    }
}
