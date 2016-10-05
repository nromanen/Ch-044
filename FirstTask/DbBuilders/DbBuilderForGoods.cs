using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTask
{
    class DbBuilderForGoods
    {
        static public List<Good> ReadFromDb()
        {
            string commString = "select * from Goods";
            List<Good> resultList = new List<Good>();

            SqlConnection conn = DbConnectionSingleton.GetInstance();
            
                
                using (SqlCommand cmd = new SqlCommand(commString, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        resultList.Add(new Good()
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Price = (decimal)reader["Price"],
                            Category = new Category() { Id = (int)reader["CategoryId"]},
                            Producer = new Producer() { Id = (int)reader["ProducerId"]}
                        });
                    }
                    reader.Dispose();
                }
                
            
            return resultList;
        }

        static public void FillToDb(List<Good> list)
        {
            string commString = @"insert into Goods values(@Id,@Name,@CategoryId,@ProducerId,@Price);";

            SqlConnection conn = DbConnectionSingleton.GetInstance();
            
                
                DropGoodsDb(conn);
                using (SqlCommand cmd = new SqlCommand(commString, conn))
                {
                    foreach (var good in list)
                    {
                        cmd.Parameters.AddWithValue("@Id", good.Id);
                        cmd.Parameters.AddWithValue("@Name", good.Name);
                        cmd.Parameters.AddWithValue("@CategoryId", good.Category.Id);
                        cmd.Parameters.AddWithValue("@ProducerId", good.Producer.Id);
                        cmd.Parameters.AddWithValue("@Price", good.Price);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }
            
        }

        static public void DropGoodsDb(SqlConnection conn)
        {
            var deleteCmd = new SqlCommand("delete from goods;", conn);
            deleteCmd.ExecuteNonQuery();
        }
        static public void InsertGood(Good good)
        {
            string commString = @"insert into Goods values(@Id,@Name,@CategoryId,@ProducerId,@Price);";

            SqlConnection conn = DbConnectionSingleton.GetInstance();

            using (SqlCommand cmd = new SqlCommand(commString, conn))
            {
                    cmd.Parameters.AddWithValue("@Id", good.Id);
                    cmd.Parameters.AddWithValue("@Name", good.Name);
                    cmd.Parameters.AddWithValue("@CategoryId", good.Category.Id);
                    cmd.Parameters.AddWithValue("@ProducerId", good.Producer.Id);
                    cmd.Parameters.AddWithValue("@Price", good.Price);
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

        static public void DeleteGood(Good good)
        {
            string commString = @"delete from Goods where Id = @Id";

            SqlConnection conn = DbConnectionSingleton.GetInstance();
            using (SqlCommand cmd = new SqlCommand(commString, conn))
            {
                cmd.Parameters.AddWithValue("@Id", good.Id);
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
        static public void UpdateGood(Good good)
        {
            string commString = @"update Goods
                                  set Name = @Name,
                                      CategoryId = @CategoryId,
                                      ProducerId = @ProducerId,
                                      Price = @Price
                                  where Id = @Id;";

            SqlConnection conn = DbConnectionSingleton.GetInstance();
            using (SqlCommand cmd = new SqlCommand(commString, conn))
            {
                cmd.Parameters.AddWithValue("@Id", good.Id);
                cmd.Parameters.AddWithValue("@Name", good.Name);
                cmd.Parameters.AddWithValue("@CategoryId", good.Category.Id);
                cmd.Parameters.AddWithValue("@ProducerId", good.Producer.Id);
                cmd.Parameters.AddWithValue("@Price", good.Price);
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
        static public Good GetGoodById(int id)
        {
            Good good = null;
            string commString = @"select * from Goods where Id = @Id";
            SqlConnection conn = DbConnectionSingleton.GetInstance();
            using (SqlCommand cmd = new SqlCommand(commString, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    try
                    {
                        reader.Read();
                        good = new Good()
                        {
                            Id = id,
                            Name = reader["Name"] as string,
                            Category = new Category() { Id = (int)reader["CategoryId"]},
                            Producer = new Producer() { Id = (int)reader["ProducerId"]},
                            Price = (decimal)reader["Price"]

                        };
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return good;
        }

        static public void DeleteGoodsByCategory(Category category)
        {
            string commString = "delete from Goods where CategoryId = @CategoryId";
            
            SqlConnection conn = DbConnectionSingleton.GetInstance();
            using (SqlCommand cmd = new SqlCommand(commString, conn))
            {
                cmd.Parameters.AddWithValue("@CategoryId", category.Id);
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
        static public void DeleteGoodsByProducer(Producer producer)
        {
            string commString = "delete from Goods where ProducerId = @ProducerId";

            SqlConnection conn = DbConnectionSingleton.GetInstance();
            using (SqlCommand cmd = new SqlCommand(commString, conn))
            {
                cmd.Parameters.AddWithValue("@ProducerId", producer.Id);
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
        static public List<Good> GetGoodsByCategory(Category category)
        {
            List<Good> goods = new List<Good>();
            string commString = @"select * from Goods where CategoryId = @CategoryId";
            SqlConnection conn = DbConnectionSingleton.GetInstance();
            using (SqlCommand cmd = new SqlCommand(commString, conn))
            {
                cmd.Parameters.AddWithValue("@CategoryId", category.Id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    try
                    {
                        while (reader.Read())
                        {
                            goods.Add(new Good()
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"] as string,
                                Category = category,
                                Producer = new Producer() { Id = (int)reader["ProducerId"] },
                                Price = (decimal)reader["Price"]

                            });
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return goods;
        }

        static public List<Good> GetGoodsByProducer(Producer producer)
        {
            List<Good> goods = new List<Good>();
            string commString = @"select * from Goods where ProducerId = @ProducerId";
            SqlConnection conn = DbConnectionSingleton.GetInstance();
            using (SqlCommand cmd = new SqlCommand(commString, conn))
            {
                cmd.Parameters.AddWithValue("@ProducerId", producer.Id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    try
                    {
                        while (reader.Read())
                        {
                            goods.Add(new Good()
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"] as string,
                                Category = new Category() { Id = (int)reader["CategoryId"]},
                                Producer = producer,
                                Price = (decimal)reader["Price"]

                            });
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return goods;
        }

        static public void DropGoodsDb()
        {
            SqlConnection conn = DbConnectionSingleton.GetInstance();
            var deleteCmd = new SqlCommand("delete from goods;", conn);
            deleteCmd.ExecuteNonQuery();
        }
    }
}
