using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
   public class ProducerDbWorker:ConnectionManager
    {
        public List<Producer> GetAll()
        {
            List<Producer> producers = new List<Producer>();
            string sql = "SELECT * FROM dbo.Producers";
            using (SqlCommand cmd = new SqlCommand(sql, con))
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
        public void InsertProducer(Producer producer)

        {
            try
            {
                string sqlStatement = "INSERT INTO dbo.Producers(Id,Name,Country) VALUES(@Id,@Name,@Country)";
                using (SqlCommand cmd = new SqlCommand(sqlStatement, con))
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
            catch(Exception ex)
            {
                Console.WriteLine("Error-It's impossible to insert new producer with these values!");
            }
        }
        public void InsertProducersList(List<Producer> producers)
        {
            try
            {
                string sqlStatement = "INSERT INTO dbo.Producers(Id,Name,Country) VALUES(@Id,@Name,@Country)";
                using (SqlCommand cmd = new SqlCommand(sqlStatement, con))
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
            catch(Exception ex)
            {
                Console.WriteLine("Error-It's impossible to insert list of Producers with these values!");
            }
        }
        public void UpdateProducer(int Id, string Name, string Country)
        {
            try
            {

                string sqlStatement = "UPDATE dbo.Producers SET Name=@Name,Country=@Country WHERE Id=@Id";
                using (SqlCommand cmd = new SqlCommand(sqlStatement, con))
                {
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@Country", Country);
                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error with updating Producer-Check values!");
            }
        }
        public void DeleteProducerById(int Id)
        {
            try
            {
                string sqlStatement = "DELETE FROM dbo.Producers WHERE Id = " + Id.ToString() + "";
                using (SqlCommand cmd = new SqlCommand(sqlStatement, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error-Producer cant be deleted because it's foreign key of Good");
            }
        }

    }
}
