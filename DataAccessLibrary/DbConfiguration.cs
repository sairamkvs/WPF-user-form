using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataAccessLibrary
{
    public class DbConfiguration
    {
        private readonly string connectionString;

        public DbConfiguration()
        {
            connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        }
        public MySqlConnection connectToSql()
        {
            return new MySqlConnection(connectionString);
        }

      
    }
}
