using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goods.DbModels;

namespace Goods.Managers
{
    public  class CategoryDbWorker
    {
        public static string connectionstring = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        public Category GetById(int Id)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string sql = "SELECT Categories.Id,Categories.Name FROM dbo.Categories WHERE Categories.Id =@Id";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int);
                    cmd.Parameters["@Id"].Value = Id;
                    using (var reader = cmd.ExecuteReader())
                    {

                        Category res = reader.Read()
                       ? new Category
                       {
                           Id = (int)reader.GetValue(0),
                           Name = reader.GetValue(1).ToString(),
                       }
                       : null;

                        return res;
                    }
                }
            }
        }

        public List<Category> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                List<Category> categories = new List<Category>();
                string sql = "SELECT * FROM dbo.Categories";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
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

        internal void InsertCategory()
        {
            throw new NotImplementedException();
        }

        public void InsertCategory(Category category)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string sqlStatement = "INSERT INTO dbo.Categories(Id,Name) VALUES(@Id,@Name)";
                    using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.Int);
                        cmd.Parameters.Add("@Name", SqlDbType.Text);

                        cmd.Parameters["@id"].Value = category.Id;
                        cmd.Parameters["@Name"].Value = category.Name;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error-it's impossible to insert category,check the values!");
            }
        }
        public void InsertCategoriesList(List<Category> categories)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string sqlStatement = "INSERT INTO dbo.Categories(Id,Name) VALUES(@Id,@Name)";
                    using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.Int);
                        cmd.Parameters.Add("@Name", SqlDbType.Text);

                        foreach (var cat in categories)
                        {
                            cmd.Parameters["@id"].Value = cat.Id;
                            cmd.Parameters["@Name"].Value = cat.Name;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error-it's imposssible to insert categories list to database,check the values!");
            }
        }
        public void UpdateCategory(Category category)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string sqlStatement = "UPDATE dbo.Categories SET Name=@Name WHERE Id=@Id";
                    using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", category.Id);
                        cmd.Parameters.AddWithValue("@Name", category.Name);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error-It's impossible to update Category check the values!");
            }
        }
        public void DeleteCategoryById(int Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string sqlStatement = "DELETE FROM dbo.Categories WHERE Id = " + Id.ToString() + "";
                    using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Category cant be deleted because it's a foreign key of Good.");
            }
        }
        public void DeleteCategoryWithGoods(int Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string sqlStatement = "DELETE FROM dbo.Goods WHERE CategoryId =" + Id.ToString() + "";
                    using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                CategoryDbWorker cworker = new CategoryDbWorker();
                cworker.DeleteCategoryById(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error-it's impossbile to delete good with foreign keys!!");
            }
        }
    }
}
