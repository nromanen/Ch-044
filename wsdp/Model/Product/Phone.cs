using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Product {

    [Serializable]
    public class ClearGood
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public Characteristics Characteristics { get; set; }

        public override string ToString()
        {
            string result = Name + " " + Description + " \n Characteristics:\n";
            foreach (var item in Characteristics.Dict)
            {
                result += "\t" + item.Key + " " + item.Value + " \n";
            }
            return result;
        }
    }

    [Serializable]
    public class ConcreteGood
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public ClearGood Good { get; set; }
        public Producer Producer { get; set; }
        public string Link { get; set; }
        public Shop Shop { get; set; }

        public override string ToString()
        {
            string result = Good.ToString() + "Producer:\n\t" + Producer.ToString() + "\nShop:\n\t" + Shop.ToString();
            return result;
        }
    }

    [Serializable]
    public class Producer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Producer()
        {
            Name = "";
            Description = "";
        }
        public override string ToString()
        {
            return Name + " " + Description;
        }
    }

    [Serializable]
    public class Characteristics
    {
        public Dictionary<string, string> Dict { get; set; }
        public Characteristics()
        {
            Dict = new Dictionary<string, string>();
        }
    }

    [Serializable]
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
