using System.Collections.Generic;

namespace Chess.Domain.Models.Pieces
{
    public class Knight : Piece
    {
        public Knight(Colour colour, Position position) : base(colour, position)
        {
            ImageName = $"{colour.ToString().ToLowerInvariant()}-knight.png";
        }

        public override List<Position> GetMoves(IReadOnlyDictionary<Position, Piece> pieces)
        {
            throw new System.NotImplementedException();
        }
    }
}
