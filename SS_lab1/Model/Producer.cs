using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_lab1.Model
{
    public class Producer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public Producer(int id, string name, string country)
        {
            this.Id = id;
            this.Name = name;
            this.Country = country;
        }
        public Producer() : this(-1, "unknown", "unknown") { }
        
        public override string ToString()
        {
            StringBuilder res = new StringBuilder();
            res.Append(Id + "\t");
            res.Append(Name + "\t");
            res.Append(Country);
            return res.ToString();
        }
    }
}
