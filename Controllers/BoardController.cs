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
using MineweeperWebApplication.Service;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace MineweeperWebApplication.Controllers
{
    public class BoardController : Controller
    {
        GameBusinessService service = new GameBusinessService();
        static Game game;
        public IActionResult Index()
        {
            int BoardSize = 10;
            Board board = new Board(BoardSize);
            board.difficulty = 1;
            
            
            board.setupLiveNeighbors();
            board.calculateLiveNeighbors();

            game = new Game { id = 0, Userid = 0, board = board, BoardJSON = "", date = DateTime.Now, IsFinished = false, IsSaved = false, Time = 0 };

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

                if (game.board.theGrid[row, col].liveNeighbors == 9)
                {
                    gameLost = true;
                    gameOver();
                }
                else
                {
                    game.board.floodFill(row, col);

                    if (game.board.winCondition())
                    {
                        gameOver();
                        gameWon = true;
                    }
                }   
            }

            string BoardString = RenderRazorViewToString(this, "_buttonBoard", game.board);

            return Json(new { boardString = BoardString, gameWon = gameWon, gameLost = gameLost });

        }

        public IActionResult Reset()
        {
            game.board = new Board(10);
            game.board.size = 10;
            game.board.difficulty = 1;

            game.board.setupLiveNeighbors();
            game.board.calculateLiveNeighbors();

            ViewBag.Board = game.board;

            return View("Index");
        }

        public void gameOver()
        {
            for (int i = 0; i < game.board.size; i++)
            {
                for (int j = 0; j < game.board.size; j++)
                {
                    game.board.theGrid[i, j].isVisible = true;
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
                game.board.theGrid[row, col].isFlag = true;
            }

            return PartialView("_buttonGrid", game.board.theGrid[row, col]);
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

        public IActionResult SavedGames()
        {
            return View("SavedGames", service.ViewByUser(0));
        }

        public IActionResult DeleteGame(int gameid)
        {
            service.Delete(gameid);

            return View("SavedGames", service.ViewByUser(0));
        }

        public JsonResult SaveGame(int time)
        {
            game.Time = time;
            game.board = game.board;
            game.BoardJSON = JsonSerializer.Serialize(new SerializeBoard(game.board));
            game.date = DateTime.Now;

            if (!game.IsSaved)
            {
                game.IsSaved = true;
                service.create(game);
            }
            else
            {
                service.Update(game);
            }

            return Json(new { url = "/Game/SavedGames" });
        }

    }
}
