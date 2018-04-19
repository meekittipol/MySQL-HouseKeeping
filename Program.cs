using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysql_housekeeping
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var dbCon = DBConnection.Instance();

                 DbOject json = LoadJson();

                if (json == null)
                    { throw new Exception("DbSettings is null"); }
                if (json.ConnectionString == null)
                    { throw new Exception("ConnectionString is undefined"); }

                dbCon.ConnectionString = json.ConnectionString;

                if (dbCon.IsConnect())
                {
                    string query = string.Empty;
                    if (json.Query.Count() != 0)
                    {
                        foreach (var item in json.Query)
                        {
                            query += item;
                        }

                        var cmd = new MySqlCommand(query, dbCon.Connection);
                        var reader = cmd.ExecuteReader();
                    }                       

                    dbCon.Close();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey();
            }
        }

        public static DbOject LoadJson()
        {
            DbOject obj = null;
            using (StreamReader r = new StreamReader("DbSettings.json"))
            {
                string json = r.ReadToEnd();
                obj = JsonConvert.DeserializeObject<DbOject>(json);
            }

            return obj;
        }

        public class DbOject
        {
            public string ConnectionString { get; set; }
            public string[] Query { get; set; }
        }
    }
}
