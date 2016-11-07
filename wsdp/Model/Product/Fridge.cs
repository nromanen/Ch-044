using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Product
{
    public class Fridge
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Price { get; set; }
        public Dictionary<string, string> CharacteristicsDictionary { get; set; }

        public Fridge()
        {
            CharacteristicsDictionary = new Dictionary<string, string>();
        }

        /*public override string ToString()
        {
            var res = $"Name:{Name}\tPrice:{Price}\n";
            foreach (var item in CharacteristicsDictionary)
            {
                res += $"\t{item.Key} : {item.Value}\n";
            }
            return res;

        }*/
    }
}
