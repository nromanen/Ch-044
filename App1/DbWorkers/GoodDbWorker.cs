using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    public class GoodDbWorker:ConnectionManager
    {
        public List<Good> GetAll()
        {
            var producerworker = new ProducerDbWorker();
            var categoriesworker = new CategoryDbWorker();
            List<Producer> producers = producerworker.GetAll();
            List<Category> categories = categoriesworker.GetAll();
            List<Good> goods = new List<Good>();
            string sql = "SELECT * FROM dbo.Goods";
            using (SqlCommand cmd = new SqlCommand(sql, con))
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
        public void InsertGood(Good good)
        {
            try
            {
                string sqlStatement = "INSERT INTO dbo.Goods(Id,Name,Price,CategoryId,ProducerId) VALUES(@Id,@Name,@Price,@CategoryId,@ProducerId)";
                using (SqlCommand cmd = new SqlCommand(sqlStatement, con))
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
            catch(Exception ex)
            {
                Console.WriteLine("Error-It's impossible to insert good,check the values!");
            }
        }
        public void InsertGoodsList(List<Good> goods)
        {
            try
            {
                string sqlStatement = "INSERT INTO dbo.Goods(Id,Name,Price,CategoryId,ProducerId) VALUES(@Id,@Name,@Price,@CategoryId,@ProducerId)";
                using (SqlCommand cmd = new SqlCommand(sqlStatement, con))
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
            catch(Exception ex)
            {
                Console.WriteLine("Error-It's impossible to insert list of goods,check the values of keys!");
            }
        }
        public void UpdateGood(int Id, string Name, decimal Price)
        {
            try
            {
                string sqlStatement = "UPDATE dbo.Producers SET Name=@Name,Price=@Price WHERE Id=@Id";
                using (SqlCommand cmd = new SqlCommand(sqlStatement, con))
                {
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@Price", Price);
                    cmd.ExecuteNonQuery();
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
                string sqlStatement = "DELETE FROM dbo.Goods WHERE Id = " + Id.ToString() + "";
                using (SqlCommand cmd = new SqlCommand(sqlStatement, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error-it's impossbile to delete good because of values!!");
            }
        }
    }
}
