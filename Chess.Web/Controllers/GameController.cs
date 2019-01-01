using System;
using System.Diagnostics;
using Chess.Domain;
using Chess.Domain.Models;
using Chess.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Chess.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        public IActionResult Index()
        {
            return View("Home");
        }

        [HttpPost]
        public IActionResult CreateGame()
        {
            // https://stackoverflow.com/questions/19392212/how-to-use-jquery-or-ajax-to-update-razor-partial-view-in-c-asp-net-for-a-mvc-p
            var gameState = _gameService.CreateGame(Guid.NewGuid());
            var gameStateViewModel = GameStateViewModel.FromGameState(gameState);
            return RedirectToAction("ShowGame");
        }

        [HttpPut]
        public IActionResult MovePiece([FromBody] MoveRequest moveRequest)
        {
            var endPosition = Position.FromString(moveRequest.End);
            var startPosition = Position.FromString(moveRequest.Start);

            _gameService.ProcessMove(startPosition, endPosition);
            return Ok();
        }

        [HttpGet]
        public IActionResult ShowGame()
        {
            var gameState = _gameService._gameState;
            return PartialView("Board", GameStateViewModel.FromGameState(gameState));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
