using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_lab1.SqlManager
{
    class SqlManager
    {        
        protected string connectionStr;
        protected SqlConnection connection;

        public SqlManager(string connectionStr)
        {
            this.connectionStr = connectionStr;
            this.connection = new SqlConnection(connectionStr);
        }
        public SqlManager() : this(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString)
        {
        }

        
        public void ConnectionClose()
        {
            connection.Close();
        }
        public void ConnectionOpen()
        {
            connection.Open();
        }
    }
}
