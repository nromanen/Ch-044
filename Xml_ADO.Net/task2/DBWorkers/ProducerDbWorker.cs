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
    public class ProducerDbWorker
    {
        public static string connectionstring = ConfigurationManager.ConnectionStrings["Task2"].ConnectionString;

        public Producer GetById(int Id)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string query = "SELECT * FROM dbo.Producers WHERE Id =@Id";
                var producer = new Producer();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int);
                    cmd.Parameters["@Id"].Value = Id;
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            producer.Id = reader.GetInt32(0);
                            producer.Name = reader.GetString(1);
                            producer.Country = reader.GetString(2);
                        }


                        return producer;
                    }
                }
            }
        }

        public List<Producer> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                List<Producer> producers = new List<Producer>();
                string sql = "SELECT * FROM Producers";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var producer = new Producer();
                            producer.Id = Convert.ToInt32(reader["Id"]);
                            producer.Name = reader["Name"].ToString();
                            producer.Country = reader["Country"].ToString();
                            producers.Add(producer);
                        }
                    }
                }

                return producers;
            }
        }
        public void CreateProducer(Producer producer)

        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string query = "INSERT INTO Producers(Id,Name,Country) VALUES(@Id,@Name,@Country)";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.Int);
                        cmd.Parameters.Add("@Name", SqlDbType.Text);
                        cmd.Parameters.Add("@Country", SqlDbType.Text);

                        cmd.Parameters["@id"].Value = producer.Id;
                        cmd.Parameters["@Name"].Value = producer.Name;
                        cmd.Parameters["@Country"].Value = producer.Country;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void CreateProducersList(List<Producer> producers)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string query = "INSERT INTO Producers(Id,Name,Country) VALUES(@Id,@Name,@Country)";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {

                        cmd.Parameters.Add("@id", SqlDbType.Int);
                        cmd.Parameters.Add("@Name", SqlDbType.Text);
                        cmd.Parameters.Add("@Country", SqlDbType.Text);

                        foreach (var prod in producers)
                        {
                            cmd.Parameters["@id"].Value = prod.Id;
                            cmd.Parameters["@Name"].Value = prod.Name;
                            cmd.Parameters["@Country"].Value = prod.Country;
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
        public void UpdateProducer(Producer producer)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string query = "UPDATE Producers SET Name=@Name,Country=@Country WHERE Id=@Id";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", producer.Id);
                        cmd.Parameters.AddWithValue("@Name", producer.Name);
                        cmd.Parameters.AddWithValue("@Country", producer.Country);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void DeleteProducerById(int Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string query = "DELETE FROM Producers WHERE Id = " + Id.ToString() + "";
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
