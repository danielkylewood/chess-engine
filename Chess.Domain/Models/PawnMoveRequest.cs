using System.Collections.Generic;
using Chess.Domain.Models.Pieces;

namespace Chess.Domain.Models
{
    public class PawnMoveRequest : PieceMoveRequest
    {
        public int MoveNumber;
        public PawnMoveRequest(IDictionary<Position, Piece> pieces, int moveNumber) : base(pieces)
        {
            MoveNumber = moveNumber;
        }
    }
}
