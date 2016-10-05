using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2.DBWorkers
{
    public class CategoryDbWorker
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["Task2"].ConnectionString;

        public Category GetById(int Id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Categories WHERE Id =@Id";
                var category = new Category();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int);
                    cmd.Parameters["@Id"].Value = Id;
                    using (var reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            category.Id = reader.GetInt32(0);
                            category.Name = reader.GetString(1);
                        }

                        return category;
                    }
                }
            }
        }

        public List<Category> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                List<Category> categories = new List<Category>();
                string query = "SELECT * FROM Categories";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var category = new Category();
                            category.Id = Convert.ToInt32(reader["Id"]);
                            category.Name = reader["Name"].ToString();
                            categories.Add(category);
                        }
                    }
                }
                return categories;
            }
        }
        public void InsertCategory(Category category)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlStatement = "INSERT INTO Categories(Id,Name) VALUES(@Id,@Name)";
                    using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                    {
                        cmd.Parameters.Add("@Id", SqlDbType.Int);
                        cmd.Parameters.Add("@Name", SqlDbType.Text);

                        cmd.Parameters["@Id"].Value = category.Id;
                        cmd.Parameters["@Name"].Value = category.Name;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void InsertCategoriesList(List<Category> categories)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Categories(Id,Name) VALUES(@Id,@Name)";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.Add("@Id", SqlDbType.Int);
                        cmd.Parameters.Add("@Name", SqlDbType.Text);

                        foreach (var category in categories)
                        {
                            cmd.Parameters["@Id"].Value = category.Id;
                            cmd.Parameters["@Name"].Value = category.Name;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void UpdateCategory(int Id, string Name)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Categories SET Name=@Name WHERE Id=@Id";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", Id);
                        cmd.Parameters.AddWithValue("@Name", Name);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void DeleteCategoryById(int Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Categories WHERE Id = " + Id.ToString() + "";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

