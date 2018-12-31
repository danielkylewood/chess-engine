using System.Collections.Generic;

namespace Chess.Domain.Models.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Colour colour, Position position) : base(colour, position)
        {
            ImageName = $"{colour.ToString().ToLowerInvariant()}-pawn.png";
        }

        public override List<Position> GetMoves(IReadOnlyDictionary<Position, Piece> pieces)
        {
            throw new System.NotImplementedException();
        }
    }
}
