using System.Collections.Generic;

namespace Chess.Domain.Models.Pieces
{
    public class Queen : Piece
    {
        public Queen(Colour colour, Position position) : base(colour, position)
        {
            ImageName = $"{colour.ToString().ToLowerInvariant()}-queen.png";
        }

        public override List<Position> GetMoves(IReadOnlyDictionary<Position, Piece> pieces)
        {
            throw new System.NotImplementedException();
        }
    }
}
