using System.Collections.Generic;
using Chess.Domain.Models.Pieces;

namespace Chess.Domain.Models
{
    public class PieceMoveRequest
    {
        public IDictionary<Position, Piece> Pieces;

        public PieceMoveRequest(IDictionary<Position, Piece> pieces)
        {
            Pieces = pieces;
        }
    }
}
