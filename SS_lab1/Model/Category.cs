using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_lab1.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Category(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public Category() : this(-1, "unknown") { }
        public override string ToString()
        {
            StringBuilder res = new StringBuilder();
            res.Append(Id);
            res.Append("\t");
            res.Append(Name);
            return res.ToString();
        }
    }
}
