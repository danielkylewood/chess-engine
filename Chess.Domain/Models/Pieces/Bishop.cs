using System.Collections.Generic;

namespace Chess.Domain.Models.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(Colour colour, Position position) : base(colour, position)
        {
            ImageName = $"{colour.ToString().ToLowerInvariant()}-bishop.png";
        }

        public override List<Position> GetMoves(IReadOnlyDictionary<Position, Piece> pieces)
        {
            var moveList = new List<Position>();
            foreach (var delta in Constants.BishopDeltas)
            {
                var position = Position.Clone(Position) + delta;
                while (position.IsValid())
                {
                    moveList.Add(position);
                    if (pieces.ContainsKey(position)) break;
                    position += delta;
                }
            }

            return moveList;
        }
    }
}
