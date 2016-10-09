using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoApplication {
	class CategoryManager : BaseManager, IRepository<Category> {
		public void Create(Category item) {
			string sqlExpression = $"INSERT INTO [dbo].[Category] VALUES ({item.Id},'{item.Name}')";
			try {
				using (SqlConnection connection = new SqlConnection(connectionString)) {
					connection.Open();
					SqlCommand command = new SqlCommand(sqlExpression, connection);
					int number = command.ExecuteNonQuery();
					if (number != 0)
						Console.WriteLine("The element was added succsessfully to the table - Category");
				}
			} catch (Exception) {
				Console.WriteLine("The element wasn`t added, because it was in the table Category!");
			}
		}

		public void Delete(int id) {
			string sqlExpression = $"DELETE [dbo].[Category] WHERE [Id]={id}";
			try {
				using (SqlConnection connection = new SqlConnection(connectionString)) {
					connection.Open();
					SqlCommand command = new SqlCommand(sqlExpression, connection);
					int number = command.ExecuteNonQuery();
					if (number != 0)
						Console.WriteLine("The element was deleted succsessfully from the table - Category");
					else { Console.WriteLine($"The element with Id - {id}, wasn`t deleted, because there wasn`t in the table - Category!"); }
				}
			} catch (Exception ex) { Console.WriteLine("We can`t delete this element from the table Category, because it has dependency!"); }


		}

		public void DeleteAll() {
			string sqlExpression = $"DELETE [dbo].[Category]";
			try {
				using (SqlConnection connection = new SqlConnection(connectionString)) {
					connection.Open();
					SqlCommand command = new SqlCommand(sqlExpression, connection);
					int number = command.ExecuteNonQuery();
					if (number != 0)
						Console.WriteLine("{0} elements were deleted succsessfully from the table - Category", number);
					else { Console.WriteLine("No one element was deleted from the table - Category, because it was empty!"); }
				}
			} catch (Exception ex) { Console.WriteLine("We can`t delete elements from the table Category, because they have dependency!"); }
		}

		public IEnumerable<Category> GetAll() {
			List<Category> categories = new List<Category>();
			string sqlExpression = $"SELECT * FROM [dbo].[Category]";
			using (SqlConnection connection = new SqlConnection(connectionString)) {
				connection.Open();
				SqlCommand command = new SqlCommand(sqlExpression, connection);
				using (SqlDataReader reader = command.ExecuteReader()) {
					if (reader.HasRows) {
						while (reader.Read()) {
							Category category = new Category() {
								Id = Convert.ToInt32(reader["Id"]),
								Name = Convert.ToString(reader["Name"])
							};
							Console.WriteLine(category);
							categories.Add(category);
						}
					} else { Console.WriteLine("There isn`t any elements in the table - Category!"); }
				}
				return categories;
			}
		}

		public Category Get(int id) {
			string sqlExpression = $"SELECT * FROM [dbo].[Category] WHERE [Id]={id}";
			Category category = new Category();
			using (SqlConnection connection = new SqlConnection(connectionString)) {
				connection.Open();
				SqlCommand command = new SqlCommand(sqlExpression, connection);
				using (SqlDataReader reader = command.ExecuteReader()) {
					if (reader.HasRows) {
						while (reader.Read()) {
							category.Id = Convert.ToInt32(reader["Id"]);
							category.Name = Convert.ToString(reader["Name"]);
							Console.WriteLine(category);
						}
					} else {
						Console.WriteLine("There isn`t Category with Id like this!");
					}
				}
				return category;
			}
		}

		public void Update(Category item) {
			string sqlExpression = $"UPDATE [dbo].[Category] SET [Name]='{item.Name}' WHERE [Id]={item.Id}";
			using (SqlConnection connection = new SqlConnection(connectionString)) {
				connection.Open();
				SqlCommand command = new SqlCommand(sqlExpression, connection);
				int number = command.ExecuteNonQuery();
				if (number != 0)
					Console.WriteLine($"The element with Id - {item.Id} in table Category was updated succsessfully");
				else { Console.WriteLine($"The element with Id - {item.Id}, wasn`t updated, because there isn`t in the table Category!"); }
			}
		}
		public void DeleteCategoriesWithGoods(int id) {
			string sqlExpression = $"DELETE [dbo].[Good] WHERE [CategoryId] = {id}";
			CategoryManager categoryManager = new CategoryManager();
			using (SqlConnection connection = new SqlConnection(connectionString)) {
				connection.Open();
				SqlCommand command = new SqlCommand(sqlExpression, connection);
				int number = command.ExecuteNonQuery();
				if (number != 0)
					Console.WriteLine("{0} elements were deleted succsessfully from the table - Good", number);
				else { Console.WriteLine($"No one element was deleted from the table - Good, because there aren`t any element with CategoryId - {id}!"); }
				categoryManager.Delete(id);
			}
		}
	}
}
