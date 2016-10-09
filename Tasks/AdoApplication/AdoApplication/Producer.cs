using System.Text;

namespace AdoApplication {
	public class Producer {
		public int Id { get; set; }

		public string Name { get; set; }

		public string Country { get; set; }

		public override string ToString() {
			StringBuilder message = new StringBuilder();
			message.Append($"Producer Id - {Id}     /     Name - {Name}     /     Country - {Country}");
			return message.ToString();
		}
	}
}