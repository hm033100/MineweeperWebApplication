using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineweeperWebApplication.Models
{
    public class Cell
    {
        public int rowNumber { get; set; }
        public int columnNumber { get; set; }
        public int liveNeighbors { get; set; }
        public bool IsLive { get; set; }
        public bool isVisible { get; set; }
        public bool isValid { get; set; }
        public bool isFlag { get; set; }

        public Cell(int x, int y)
        {
            rowNumber = x;
            columnNumber = y;
            liveNeighbors = 0;
            isVisible = false;
            isFlag = false;
        }
    }
}
