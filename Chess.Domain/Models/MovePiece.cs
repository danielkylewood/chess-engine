using System.Collections.Generic;
using Chess.Domain.Models.Pieces;

namespace Chess.Domain.Models
{
    public class MovePiece
    {
        public Position Start { get; set; }
        public Position End { get; set; }
        public IDictionary<Position, Piece> Pieces { get; set; }
        public HashSet<Piece> WhitePieces { get; set; }
        public HashSet<Piece> BlackPieces { get; set; }
    }
}
