namespace AdoApplication {
	public class Category {
		public int Id { get; set; }

		public string Name { get; set; }

		public override string ToString() {
			return $"Category ID - {Id}     /     Name - {Name}";
		}
	}
}