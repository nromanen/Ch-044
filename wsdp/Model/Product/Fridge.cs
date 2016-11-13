using System.Collections.Generic;

namespace Model.Product
{
    public class Fridge
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageLink { get; set; }
        public string Price { get; set; }
        public Dictionary<string, string> CharacteristicsDictionary { get; set; }

        public Fridge()
        {
            CharacteristicsDictionary = new Dictionary<string, string>();
        }

    }
}