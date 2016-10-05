using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    public class GoodDbWorker
    {
       public static string connectionstring= ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        public Good GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string query = "SELECT Goods.Id, Goods.Name, Goods.Price, Categories.Id, Categories.Name, Producers.Id, Producers.Name, Producers.Country";
                query += " FROM Goods";
                query += " INNER JOIN Categories ON Goods.CategoryId = Categories.Id";
                query += " INNER JOIN Producers ON Goods.ProducerId = Producers.Id";
                query += " WHERE Goods.Id = @Id;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@Id", SqlDbType.Int);
                command.Parameters["@Id"].Value = id;

                SqlDataReader reader = command.ExecuteReader();

                Good res = reader.Read()
                    ? new Good
                    {
                        Id = (int)reader.GetValue(0),
                        Name = reader.GetValue(1).ToString(),
                        Price = (decimal)reader.GetValue(2),
                        Category=new Category {Id= (int)reader.GetValue(3),Name= reader.GetValue(4).ToString() },
                        Producer=new Producer {Id= (int)reader.GetValue(5),Name= reader.GetValue(6).ToString(),Country= reader.GetValue(7).ToString() }
                    }
                    : null;
                reader.Close();

                return res;
            }
        }
        public List<Good> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                var producerworker = new ProducerDbWorker();
                var categoriesworker = new CategoryDbWorker();
                List<Producer> producers = producerworker.GetAll();
                List<Category> categories = categoriesworker.GetAll();
                List<Good> goods = new List<Good>();
                string sql = "SELECT * FROM dbo.Goods";
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
                goods = Manager_FullGoods.Full_Goods(producers, categories, goods);
                return goods;
            }
        }
        public void InsertGood(Good good)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string sqlStatement = "INSERT INTO dbo.Goods(Id,Name,Price,CategoryId,ProducerId) VALUES(@Id,@Name,@Price,@CategoryId,@ProducerId)";
                    using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.Int);
                        cmd.Parameters.Add("@Name", SqlDbType.Text);
                        cmd.Parameters.Add("@Price", SqlDbType.Decimal);
                        cmd.Parameters.Add("@CategoryId", SqlDbType.Int);
                        cmd.Parameters.Add("@ProducerId", SqlDbType.Int);

                        cmd.Parameters["@id"].Value = good.Id;
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
                Console.WriteLine("Error-It's impossible to insert good,check the values!");
            }
        }
        public void InsertGoodsList(List<Good> goods)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string sqlStatement = "INSERT INTO dbo.Goods(Id,Name,Price,CategoryId,ProducerId) VALUES(@Id,@Name,@Price,@CategoryId,@ProducerId)";
                    using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.Int);
                        cmd.Parameters.Add("@Name", SqlDbType.Text);
                        cmd.Parameters.Add("@Price", SqlDbType.Decimal);
                        cmd.Parameters.Add("@CategoryId", SqlDbType.Int);
                        cmd.Parameters.Add("@ProducerId", SqlDbType.Int);

                        foreach (var item in goods)
                        {
                            cmd.Parameters["@id"].Value = item.Id;
                            cmd.Parameters["@Name"].Value = item.Name;
                            cmd.Parameters["@Price"].Value = item.Price;
                            cmd.Parameters["@CategoryId"].Value = item.Category.Id;
                            cmd.Parameters["@ProducerId"].Value = item.Producer.Id;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error-It's impossible to insert list of goods,check the values of keys!");
            }
        }
        public void UpdateGood(int Id, string Name, decimal Price)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string sqlStatement = "UPDATE dbo.Producers SET Name=@Name,Price=@Price WHERE Id=@Id";
                    using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", Id);
                        cmd.Parameters.AddWithValue("@Name", Name);
                        cmd.Parameters.AddWithValue("@Price", Price);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error-It's impossible to update good because of value key");
            }
        }
        public void DeleteGoodById(int Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string sqlStatement = "DELETE FROM dbo.Goods WHERE Id = " + Id.ToString() + "";
                    using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error-it's impossbile to delete good because of values!!");
            }
        }
        public void DeleteGoodWithForeignKeys(int Id)
        {
            try
            {
                int CategoryId = 0;
                int ProducerId = 0;
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string sqlStatement = "SELECT Goods.Id,Goods.CategoryId,Goods.ProducerId FROM dbo.Goods WHERE Goods.Id =@id";
                    using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                    {
                        cmd.Parameters.Add("@Id", SqlDbType.Int);
                        cmd.Parameters["@Id"].Value = Id;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CategoryId = (int)reader.GetValue(1);
                                ProducerId = (int)reader.GetValue(2);
                            }
                        }
                    }
                }

                DeleteGoodById(Id);

                ProducerDbWorker pworker = new ProducerDbWorker();
                pworker.DeleteProducerById(ProducerId);
                CategoryDbWorker cworker = new CategoryDbWorker();
                cworker.DeleteCategoryById(CategoryId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error-it's impossbile to delete good with foreign keys!!");
            }
        }
    }
}
