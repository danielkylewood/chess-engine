using System.Collections.Generic;
using System.Linq;
using Chess.Domain.Models;
using Chess.Domain.Models.Pieces;

namespace Chess.Web.Models
{
    public class GameViewModel
    {
        public List<List<Square>> Board { get; set; }
        public Dictionary<string, Piece> Pieces { get; set; }

        public GameViewModel()
        {
            Board = InitialiseBoard();
            Pieces = new Dictionary<string, Piece>();
        }

        public static GameViewModel FromGameState(GameState gameState)
        {
            var gameViewModel = new GameViewModel
            {
                Pieces = gameState.Pieces.ToDictionary(pair => $"{ViewConstants.SquarePrefix}{pair.Key.ToString()}", pair => pair.Value)
            };

            return gameViewModel;
        }

        private static List<List<Square>> InitialiseBoard()
        {
            var board = new List<List<Square>>();
            for (var i = 7; i >= 0; i--)
            {
                var colourModifier = i % 2;
                var squareColourClass = colourModifier == 1
                    ? ViewConstants.BoardSquareWhite
                    : ViewConstants.BoardSquareBlack;

                var innerList = new List<Square>();
                for (var j = 0; j <= 7; j++)
                {
                    var squarePosition = new Position(i, j);
                    innerList.Add(new Square
                    {
                        SquareClass = squareColourClass,
                        SquareId = $"{ViewConstants.SquarePrefix}{squarePosition}"
                    });
                    squareColourClass = squareColourClass.Equals(ViewConstants.BoardSquareWhite) 
                        ? ViewConstants.BoardSquareBlack 
                        : ViewConstants.BoardSquareWhite;
                }
                board.Add(innerList);
            }

            return board;
        }
    }

    public class Square
    {
        public string SquareClass { get; set; }
        public string SquareId { get; set; }
    }
}
