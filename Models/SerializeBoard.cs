using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace MineweeperWebApplication.Models
{
    public class SerializeBoard
    {
        public int size { get; set; }
        public Cell[] grid { get; set; }
        public double difficulty { get; set; }

        public SerializeBoard(Board board)
        {
            size = board.size;
            difficulty = board.difficulty;

            grid = new Cell[size * size];
            int counter = 0;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    grid[i + j + counter] = board.theGrid[i, j];
                }
                counter += size - 1;
            }
        }

        [JsonConstructor]
        public SerializeBoard(int size, Cell[] grid, double difficulty)
        {
            this.size = size;
            this.grid = grid;
            this.difficulty = difficulty;
        }
    }
}
