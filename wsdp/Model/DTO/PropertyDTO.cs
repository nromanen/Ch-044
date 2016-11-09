using Common.Enum;

namespace Model.DTO
{
    public class PropertyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PropertyType Type { get; set; }
        public string DefaultValue { get; set; }
        public string Prefix { get; set; }
        public string Sufix { get; set; }
        public int Category_Id { get; set; }
    }
}