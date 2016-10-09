using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoApplication {
	class ProducerManager : BaseManager, IRepository<Producer> {
		public void Create(Producer item) {
			string sqlExpression = $"INSERT INTO [dbo].[Producer] VALUES ({item.Id},'{item.Name}', '{item.Country}')";
			try {
				using (SqlConnection connection = new SqlConnection(connectionString)) {
					connection.Open();
					SqlCommand command = new SqlCommand(sqlExpression, connection);
					int number = command.ExecuteNonQuery();
					if (number != 0)
						Console.WriteLine("The element was added succsessfully to the table - Producer");
				}
			} catch (Exception) {
				Console.WriteLine("The element wasn`t added, because it was in the table Producer!");
			}
		}

		public void Delete(int id) {
			string sqlExpression = $"DELETE [dbo].[Producer] WHERE [Id]={id}";
			try {
				using (SqlConnection connection = new SqlConnection(connectionString)) {
					connection.Open();
					SqlCommand command = new SqlCommand(sqlExpression, connection);
					int number = command.ExecuteNonQuery();
					if (number != 0)
						Console.WriteLine($"The element with Id - {id}, was deleted succsessfully from the table - Producer");
					else { Console.WriteLine($"The element with Id - {id}, wasn`t deleted, because there wasn`t in the table - Producer!"); }
				}
			} catch (Exception ex) { Console.WriteLine("We can`t delete this element, from the table Producer, because it has dependency!"); }
		}

		public void DeleteAll() {
			string sqlExpression = $"DELETE [dbo].[Producer]";
			try {
				using (SqlConnection connection = new SqlConnection(connectionString)) {
					connection.Open();
					SqlCommand command = new SqlCommand(sqlExpression, connection);
					int number = command.ExecuteNonQuery();
					if (number != 0)
						Console.WriteLine("{0} elements were deleted succsessfully from the table - Producer", number);
					else { Console.WriteLine("No one element was deleted from the table - Producer, because it was empty!"); }
				}
			} catch (Exception ex) { Console.WriteLine("We can`t delete elements from the table Producer, because they have dependency!"); }
		}

		public IEnumerable<Producer> GetAll() {
			List<Producer> producers = new List<Producer>();
			string sqlExpression = $"SELECT * FROM [dbo].[Producer]";
			using (SqlConnection connection = new SqlConnection(connectionString)) {
				connection.Open();
				SqlCommand command = new SqlCommand(sqlExpression, connection);
				using (SqlDataReader reader = command.ExecuteReader()) {
					if (reader.HasRows) {
						while (reader.Read()) {
							Producer producer = new Producer() {
								Id = Convert.ToInt32(reader["Id"]),
								Name = Convert.ToString(reader["Name"]),
								Country = Convert.ToString(reader["Country"])
							};
							Console.WriteLine(producer);
							producers.Add(producer);
						}
					} else { Console.WriteLine("There isn`t any elements in the table - Producer!"); }
				}
				return producers;
			}
		}

		public Producer Get(int id) {
			string sqlExpression = $"SELECT * FROM [dbo].[Producer] WHERE [Id]={id}";
			Producer producer = new Producer();
			using (SqlConnection connection = new SqlConnection(connectionString)) {
				connection.Open();
				SqlCommand command = new SqlCommand(sqlExpression, connection);
				using (SqlDataReader reader = command.ExecuteReader()) {
					if (reader.HasRows) {
						while (reader.Read()) {
							producer.Id = Convert.ToInt32(reader["Id"]);
							producer.Name = Convert.ToString(reader["Name"]);
							producer.Country = Convert.ToString(reader["Country"]);
							Console.WriteLine(producer);
						}
					} else {
						Console.WriteLine($"There isn`t element with Id - {id} in the table Producer!");
					}
				}
				return producer;
			}
		}

		public void Update(Producer item) {
			string sqlExpression = $"UPDATE [dbo].[Producer] SET [Name]='{item.Name}', [Country]='{item.Country}' WHERE [Id]={item.Id}";
			using (SqlConnection connection = new SqlConnection(connectionString)) {
				connection.Open();
				SqlCommand command = new SqlCommand(sqlExpression, connection);
				int number = command.ExecuteNonQuery();
				if (number != 0)
					Console.WriteLine($"The element with Id - {item.Id} in table Producer was updated succsessfully");
				else { Console.WriteLine($"The element with Id - {item.Id}, wasn`t updated, because there isn`t in the table Producer!"); }
			}
		}
	}
}
