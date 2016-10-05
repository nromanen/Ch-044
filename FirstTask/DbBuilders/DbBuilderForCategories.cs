using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace FirstTask
{
    class DbBuilderForCategories
    {
        static public List<Category> ReadFromDb()
        {
            string commString = "select * from Categories";
            List<Category> resultList = new List<Category>();

            SqlConnection conn = DbConnectionSingleton.GetInstance();
            
            using (SqlCommand cmd = new SqlCommand(commString, conn))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resultList.Add(new Category()
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"]
                    });
                }
                reader.Dispose();
            }
            
            return resultList;
        }
        
        static public void FillToDb(List<Category> list)
        {
            string commString = @"insert into Categories values(@Id,@Name);";

            SqlConnection conn = DbConnectionSingleton.GetInstance();
            
                
                DropCategoriesDb(conn);
                using (SqlCommand cmd = new SqlCommand(commString, conn))
                {
                    foreach (var category in list)
                    {
                        cmd.Parameters.AddWithValue("@Id", category.Id);
                        cmd.Parameters.AddWithValue("@Name", category.Name);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }
                
            
        }

        static public void InsertCategory(Category category)
        {
            string commString = @"insert into Categories values(@Id,@Name);";

            SqlConnection conn = DbConnectionSingleton.GetInstance();

            using (SqlCommand cmd = new SqlCommand(commString, conn))
            {
                cmd.Parameters.AddWithValue("@Id", category.Id);
                cmd.Parameters.AddWithValue("@Name", category.Name);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        static public void DeleteCategoryNotHard(Category category, bool IsHard)
        {
            string commString = @"delete from Categories where Id = @Id";
            if (IsHard)
                DbBuilderForGoods.DeleteGoodsByCategory(category);
            SqlConnection conn = DbConnectionSingleton.GetInstance();
            using (SqlCommand cmd = new SqlCommand(commString, conn))
            {
                cmd.Parameters.AddWithValue("@Id", category.Id);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static public void UpdateCategory(Category category)
        {
            string commString = @"update Categories
                                  set Name = @Name
                                  where Id = @Id;";
            
            SqlConnection conn = DbConnectionSingleton.GetInstance();
            using (SqlCommand cmd = new SqlCommand(commString, conn))
            {
                cmd.Parameters.AddWithValue("@Id", category.Id);
                cmd.Parameters.AddWithValue("@Name", category.Name);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        static public Category GetCategoryById(int id)
        {
            Category category = null;
            string commString = @"select * from Categories where Id = @Id";
            SqlConnection conn = DbConnectionSingleton.GetInstance();
            using (SqlCommand cmd = new SqlCommand(commString, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    try
                    {
                        reader.Read();
                        category = new Category()
                        {
                            Id = id,
                            Name = reader["Name"] as string
                        };
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return category;
        }
        static public void DropCategoriesDb(SqlConnection conn)
        {
            var deleteCmd = new SqlCommand("delete from categories;", conn);
            deleteCmd.ExecuteNonQuery();
        }
    }
}
