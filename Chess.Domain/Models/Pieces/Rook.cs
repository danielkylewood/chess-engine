using System.Collections.Generic;

namespace Chess.Domain.Models.Pieces
{
    public class Rook : Piece
    {
        public Rook(Colour colour, Position position) : base(colour, position)
        {
            ImageName = $"{colour.ToString().ToLowerInvariant()}-rook.png";
        }

        public override List<Position> GetMoves(IReadOnlyDictionary<Position, Piece> pieces)
        {
            throw new System.NotImplementedException();
        }
    }
}
