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
    public class ProducerDbWorker
    {
        public static string connectionstring = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        public Producer GetById(int Id)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string sql = "SELECT Producers.Id,Producers.Name,Producers.Country FROM dbo.Producers WHERE Producers.Id =@Id";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int);
                    cmd.Parameters["@Id"].Value = Id;
                    using (var reader = cmd.ExecuteReader())
                    {

                        Producer res = reader.Read()
                       ? new Producer
                       {
                           Id = (int)reader.GetValue(0),
                           Name = reader.GetValue(1).ToString(),
                           Country = reader.GetValue(2).ToString()
                       }
                       : null;

                        return res;
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
                string sql = "SELECT * FROM dbo.Producers";
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
        public void InsertProducer(Producer producer)

        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string sqlStatement = "INSERT INTO dbo.Producers(Id,Name,Country) VALUES(@Id,@Name,@Country)";
                    using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
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
                Console.WriteLine("Error-It's impossible to insert new producer with these values!");
            }
        }
        public void InsertProducersList(List<Producer> producers)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string sqlStatement = "INSERT INTO dbo.Producers(Id,Name,Country) VALUES(@Id,@Name,@Country)";
                    using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
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
                Console.WriteLine("Error-It's impossible to insert list of Producers with these values!");
            }
        }
        public void UpdateProducer(int Id, string Name, string Country)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string sqlStatement = "UPDATE dbo.Producers SET Name=@Name,Country=@Country WHERE Id=@Id";
                    using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", Id);
                        cmd.Parameters.AddWithValue("@Name", Name);
                        cmd.Parameters.AddWithValue("@Country", Country);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error with updating Producer-Check values!");
            }
        }
        public void DeleteProducerById(int Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string sqlStatement = "DELETE FROM dbo.Producers WHERE Id = " + Id.ToString() + "";
                    using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error-Producer cant be deleted because it's foreign key of Good");
            }
        }

    }
}
