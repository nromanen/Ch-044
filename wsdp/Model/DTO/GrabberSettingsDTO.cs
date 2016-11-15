using System.Collections.Generic;

namespace Model.DTO {
	public class GrabberSettingsDTO {
		public int Id { get; set; }
		public List<GraberPropertyItemDTO> PropertyItems { get; set; }

		public GrabberSettingsDTO() {
			PropertyItems = new List<GraberPropertyItemDTO>();
		}
	}
}
