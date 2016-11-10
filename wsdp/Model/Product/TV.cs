using System.Collections.Generic;

namespace Model.Product
{
    public class TV
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string ImageLink { get; set; }
        public Dictionary<string, string> Characteristics { get; set; }
    }
}