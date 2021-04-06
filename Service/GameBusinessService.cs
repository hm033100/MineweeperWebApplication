using MineweeperWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MineweeperWebApplication.Service
{
    public class GameBusinessService
    {
        GameDataService gameDAO = new GameDataService();

        public bool create(Game game)
        {
            return gameDAO.create(game);
        }

        public List<Game> ViewByUser(int Userid)
        {
            List<Game> games = gameDAO.ViewByUser(Userid);

            for (int i = 0; i < games.Count; i++)
            {
                /*var options = new JsonSerializerOptions
                {
                    IncludeFields = true,
                };*/
                SerializeBoard ser = JsonSerializer.Deserialize<SerializeBoard>(games[i].BoardJSON);
                int counter = 0;
                Board board = new Board(ser.size);
                board.size = ser.size;
                board.difficulty = ser.difficulty;

                for (int x = 0; x < ser.size; x++)
                {
                    for (int y = 0; y < ser.size; y++)
                    {
                        board.theGrid[x, y] = ser.grid[x + y + counter];

                    }
                    counter += ser.size - 1;
                }

                games[i].board = board;
            }
            return games;
        }

        public List<Game> ViewAll()
        {
            List<Game> games = gameDAO.ViewAll();

            for (int i = 0; i < games.Count; i++)
            {
                /*var options = new JsonSerializerOptions
                {
                    IncludeFields = true,
                };*/
                SerializeBoard ser = JsonSerializer.Deserialize<SerializeBoard>(games[i].BoardJSON);
                int counter = 0;
                Board board = new Board(ser.size);
                board.size = ser.size;
                board.difficulty = ser.difficulty;

                for (int x = 0; x < ser.size; x++)
                {
                    for (int y = 0; y < ser.size; y++)
                    {
                        board.theGrid[x, y] = ser.grid[x + y + counter];

                    }
                    counter += ser.size - 1;
                }

                games[i].board = board;
            }
            return games;
        }

        public Game ViewOne(int GameId)
        {
            Game game = gameDAO.ViewOne(GameId);

            /*var options = new JsonSerializerOptions
            {
                IncludeFields = true,
            };*/
            SerializeBoard ser = JsonSerializer.Deserialize<SerializeBoard>(game.BoardJSON);
            int counter = 0;
            Board board = new Board(ser.size);
            board.size = ser.size;
            board.difficulty = ser.difficulty;

            for (int x = 0; x < ser.size; x++)
            {
                for (int y = 0; y < ser.size; y++)
                {
                    board.theGrid[x, y] = ser.grid[x + y + counter];

                }
                counter += ser.size - 1;
            }

            game.board = board;

            return game;
        }

        public int Update(Game game)
        {
            return gameDAO.Update(game);
        }
        public int Delete(int gameid)
        {
            return gameDAO.Delete(gameid);
        }
    }
}
