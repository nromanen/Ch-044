namespace Model.DTO
{
    public class IteratorSettingsDTO
    {
        public string Url { get; set; }
        public string GoodsIteratorXpath { get; set; }
        public string XPathPageIterator { get; set; }
        public string UrlMask { get; set; }
        public int From { get; set; }
        public int To { get; set; }
    }
}