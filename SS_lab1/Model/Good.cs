using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SS_lab1.Model
{
    [Serializable]
    public class Good
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public Producer Producer { get; set; }

        public Good(int id, string name, decimal price, Category category, Producer producer)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.Category = category;
            this.Producer = producer;
        }

        public Good() : this(-1, "unknown", 0, new Category(), new Producer())
        {
        }

        public override string ToString()
        {
            StringBuilder res = new StringBuilder();
            res.Append(Id);
            res.Append("\t");
            res.Append(Name);
            res.Append("\t");
            res.Append(Price);
            res.Append("\t");
            res.Append(Category.Name);
            res.Append("\t");
            res.Append(Producer.Name);
            return res.ToString();
        }
    }
}
