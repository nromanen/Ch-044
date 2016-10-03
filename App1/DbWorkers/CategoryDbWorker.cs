using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    public  class CategoryDbWorker:ConnectionManager
    {
        public List<Category> GetAll()
        {
            List<Category> categories = new List<Category>();
            string sql = "SELECT * FROM dbo.Categories";
            using (SqlCommand cmd = new SqlCommand(sql, con))
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
        public void InsertCategory(Category category)
        {
            try
            {
                string sqlStatement = "INSERT INTO dbo.Categories(Id,Name) VALUES(@Id,@Name)";
                using (SqlCommand cmd = new SqlCommand(sqlStatement, con))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters.Add("@Name", SqlDbType.Text);

                    cmd.Parameters["@id"].Value = category.Id;
                    cmd.Parameters["@Name"].Value = category.Name;

                    cmd.ExecuteNonQuery();
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
                string sqlStatement = "INSERT INTO dbo.Categories(Id,Name) VALUES(@Id,@Name)";
                using (SqlCommand cmd = new SqlCommand(sqlStatement, con))
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
            catch(Exception ex)
            {
                Console.WriteLine("Error-it's imposssible to insert categories list to database,check the values!");
            }
        }
        public void UpdateCategory(int Id, string Name)
        {
            try
            {
                string sqlStatement = "UPDATE dbo.Categories SET Name=@Name WHERE Id=@Id";
                using (SqlCommand cmd = new SqlCommand(sqlStatement, con))
                {
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.ExecuteNonQuery();
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
                string sqlStatement = "DELETE FROM dbo.Categories WHERE Id = " + Id.ToString() + "";
                using (SqlCommand cmd = new SqlCommand(sqlStatement, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Category cant be deleted because it's a foreign key of Good.");
            }
        }
    }
}
