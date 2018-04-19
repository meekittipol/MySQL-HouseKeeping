using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysql_housekeeping
{
    public class DBConnection
    {
        private DBConnection()
        {
        }

        private string databaseName = string.Empty;
        public string DatabaseName
        {
            get { return databaseName; }
            set { databaseName = value; }
        }

        private string conString = string.Empty;
        public string ConnectionString
        {
            get { return conString; }
            set { conString = value; }
        }

        public string Password { get; set; }
        private MySqlConnection connection = null;
        public MySqlConnection Connection
        {
            get { return connection; }
        }

        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if (_instance == null)
                _instance = new DBConnection();
            return _instance;
        }

        public bool IsConnect()
        {
            if (Connection == null)
            {
                if (!string.IsNullOrEmpty(conString))
                {
                    string connstring = conString;
                    connection = new MySqlConnection(connstring);
                    connection.Open();
                }             
            }

            return true;
        }

        public void Close()
        {
            connection.Close();
        }
    }
}
