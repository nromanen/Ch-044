using System.Collections.Generic;

namespace Model.DTO {
	public class GrabberSettingsDTO {
		public int Id { get; set; }
		public List<string> Name { get; set; }
		public List<string> Price { get; set; }
		public List<string> OldPrice { get; set; }
		public List<string> ImgLink { get; set; }
		public List<GrabberPropertyItemDTO> PropertyItems { get; set; }
		public string urlJsonData { get; set; }

		public GrabberSettingsDTO() {
			PropertyItems = new List<GrabberPropertyItemDTO>();
		}
	}
}
