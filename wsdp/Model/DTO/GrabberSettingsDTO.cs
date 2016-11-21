using System.Collections.Generic;

namespace Model.DTO {
	public class GrabberSettingsDTO {
		public int Id { get; set; }
		public List<GrabberPropertyItemDTO> PropertyItems { get; set; }
		public string urlJsonData { get; set; }

		public GrabberSettingsDTO() {
			PropertyItems = new List<GrabberPropertyItemDTO>();
		}
	}
}
