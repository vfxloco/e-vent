using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace E_Vent.Connection
{
    public class Connection : IDisposable
    {
        protected SqlConnection connection { get; set; }

        public Connection()
        {
            //string StrConn = @"Data Source = 192.168.40.108; Initial Catalog=DB_EVent10; User ID = con; Password = 123";
            //string StrConn = @"Data Source = 192.168.1.30; Initial Catalog = DB_EVent10; User ID = con; Password = 123";
            string StrConn = @"Data Source = 192.168.2.120; Initial Catalog = DB_EVent10; User ID = con; Password = 123";
            //string StrConn = @"Data Source = localhost; Initial Catalog=DB_EVent; User ID = sa; Password = 123";
            connection = new SqlConnection(StrConn);
            connection.Open();
        }
        public void Dispose()
        {
            connection.Close();
        }
    }
}