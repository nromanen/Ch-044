using Common.Enum;
using System;

namespace Model.DTO
{
    public class ParserTaskDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public CategoryDTO Category { get; set; }

        public int WebShopId { get; set; }
        public WebShopDTO WebShop { get; set; }

        public Priority Priority { get; set; }

        public Status Status { get; set; }

        public DateTime? EndDate { get; set; }

        public IteratorSettingsDTO IteratorSettings { get; set; }

        public GrabberSettingsDTO GrabberSettings { get; set; }

		public int ExecuteInfoCount { get; set; }
    }
}