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
    public class GoodDbWorker
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["Task2"].ConnectionString;

        public Good GetById(int id)
        {
            string query = "SELECT [G].[Id], [G].[Name], [G].[Price],";
            query += " [C].[Id] AS [CategoryId], [C].[Name] AS [CategoryName],";
            query += " [P].[Id] AS [ProducerId], [P].[Name] AS [ProducerName], [P].[Country] AS [ProducerCountry]";
            query += " FROM Goods [G]";
            query += " INNER JOIN Categories [C] ON [C].[Id] = [G].[CategoryId]";
            query += " INNER JOIN Producers [P] ON [P].[Id] = [G].[ProducerId]";
            query += $" WHERE [G].[Id] = {id}";
            Good good = new Good();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            good.Id = Convert.ToInt32(reader["Id"]);
                            good.Name = Convert.ToString(reader["Name"]);
                            good.Price = Convert.ToDecimal(reader["Price"]);
                            good.Category = new Category
                            {
                                Id = Convert.ToInt32(reader["CategoryId"]),
                                Name = Convert.ToString(reader["CategoryName"])
                            };
                            good.Producer = new Producer
                            {
                                Id = Convert.ToInt32(reader["ProducerId"]),
                                Name = Convert.ToString(reader["ProducerName"]),
                                Country = Convert.ToString(reader["ProducerCountry"])
                            };
                           // Console.WriteLine(good);
                        }
                    }
                    else { Console.WriteLine($"There isn`t element with Id - {id} in the table Good!"); }
                }
                return good;
            }
        }
        public List<Good> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var producerworker = new ProducerDbWorker();
                var categoriesworker = new CategoryDbWorker();
                List<Producer> producers = producerworker.GetAll();
                List<Category> categories = categoriesworker.GetAll();
                List<Good> goods = new List<Good>();
                var mngr = new Manager();
                string sql = "SELECT * FROM Goods";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var good = new Good();
                            good.Id = Convert.ToInt32(reader["Id"]);
                            good.Name = reader["Name"].ToString();
                            good.Price = Convert.ToDecimal(reader["Price"]);
                            good.Category = new Category()
                            {
                                Id = Convert.ToInt32(reader["CategoryId"])
                            };
                            good.Producer = new Producer()
                            {
                                Id = Convert.ToInt32(reader["ProducerId"])
                            };

                            goods.Add(good);
                        }
                    }
                }
                goods = mngr.GoodsBuilder(goods, producers, categories);
                return goods;
            }
        }
        public void AddGood(Good good)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlStatement = "INSERT INTO Goods(Id,Name,Price,CategoryId,ProducerId) VALUES(@Id,@Name,@Price,@CategoryId,@ProducerId)";
                    using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                    {
                        cmd.Parameters.Add("@Id", SqlDbType.Int);
                        cmd.Parameters.Add("@Name", SqlDbType.Text);
                        cmd.Parameters.Add("@Price", SqlDbType.Decimal);
                        cmd.Parameters.Add("@CategoryId", SqlDbType.Int);
                        cmd.Parameters.Add("@ProducerId", SqlDbType.Int);

                        cmd.Parameters["@Id"].Value = good.Id;
                        cmd.Parameters["@Name"].Value = good.Name;
                        cmd.Parameters["@Price"].Value = good.Price;
                        cmd.Parameters["@CategoryId"].Value = good.Category.Id;
                        cmd.Parameters["@ProducerId"].Value = good.Producer.Id;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void InsertGoodsList(List<Good> goods)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlStatement = "INSERT INTO Goods(Id,Name,Price,CategoryId,ProducerId) VALUES(@Id,@Name,@Price,@CategoryId,@ProducerId)";
                    using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                    {
                        cmd.Parameters.Add("@Id", SqlDbType.Int);
                        cmd.Parameters.Add("@Name", SqlDbType.Text);
                        cmd.Parameters.Add("@Price", SqlDbType.Decimal);
                        cmd.Parameters.Add("@CategoryId", SqlDbType.Int);
                        cmd.Parameters.Add("@ProducerId", SqlDbType.Int);

                        foreach (var good in goods)
                        {
                            cmd.Parameters["@Id"].Value = good.Id;
                            cmd.Parameters["@Name"].Value = good.Name;
                            cmd.Parameters["@Price"].Value = good.Price;
                            cmd.Parameters["@CategoryId"].Value = good.Category.Id;
                            cmd.Parameters["@ProducerId"].Value = good.Producer.Id;
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
        public void UpdateGood(Good good)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Goods SET Id=@Id, Name=@Name,Price=@Price, CategoryId=@CategoryId, ProducerId=@ProducerId WHERE Id=@Id";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", good.Id);
                        cmd.Parameters.AddWithValue("@Name", good.Name);
                        cmd.Parameters.AddWithValue("@Price", good.Price);
                        cmd.Parameters.AddWithValue("@CategoryId", good.Category.Id);
                        cmd.Parameters.AddWithValue("@ProducerId", good.Producer.Id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void DeleteGoodById(int Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Goods WHERE Id = " + Id.ToString() + "";
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
        public void DeleteCategoryWithGoods(int Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Goods WHERE CategoryId =" + Id.ToString() + "";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                CategoryDbWorker cworker = new CategoryDbWorker();
                cworker.DeleteCategoryById(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

