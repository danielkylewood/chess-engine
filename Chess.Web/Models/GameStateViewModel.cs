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
        public List<ValidMove> ValidMoves { get; set; }
        public List<string> UniqueMoveableSquares { get; set; }

        public static GameStateViewModel FromGameState(GameState gameState)
        {
            var validMoves = gameState.ValidMoves.Keys.Select(piece => new ValidMove
            {
                PiecePosition = "#piece-" + piece.Position,
                PieceMoves = gameState.ValidMoves[piece].Select(x => "#square-" + x.ToString()).ToList()
            }).ToList();

            var uniqueSquareList = new List<string>();
            foreach (var move in validMoves)
            {
                uniqueSquareList.AddRange(move.PieceMoves);
            }

            var gameViewModel = new GameStateViewModel
            {
                ValidMoves = validMoves,
                CurrentPlayer = gameState.CurrentPlayer,
                UniqueMoveableSquares = uniqueSquareList.Distinct().ToList(),
                Pieces = gameState.Pieces.ToDictionary(pair => pair.Key.ToString(), pair => pair.Value)
            };

            return gameViewModel;
        }
    }
}
