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
            var moveList = new List<Position>();
            foreach (var delta in Constants.KnightDeltas)
            {
                var position = Position.Clone(Position) + delta;
                if (pieces.ContainsKey(position) && pieces[position].Colour == Colour) continue;
                if (position.IsValid())
                {
                    moveList.Add(position);
                }
            }

            return moveList;
        }
    }
}
