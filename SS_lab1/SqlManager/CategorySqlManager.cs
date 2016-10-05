using SS_lab1.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SS_lab1.SqlManager
{
    class CategorySqlManager : SqlManager
    {       
        public CategorySqlManager() : base() { }
        public CategorySqlManager(string connectionStr) : base(connectionStr) { }        


        public void Create(Category category)
        {
            string sqlExpression = "INSERT INTO Category VALUES (@Name);";            
            
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            command.Parameters.Add("@Name", SqlDbType.VarChar);
            command.Parameters["@Name"].Value = category.Name;
            command.ExecuteNonQuery();              
            
        }
        public bool Delete(Category category)
        {
            string query = "DELETE Category WHERE Id = @Id;";
            try
            {                
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@Id", SqlDbType.Int);
                command.Parameters["@Id"].Value = category.Id;                    
                return command.ExecuteNonQuery() != 0;                    
                
            }
            catch (SqlException ex)
            {
                throw ex;
            }            
        }

        public Category Retrieve(int id)
        {
            string query = "Select * FROM Category WHERE Id = @Id;";         

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add("@Id", SqlDbType.Int);
            command.Parameters["@Id"].Value = id;

            SqlDataReader reader = command.ExecuteReader();            
                                
            Category res = reader.Read() 
                ? new Category(reader.GetInt32(0), reader.GetString(1)) 
                : null;                
            reader.Close();

            return res;        
        }
        public Category Retrieve(string name)
        {
            string query = "Select * FROM Category WHERE Name = @Name;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add("@Name", SqlDbType.VarChar);
            command.Parameters["@Name"].Value = name;

            SqlDataReader reader = command.ExecuteReader();

            Category res = reader.Read()
                ? new Category(reader.GetInt32(0), reader.GetString(1))
                : null;
            reader.Close();

            return res;
        }

        public List<Category> RetrieveAll()
        {
            List<Category> res = new List<Category>();
            string query = "Select * FROM Category;";
            
            SqlCommand command = new SqlCommand(query, connection);                
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                res.Add(new Category(reader.GetInt32(0), reader.GetString(1)));
            }
            reader.Close();
            return res.Count() != 0 ? res : null;            
        }

        public bool Update(Category category)
        {
            string query = "UPDATE Category SET Name = @Name WHERE Id = @Id;";
            
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add("@Id", SqlDbType.Int);
            command.Parameters["@Id"].Value = category.Id;
            command.Parameters.Add("@Name", SqlDbType.VarChar);
            command.Parameters["@Name"].Value = category.Name;

            return command.ExecuteNonQuery() != 0;   
        }


        public void CustomDelete(Category category)
        {
            Category noNameCategory = Retrieve(new Category().Name);      
           
            if (noNameCategory == null)
            {
                noNameCategory = new Category();
                Create(noNameCategory);
            }
            
            string query = @"UPDATE Good 
                             SET CategoryId = @defId 
                             WHERE CategoryId = @CategoryId;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add("@defId", SqlDbType.Int);
            command.Parameters["@defId"].Value =noNameCategory.Id;
            command.Parameters.Add("@CategoryId", SqlDbType.Int);
            command.Parameters["@CategoryId"].Value = category.Id;

            command.ExecuteNonQuery();
            Delete(category);
        }      
        

        
    }
}
