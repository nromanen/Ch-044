using Common.Enum;

namespace Model.DB
{
    public class Property
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PropetyType Type { get; set; }
        public string Prefix { get; set; }
        public string Sufix { get; set; }
        public int Characteristic_Id { get; set; }

    }
}