using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineweeperWebApplication.Service
{
    public class BoardController : Controller
    {
        static Board board = new Board(10);
        public IActionResult Index()
        {
            board.size = 10;
            board.difficulty = 1;
            
            
            board.setupLiveNeighbors();
            board.calculateLiveNeighbors();
            ViewBag.Board = board;
            return View();
        }

        public IActionResult HandleButtonClick(string rowAndColumn)
        {
            if (!rowAndColumn.StartsWith("visited"))
            {
                string[] cell = rowAndColumn.Split(',');
                int row = int.Parse(cell[0]);
                int col = int.Parse(cell[1]);
                board.floodFill(row, col);
            }


            ViewBag.Board = board;

            return View("Index", board);
        }

    }
}
