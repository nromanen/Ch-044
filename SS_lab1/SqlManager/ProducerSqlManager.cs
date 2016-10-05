using SS_lab1.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SS_lab1.SqlManager
{
    class ProducerSqlManager : SqlManager
    {        
        public ProducerSqlManager() : base() { }
        public ProducerSqlManager(string connectionStr) : base(connectionStr) { }        

        public void Create(Producer producer)
        {
            string sqlExpression = "INSERT INTO Producer VALUES (@Name, @Country);";

            SqlCommand command = new SqlCommand(sqlExpression, connection);
            command.Parameters.Add("@Name", SqlDbType.VarChar);
            command.Parameters["@Name"].Value = producer.Name;
            command.Parameters.Add("@Country", SqlDbType.VarChar);
            command.Parameters["@Country"].Value = producer.Country;

            command.ExecuteNonQuery();
        }

        public bool Delete(Producer producer)
        {
            string query = "DELETE Producer WHERE Id = @Id;";
            try
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@Id", SqlDbType.Int);
                command.Parameters["@Id"].Value = producer.Id;
                return command.ExecuteNonQuery() != 0;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public Producer Retrieve(int id)
        {
            string query = "Select * FROM Producer WHERE Id = @Id;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add("@Id", SqlDbType.Int);
            command.Parameters["@Id"].Value = id;

            SqlDataReader reader = command.ExecuteReader();
            
            Producer res = reader.Read() 
                ? new Producer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)) 
                : null;
            reader.Close();
            return res;
        }
        

        public List<Producer> RetrieveAll()
        {
            List<Producer> res = new List<Producer>();
            string query = "Select * FROM Producer;";

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                res.Add(new Producer((int)reader.GetValue(0), reader.GetValue(1).ToString(), reader.GetValue(2).ToString()));
            }
            reader.Close();
            return res.Count() != 0 ? res : null;
        }

        public bool Update(Producer producer)
        {
            string query = "UPDATE Producer SET Name = @Name, Country = @Country WHERE Id = @Id;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Add("@Id", SqlDbType.Int);
            command.Parameters["@Id"].Value = producer.Id;
            command.Parameters.Add("@Name", SqlDbType.VarChar);
            command.Parameters["@Name"].Value = producer.Name;
            command.Parameters.Add("@Country", SqlDbType.VarChar);
            command.Parameters["@Country"].Value = producer.Name;

            return command.ExecuteNonQuery() != 0;
        }


    }
}
