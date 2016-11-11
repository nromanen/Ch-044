using System.Xml.Serialization;

namespace Model.DTO
{
    [XmlRoot("TV")]
    public class TVDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [XmlElement("ImageLink")]
        public string ImgUrl { get; set; }
        public string Price { get; set; }
    }
}