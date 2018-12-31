using System.Collections.Generic;
using Chess.Domain.Models.Pieces;

namespace Chess.Domain.Models
{
    public class MoveServiceResult
    {
        public IDictionary<Position, Piece> Pieces { get; set; }
        public HashSet<Piece> WhitePieces { get; set; }
        public HashSet<Piece> BlackPieces { get; set; }
    }
}
