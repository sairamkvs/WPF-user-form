using ModelLibary;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataAccessLibrary.Repository
{
    public class Repository : IRepository<Users>
    {
        private readonly DbConfiguration _dbConfiguration;

        public Repository(DbConfiguration dbConfiguration)
        {
            _dbConfiguration = dbConfiguration;
        }
        public async Task Add(Users user)
        {
            try
            {
                using (var conn = _dbConfiguration.connectToSql())
                {
                    await conn.OpenAsync();

                    using MySqlCommand cmd = new MySqlCommand("InsertUser", conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userid", user.User_Id);
                    cmd.Parameters.AddWithValue("@UserName", user.User_Name);
                        cmd.Parameters.AddWithValue("@GroupName", user.Group);
                        cmd.Parameters.AddWithValue("@Host", user.Host);
                        cmd.Parameters.AddWithValue("@IPAddress", user.IP_Address);
                    
                    await cmd.ExecuteNonQueryAsync();
                    MessageBox.Show("Vaues inserted");
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async Task Delete(Users user)
        {
            try
            {
                using var conn = _dbConfiguration.connectToSql();
                await conn.OpenAsync();
                using MySqlCommand cmd = new MySqlCommand("DeleteUser", conn)
                {
                    CommandType = CommandType.StoredProcedure
                     
                };
                cmd.Parameters.AddWithValue("@P_UserId", user.User_Id);
                await cmd.ExecuteNonQueryAsync();
                MessageBox.Show("Vaues updated");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async Task<IEnumerable<Users>> GetAll()
        {
            var users = new List<Users>();
            try
            {
                using var conn = _dbConfiguration.connectToSql();
                await conn.OpenAsync();

                using MySqlCommand cmd = new MySqlCommand("getall", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                using MySqlDataReader reader =   cmd.ExecuteReader();

                while (await reader.ReadAsync())
                {
                    users.Add(new Users()
                    {
                        User_Id = reader.GetInt32("UserId"),      // Ensure the column name matches that in the table  
                        User_Name = reader.GetString("UserName"), // Ensure the column name matches that in the table  
                        Group = reader.GetString("GroupName"),     // Ensure the column name matches that in the table  
                        Host = reader.GetString("Host"),           // Ensure the column name matches that in the table  
                        IP_Address = reader.GetInt32("IPAddress")  // Ensure the column name matches that in the table  
                    });
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (logging, rethrowing, etc.)  
                throw new Exception("An error occurred while retrieving users", ex);
            }

            return users;
        }
              
          
        public async Task Update(Users user)
        {
            try
            {
                using var conn = _dbConfiguration.connectToSql();
                await conn.OpenAsync();
                using var cmd = new MySqlCommand("UpdateUsers", conn) {
                CommandType  = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@P_userid", user.User_Id);
                cmd.Parameters.AddWithValue("@P_UserName", user.User_Name);
                cmd.Parameters.AddWithValue("@P_GroupName", user.Group);
                cmd.Parameters.AddWithValue("@P_Host", user.Host);
                cmd.Parameters.AddWithValue("@P_IPAddress", user.IP_Address);

                await cmd.ExecuteNonQueryAsync();
                MessageBox.Show("Vaues updated");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
