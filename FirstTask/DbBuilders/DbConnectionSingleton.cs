using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTask
{
    class DbConnectionSingleton
    {
        static SqlConnection _conn = null;
        static string _connectionString = null;
        private DbConnectionSingleton()
        {

        }
        static DbConnectionSingleton()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["LocalStringConnection"].ConnectionString as string;
        }

        static public SqlConnection GetInstance()
        {
            if (_conn == null)
                {
                    _conn = new SqlConnection(_connectionString);
                    _conn.Open();
	            }
            return _conn;
        }
    }
}
