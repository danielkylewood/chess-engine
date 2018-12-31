using System.Collections.Generic;
using Chess.Domain.Models;
using Chess.Domain.Models.Pieces;

namespace Chess.Domain
{
    public class MoveService : IMoveService
    {
        public GameState MovePiece(Position start, Position end, GameState gameState)
        {
            var piece = gameState.Pieces[start];
            gameState.Pieces.Remove(start);

            piece.Position = end;
            if (gameState.Pieces.ContainsKey(end))
            {
                var capturedPiece = gameState.Pieces[end];
                gameState.Pieces[end] = piece;
                if (capturedPiece.Colour == Colour.White)
                {
                    gameState.WhitePieces.Remove(capturedPiece);
                }
                else
                {
                    gameState.BlackPieces.Remove(capturedPiece);
                }
            }
            else
            {
                // TODO Check for castling
                gameState.Pieces.Add(end, piece);
            }

            return gameState;
        }

        private bool IsCastleMove(Position start, Position end, IReadOnlyDictionary<Position, Piece> pieces)
        {
            if (!(pieces[start] is King kingPiece))
            {
                return false;
            }

            if (kingPiece.NumberMoves != 0)
            {
                return false;
            }

            if (kingPiece.Colour == Colour.White &&
                !(end.Equals(new Position(0, 1)) ||
                  end.Equals(new Position(0, 6))))
            {
                return false;
            }

            if (kingPiece.Colour == Colour.Black &&
                !(end.Equals(new Position(7, 1)) ||
                  end.Equals(new Position(7, 6))))
            {
                return false;
            }

            return true;
        }
    }
}
