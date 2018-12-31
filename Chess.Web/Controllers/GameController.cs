using System;
using System.Diagnostics;
using Chess.Domain;
using Chess.Domain.Models;
using Chess.Web.Models;
using Microsoft.AspNetCore.Mvc;

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
            var gameState = _gameService.CreateGame(Guid.NewGuid());
            var gameStateViewModel = GameStateViewModel.FromGameState(gameState);
            return View("Board", gameStateViewModel);
        }

        public IActionResult MovePiece(string start, string end)
        {
            var endPosition = Position.FromString(end);
            var startPosition = Position.FromString(start);

            var gameState = _gameService.ProcessMove(startPosition, endPosition);
            var gameStateViewModel = GameStateViewModel.FromGameState(gameState);

            return View("Board", gameStateViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
