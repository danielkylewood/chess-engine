using System.Collections.Generic;
using System.Linq;
using Chess.Domain.Models;
using Chess.Domain.Models.Pieces;

namespace Chess.Web.Models
{
    public class GameStateViewModel
    {
        public Colour CurrentPlayer { get; set; }
        public Dictionary<string, Piece> Pieces { get; set; }
        public Dictionary<Piece, List<string>> ValidMoves { get; set; }

        public static GameStateViewModel FromGameState(GameState gameState)
        {
            var gameViewModel = new GameStateViewModel
            {
                CurrentPlayer = gameState.CurrentPlayer,
                Pieces = gameState.Pieces.ToDictionary(pair => pair.Key.ToString(), pair => pair.Value),
                ValidMoves = gameState.ValidMoves.ToDictionary(pair => pair.Key, pair => pair.Value.Select(x => x.ToString()).ToList())
            };
            return gameViewModel;
        }
    }
}
