using SS_lab1.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace SS_lab1.SqlManager
{
    class GoodSqlManager : SqlManager
    {
        public GoodSqlManager() : base() { }
        public GoodSqlManager(string connectionStr) : base(connectionStr) { }

        public bool Create(Good good)
        {
            string query = "INSERT INTO Producer VALUES (@Name, @Price, @CategoryId, @ProducerId);";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add("@Name", SqlDbType.VarChar);
            command.Parameters["@Name"].Value = good.Name;
            command.Parameters.Add("@Price", SqlDbType.Decimal);
            command.Parameters["@Price"].Value = good.Price;
            command.Parameters.Add("@CategoryId", SqlDbType.Int);
            command.Parameters["@CategoryId"].Value = good.Category.Id;
            command.Parameters.Add("@ProducerId", SqlDbType.Int);
            command.Parameters["@ProducerId"].Value = good.Producer.Id;

            return command.ExecuteNonQuery() != 0;
        }
        public bool Delete(Good good)
        {
            string query = "DELETE Good WHERE Id = @Id;";
            try
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@Id", SqlDbType.Int);
                command.Parameters["@Id"].Value = good.Id;
                return command.ExecuteNonQuery() != 0;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public Good Retrieve(int id)
        {
            string query = "SELECT Good.Id, Good.Name, Good.Price, Category.Id, Category.Name, Producer.Id, Producer.Name, Producer.Country"; 
            query += " FROM Good";
            query += " INNER JOIN Category ON Good.CategoryId = Category.Id";
            query += " INNER JOIN Producer ON Good.ProducerId = Producer.Id";
            query += " WHERE Good.Id = @Id;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add("@Id", SqlDbType.Int);
            command.Parameters["@Id"].Value = id;

            SqlDataReader reader = command.ExecuteReader();

            Good res = reader.Read()
                ? new Good(reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2),
                        new Category(reader.GetInt32(3), reader.GetString(4)),
                        new Producer(reader.GetInt32(5), reader.GetString(6), reader.GetString(7)))
                : null;
            reader.Close();

            return res;
        }
        public List<Good> RetrieveAll()
        {
            List<Good> res = new List<Good>();
            string query = "SELECT Good.Id, Good.Name, Good.Price, Category.Id, Category.Name, Producer.Id, Producer.Name, Producer.Country";
            query += " FROM Good";
            query += " INNER JOIN Category ON Good.CategoryId = Category.Id";
            query += " INNER JOIN Producer ON Good.ProducerId = Producer.Id;";           

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                res.Add(new Good(reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2),
                        new Category(reader.GetInt32(3), reader.GetString(4)),
                        new Producer(reader.GetInt32(5), reader.GetString(6), reader.GetString(7))));
            }
            reader.Close();
            return res.Count() != 0 ? res : null;
        }
        public bool Update(Good good)
        {
            bool res = false;
            string query = @"UPDATE Good 
                             SET Name = @Name, Price = @Price, CategoryId = @CategoryId, ProducerId = @ProducerId 
                             WHERE Id = @Id;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add("@Name", SqlDbType.VarChar);
            command.Parameters["@Name"].Value = good.Name;
            command.Parameters.Add("@Price", SqlDbType.Decimal);
            command.Parameters["@Price"].Value = good.Price;
            command.Parameters.Add("@CategoryId", SqlDbType.Int);
            command.Parameters["@CategoryId"].Value = good.Category.Id;
            command.Parameters.Add("@ProducerId", SqlDbType.Int);
            command.Parameters["@ProducerId"].Value = good.Producer.Id;

            try
            { 
                res = command.ExecuteNonQuery() != 0;
            }
            catch(SqlException ex)
            {
                throw ex;
            }

            return res;
        }

        
    }
}
