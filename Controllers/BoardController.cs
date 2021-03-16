using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MineweeperWebApplication.Models;
using System.IO;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace MineweeperWebApplication.Controllers
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
            bool gameWon = false;
            bool gameLost = false;

            if (!rowAndColumn.StartsWith("visited"))
            {
                string[] cell = rowAndColumn.Split('-');
                int row = int.Parse(cell[0]);
                int col = int.Parse(cell[1]);

                if (board.theGrid[row, col].liveNeighbors == 9)
                {
                    gameLost = true;
                    gameOver();
                }
                else
                {
                    board.floodFill(row, col);

                    if (board.winCondition())
                    {
                        gameOver();
                        gameWon = true;
                    }
                }   
            }

            string BoardString = RenderRazorViewToString(this, "_buttonBoard", board);

            return Json(new { boardString = BoardString, gameWon = gameWon, gameLost = gameLost });

        }

        public IActionResult Reset()
        {
            board = new Board(10);
            board.size = 10;
            board.difficulty = 1;

            board.setupLiveNeighbors();
            board.calculateLiveNeighbors();

            ViewBag.Board = board;

            return View("Index");
        }

        public void gameOver()
        {
            for (int i = 0; i < board.size; i++)
            {
                for (int j = 0; j < board.size; j++)
                {
                    board.theGrid[i, j].isVisible = true;
                }
            }
        }

        public PartialViewResult RightClick(string rowAndColumn)
        {
            string[] point = rowAndColumn.Split('-');
            int row = int.Parse(point[0]);
            int col = int.Parse(point[1]);

            Console.WriteLine("Row: " + row + ", Column: " + col);

            if (!rowAndColumn.StartsWith("visited"))
            {
                board.theGrid[row, col].isFlag = true;
            }

            return PartialView("_buttonGrid", board.theGrid[row, col]);
        }

        public static string RenderRazorViewToString(Controller controller, string viewName, object model = null)
        {
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                IViewEngine viewEngine =
                    controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as
                        ICompositeViewEngine;
                ViewEngineResult viewResult = viewEngine.FindView(controller.ControllerContext, viewName, false);

                ViewContext viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    sw,
                    new HtmlHelperOptions()
                );
                viewResult.View.RenderAsync(viewContext);
                return sw.GetStringBuilder().ToString();
            }
        }

    }
}
