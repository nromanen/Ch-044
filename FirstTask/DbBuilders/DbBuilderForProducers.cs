using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTask
{
    class DbBuilderForProducers
    {
        static public List<Producer> ReadFromDb()
        {
            string commString = "select * from Producers";
            List<Producer> resultList = new List<Producer>();

            SqlConnection conn = DbConnectionSingleton.GetInstance();
            
                
                using (SqlCommand cmd = new SqlCommand(commString, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        resultList.Add(new Producer()
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Country = (string)reader["Country"]
                        });
                    }
                    reader.Dispose();
                }
                
            
            return resultList;
        }

        static public void FillToDb(List<Producer> list)
        {
            string commString = @"insert into Producers values(@Id,@Name,@Country);";

            SqlConnection conn = DbConnectionSingleton.GetInstance();
            
                
                DropProducersDb(conn);
                using (SqlCommand cmd = new SqlCommand(commString, conn))
                {

                    foreach (var producer in list)
                    {
                        cmd.Parameters.AddWithValue("@Id", producer.Id);
                        cmd.Parameters.AddWithValue("@Name", producer.Name);
                        cmd.Parameters.AddWithValue("@Country", producer.Country);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }
                
            
        }

        static public void InsertProducer(Producer producer)
        {
            string commString = @"insert into Producers values(@Id,@Name,@Country);";

            SqlConnection conn = DbConnectionSingleton.GetInstance();
            using (SqlCommand cmd = new SqlCommand(commString, conn))
            {
                    cmd.Parameters.AddWithValue("@Id", producer.Id);
                    cmd.Parameters.AddWithValue("@Name", producer.Name);
                    cmd.Parameters.AddWithValue("@Country", producer.Country);
                    cmd.ExecuteNonQuery();
            }
        }

        static public void DeleteProducerNotHard(Producer producer, bool IsHard)
        {
            string commString = @"delete from Producers where Id = @Id";
            if (IsHard)
                DbBuilderForGoods.DeleteGoodsByProducer(producer);
            SqlConnection conn = DbConnectionSingleton.GetInstance();
            using (SqlCommand cmd = new SqlCommand(commString, conn))
            {
                cmd.Parameters.AddWithValue("@Id", producer.Id);
                cmd.ExecuteNonQuery();
            }
        }

        static public void UpdateProducer(Producer producer)
        {
            string commString = @"update Producers
                                  set Name = @Name,
                                      Country = @Country
                                  where Id = @Id;";

            SqlConnection conn = DbConnectionSingleton.GetInstance();
            using (SqlCommand cmd = new SqlCommand(commString, conn))
            {
                cmd.Parameters.AddWithValue("@Id", producer.Id);
                cmd.Parameters.AddWithValue("@Name", producer.Name);
                cmd.Parameters.AddWithValue("@Country", producer.Country);
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
        static public Producer GetProducerById(int id)
        {
            Producer producer = null;
            string commString = @"select * from Producers where Id = @Id";
            SqlConnection conn = DbConnectionSingleton.GetInstance();
            using (SqlCommand cmd = new SqlCommand(commString, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    try
                    {
                        reader.Read();
                        producer = new Producer()
                        {
                            Id = id,
                            Name = reader["Name"] as string,
                            Country = reader["Country"] as string
                        };
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return producer;
        }
        static public void DropProducersDb(SqlConnection conn)
        {
            var deleteCmd = new SqlCommand("delete from producers;", conn);
            deleteCmd.ExecuteNonQuery();
        }
    }
}
