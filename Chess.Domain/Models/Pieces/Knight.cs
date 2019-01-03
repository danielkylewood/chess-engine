using System.Collections.Generic;

namespace Chess.Domain.Models.Pieces
{
    public class Knight : Piece
    {
        public Knight(Colour colour, Position position) : base(colour, position)
        {
            ImageName = $"{colour.ToString().ToLowerInvariant()}-knight.png";
        }

        public override List<Position> GetMoves(PieceMoveRequest pieceMoveRequest)
        {
            var moveList = new List<Position>();
            foreach (var delta in Constants.KnightDeltas)
            {
                var position = Position.Clone(Position) + delta;
                if (pieceMoveRequest.Pieces.ContainsKey(position) && pieceMoveRequest.Pieces[position].Colour == Colour) continue;
                if (position.IsValid())
                {
                    moveList.Add(position);
                }
            }

            return moveList;
        }
    }
}
