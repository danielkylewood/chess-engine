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
        public IActionResult ShowGame()
        {   
            _gameService.CreateGame(Guid.NewGuid());
            return View("Game");
        }

        [HttpGet]
        public IActionResult GetGame()
        {
            var gameState = _gameService._gameState;
            return PartialView("_Board", GameStateViewModel.FromGameState(gameState));
        }

        [HttpPut]
        public IActionResult MovePiece([FromBody] MoveRequest moveRequest)
        {
            var endPosition = Position.FromString(moveRequest.End);
            var startPosition = Position.FromString(moveRequest.Start);

            _gameService.ProcessMove(startPosition, endPosition);
            return PartialView("_Board", GameStateViewModel.FromGameState(_gameService._gameState));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
