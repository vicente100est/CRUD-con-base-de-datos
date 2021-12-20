using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDeDatos
{
    public abstract class DB
    {
        private string _conectionStr;
        protected SqlConnection _connection;

        public DB(string server, string db, bool windowsAuth)
        {
            _conectionStr = $"Data Source={server}; Initial Catalog={db};" +
                $"integrated security={windowsAuth}";
        }

        public void Connect()
        {
            _connection = new SqlConnection(_conectionStr);
            _connection.Open();
        }

        public void Close()
        {
            if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }
}
