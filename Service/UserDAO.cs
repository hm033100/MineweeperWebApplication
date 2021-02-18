using MineweeperWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MineweeperWebApplication.Service
{
    public class UserDAO
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MinesweeperDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public bool authenticateUser(User user)
        {
            bool success = false;

            string sqlStatment = "SELECT * FROM dbo.Users WHERE USERNAME = @username AND PASSWORD = @password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatment, connection);

                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 40).Value = user.Username;
                command.Parameters.Add("@PASSWORD", System.Data.SqlDbType.VarChar, 40).Value = user.Password;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                        success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return success;
        }

        public bool createUser(User user)
        {
            bool success = false;

            string sqlStatment = "INSERT INTO dbo.Users (FIRSTNAME, LASTNAME, SEX, AGE, STATE, EMAIL, USERNAME, PASSWORD) VALUES (@FIRSTNAME, @LASTNAME, @SEX, @AGE, @STATE, @EMAIL, @USERNAME, @PASSWORD);";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatment, connection);

                command.Parameters.Add("@FIRSTNAME", System.Data.SqlDbType.VarChar, 50).Value = user.FirstName;
                command.Parameters.Add("@LASTNAME", System.Data.SqlDbType.VarChar, 50).Value = user.LastName;
                command.Parameters.Add("@SEX", System.Data.SqlDbType.VarChar, 20).Value = user.Sex;
                command.Parameters.Add("@AGE", System.Data.SqlDbType.Int).Value = user.Age;
                command.Parameters.Add("@STATE", System.Data.SqlDbType.VarChar, 20).Value = user.State;
                command.Parameters.Add("@EMAIL", System.Data.SqlDbType.VarChar, 50).Value = user.Email;
                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 40).Value = user.Username;
                command.Parameters.Add("@PASSWORD", System.Data.SqlDbType.VarChar, 40).Value = user.Password;

                try
                {
                    connection.Open();
                    int status = command.ExecuteNonQuery();

                    if (status > 0)
                        success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return success;
        }
    }
}
