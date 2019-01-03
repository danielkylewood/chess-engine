using System;
using System.Collections.Generic;
using Chess.Domain.Models.Pieces;

namespace Chess.Domain.Models
{
    public static class PieceMoveFactory
    {
        public static PieceMoveRequest GetPieceMoveRequest(Piece piece, IDictionary<Position, Piece> pieces, int moveNumber, bool isCheck)
        {
            switch (piece)
            {
                case Bishop bishop:
                    return new PieceMoveRequest(pieces);
                case King king:
                    return new PieceMoveRequest(pieces);
                case Knight knight:
                    return new PieceMoveRequest(pieces);
                case Pawn pawn:
                    return new PawnMoveRequest(pieces, moveNumber);
                case Queen queen:
                    return new PieceMoveRequest(pieces);
                case Rook rook:
                    return new PieceMoveRequest(pieces);
                default:
                    throw new ArgumentException($"Piece must be of type {typeof(Piece).Name}");
            }
        }
    }
}
