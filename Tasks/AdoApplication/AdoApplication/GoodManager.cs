using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoApplication {
	class GoodManager : BaseManager, IRepository<Good> {
		public void Create(Good item) {
			string sqlExpression = $"INSERT INTO [dbo].[Good] VALUES ({item.Id},'{item.Name}', {item.Price}, {item.Category.Id}, {item.Producer.Id})";
			try {
				using (SqlConnection connection = new SqlConnection(connectionString)) {
					connection.Open();
					SqlCommand command = new SqlCommand(sqlExpression, connection);
					int number = command.ExecuteNonQuery();
					if (number != 0)
						Console.WriteLine("The element was added succsessfully to the table - Good");
				}
			} catch (Exception) {
				Console.WriteLine("The element wasn`t added, because it was in the table Good or CategoryId / ProducerId not valid!");
			}
		}

		public void Delete(int id) {
			string sqlExpression = $"DELETE [dbo].[Good] WHERE [Id]={id}";

			using (SqlConnection connection = new SqlConnection(connectionString)) {
				connection.Open();
				SqlCommand command = new SqlCommand(sqlExpression, connection);
				int number = command.ExecuteNonQuery();
				if (number != 0)
					Console.WriteLine($"The element with Id - {id}, was deleted succsessfully from the table - Good");
				else { Console.WriteLine($"The element with Id - {id}, wasn`t deleted, because there wasn`t in the table - Good!"); }
			}

		}

		public void DeleteAll() {
			string sqlExpression = $"DELETE [dbo].[Good]";
			using (SqlConnection connection = new SqlConnection(connectionString)) {
				connection.Open();
				SqlCommand command = new SqlCommand(sqlExpression, connection);
				int number = command.ExecuteNonQuery();
				if (number != 0)
					Console.WriteLine("{0} elements were deleted succsessfully from the table - Good", number);
				else { Console.WriteLine("No one element was deleted from the table - Good, because it was empty!"); }
			}
		}

		public IEnumerable<Good> GetAll() {
			List<Good> goods = new List<Good>();
			string sqlExpression = "SELECT [G].[Id], [G].[Name], [G].[Price],";
			sqlExpression += " [C].[Id] AS [CategoryId], [C].[Name] AS [CategoryName],";
			sqlExpression += " [P].[Id] AS [ProducerId], [P].[Name] AS [ProducerName], [P].[Country] AS [ProducerCountry]";
			sqlExpression += " FROM Good [G]";
			sqlExpression += " INNER JOIN Category [C] ON [C].[Id] = [G].[CategoryId]";
			sqlExpression += " INNER JOIN Producer [P] ON [P].[Id] = [G].[ProducerId]";
			using (SqlConnection connection = new SqlConnection(connectionString)) {
				connection.Open();
				SqlCommand command = new SqlCommand(sqlExpression, connection);
				using (SqlDataReader reader = command.ExecuteReader()) {
					if (reader.HasRows) {
						while (reader.Read()) {
							Good good = new Good() {
								Id = Convert.ToInt32(reader["Id"]),
								Name = Convert.ToString(reader["Name"]),
								Price = Convert.ToDecimal(reader["Price"]),
								Category = new Category {
									Id = Convert.ToInt32(reader["CategoryId"]),
									Name = Convert.ToString(reader["CategoryName"])
								},
								Producer = new Producer {
									Id = Convert.ToInt32(reader["ProducerId"]),
									Name = Convert.ToString(reader["ProducerName"]),
									Country = Convert.ToString(reader["ProducerCountry"])
								}
							};
							Console.WriteLine(good);
							goods.Add(good);
						}
					} else { Console.WriteLine("There isn`t any elements in the table - Good!"); }
				}
				return goods;
			}
		}

		public Good Get(int id) {
			string sqlExpression = "SELECT [G].[Id], [G].[Name], [G].[Price],";
			sqlExpression += " [C].[Id] AS [CategoryId], [C].[Name] AS [CategoryName],";
			sqlExpression += " [P].[Id] AS [ProducerId], [P].[Name] AS [ProducerName], [P].[Country] AS [ProducerCountry]";
			sqlExpression += " FROM Good [G]";
			sqlExpression += " INNER JOIN Category [C] ON [C].[Id] = [G].[CategoryId]";
			sqlExpression += " INNER JOIN Producer [P] ON [P].[Id] = [G].[ProducerId]";
			sqlExpression += $" WHERE [G].[Id] = {id}";
			Good good = new Good();
			using (SqlConnection connection = new SqlConnection(connectionString)) {
				connection.Open();
				SqlCommand command = new SqlCommand(sqlExpression, connection);
				using (SqlDataReader reader = command.ExecuteReader()) {
					if (reader.HasRows) {
						while (reader.Read()) {
							good.Id = Convert.ToInt32(reader["Id"]);
							good.Name = Convert.ToString(reader["Name"]);
							good.Price = Convert.ToDecimal(reader["Price"]);
							good.Category = new Category {
								Id = Convert.ToInt32(reader["CategoryId"]),
								Name = Convert.ToString(reader["CategoryName"])
							};
							good.Producer = new Producer {
								Id = Convert.ToInt32(reader["ProducerId"]),
								Name = Convert.ToString(reader["ProducerName"]),
								Country = Convert.ToString(reader["ProducerCountry"])
							};
							Console.WriteLine(good);
						}
					} else { Console.WriteLine($"There isn`t element with Id - {id} in the table Good!"); }
				}
				return good;
			}
		}

		public void Update(Good item) {
			string sqlExpression = $"UPDATE [dbo].[Good] SET [Name]='{item.Name}', [Price]={item.Price}, [CategoryId]={item.Category.Id}, [ProducerId]={item.Producer.Id} WHERE [Id]={item.Id}";
			using (SqlConnection connection = new SqlConnection(connectionString)) {
				connection.Open();
				SqlCommand command = new SqlCommand(sqlExpression, connection);
				int number = command.ExecuteNonQuery();
				if (number != 0)
					Console.WriteLine($"The element with Id - {item.Id} in table Good was updated succsessfully");
				else { Console.WriteLine($"The element with Id - {item.Id}, wasn`t updated, because there isn`t in the table Good!"); }
			}
		}
	}
}
