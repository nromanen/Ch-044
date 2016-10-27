using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Product {
    public class Laptop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgPath { get; set; }
        public Dictionary<string, string> Characteristic { get; set; }

        public Laptop()
        {
            Characteristic = new Dictionary<string, string>();
        }

        public override string ToString()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("{0}\n{1}\n{2}\n", Name, Description, ImgPath);
            foreach (var item in Characteristic)
            {
                strBuilder.Append(item.Key + " " + item.Value + "\n");
            }
            return strBuilder.ToString();
        }
    }
}
