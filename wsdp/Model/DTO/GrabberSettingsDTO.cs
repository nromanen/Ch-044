using System.Collections.Generic;

namespace Model.DTO {
	public class GrabberSettingsDTO {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Price { get; set; }
		public string OldPrice { get; set; }
		public string ImgLink { get; set; }
		public List<GrabberPropertyItemDTO> PropertyItems { get; set; }
		public string urlJsonData { get; set; }

		public GrabberSettingsDTO() {
			PropertyItems = new List<GrabberPropertyItemDTO>();
		}
	}
}
