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
            var moveList = new List<Position>();
            foreach (var delta in Constants.RookDeltas)
            {
                var position = Position.Clone(Position) + delta;
                while (position.IsValid())
                {
                    if (pieces.ContainsKey(position) &&
                        pieces[position].Colour == Colour)
                        break;

                    moveList.Add(position);
                    if (pieces.ContainsKey(position)) break;
                    position += delta;
                }
            }

            return moveList;
        }
    }
}
