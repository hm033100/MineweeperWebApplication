using MineweeperWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MineweeperWebApplication.Service
{
    public class GameDataService
    {
        string connectionString = @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = MinesweeperDB; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public bool create(Models.Game game)
        {
            bool success = false;

            string sql = "INSERT INTO games (GAME, TIME, USER_ID, IS_FINISHED, IS_SAVED, DATE) VALUES (@game, @time, @userid, @isFinished, @isSaved, @date)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@game", game.BoardJSON);
                command.Parameters.AddWithValue("@time", game.Time);
                command.Parameters.AddWithValue("@userid", game.Userid);
                command.Parameters.AddWithValue("@isFinished", game.IsFinished);
                command.Parameters.AddWithValue("@isSaved", game.IsSaved);
                command.Parameters.AddWithValue("@date", game.date.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                try
                {
                    connection.Open();
                    int status = command.ExecuteNonQuery();

                    if (status > 0)
                    {
                        success = true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return success;
        }

        public List<Game> ViewByUser(int Userid)
        {
            List<Game> products = new List<Game>();

            string SqlStatment = "SELECT * FROM games WHERE USER_ID = @userid order by DATE DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(SqlStatment, connection);

                sqlCommand.Parameters.AddWithValue("@userid", Userid);

                try
                {
                    connection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        products.Add(new Game
                        {
                            id = (int)reader[0],
                            BoardJSON = (string)reader[1],
                            Time = (int)reader[2],
                            Userid = (int)reader[3],
                            IsFinished = (bool)reader[4],
                            IsSaved = (bool)reader[5],
                            date = (DateTime)reader[6]
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("SQL Error within GameDateSerivce.ViewAll(): " + ex.Message);
                }
            }

            return products;
        }

        public Game ViewOne(int GameId)
        {
            Game game = null;

            string SqlStatment = "SELECT * FROM games WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(SqlStatment, connection);
                sqlCommand.Parameters.AddWithValue("@Id", GameId);

                try
                {
                    connection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        game = new Game
                        {
                            id = (int)reader[0],
                            BoardJSON = (string)reader[1],
                            Time = (int)reader[2],
                            Userid = (int)reader[3],
                            IsFinished = (bool)reader[4],
                            IsSaved = (bool)reader[5],
                            date = (DateTime)reader[6]
                        };
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("SQL Error within ProductsDAO.GetAll(): " + ex.Message);
                }
            }

            return game;
        }

        public int Update(Game game)
        {
            int newIdNumber = -1;

            string SqlStatment = "UPDATE games SET GAME = @game, TIME = @time, USER_ID = @userid, IS_FINISHED = @isFinished, IS_SAVED = @isSaved, DATE = @date WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(SqlStatment, connection);

                sqlCommand.Parameters.AddWithValue("@game", game.BoardJSON);
                sqlCommand.Parameters.AddWithValue("@time", game.Time);
                sqlCommand.Parameters.AddWithValue("@userid", game.Userid);
                sqlCommand.Parameters.AddWithValue("@isFinished", game.IsFinished);
                sqlCommand.Parameters.AddWithValue("@isSaved", game.IsSaved);
                sqlCommand.Parameters.AddWithValue("@date", game.date.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                sqlCommand.Parameters.AddWithValue("@id", game.id);


                try
                {
                    connection.Open();

                    newIdNumber = Convert.ToInt32(sqlCommand.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("SQL Error within ProductsDAO.GetAll(): " + ex.Message);
                }
            }

            return newIdNumber;
        }

        public int Delete(int gameId)
        {
            int rowsAffected = -1;

            string SqlStatment = "DELETE FROM games WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(SqlStatment, connection);

                sqlCommand.Parameters.AddWithValue("@Id", gameId);

                try
                {
                    connection.Open();

                    rowsAffected = Convert.ToInt32(sqlCommand.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("SQL Error within GamesDataService.Delete(): " + ex.Message);
                }
            }

            return rowsAffected;
        }

        public List<Game> ViewAll()
        {
            List<Game> products = new List<Game>();

            string SqlStatment = "SELECT * FROM games";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(SqlStatment, connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        products.Add(new Game
                        {
                            id = (int)reader[0],
                            BoardJSON = (string)reader[1],
                            Time = (int)reader[2],
                            Userid = (int)reader[3],
                            IsFinished = (bool)reader[4],
                            IsSaved = (bool)reader[5],
                            date = (DateTime)reader[6]
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("SQL Error within GameDateSerivce.ViewAll(): " + ex.Message);
                }
            }

            return products;
        }
    }
}
