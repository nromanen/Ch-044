using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    public class ConnectionManager
    {
        public SqlConnection con = null;
        public void OpenConnection(string connectionString)
        {
            con = new SqlConnection(connectionString);
            con.Open();
        }
        public void CloseConnection()
        {
            con.Close();
        }
    }
}
