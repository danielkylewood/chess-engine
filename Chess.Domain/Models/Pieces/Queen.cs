using System.Collections.Generic;

namespace Chess.Domain.Models.Pieces
{
    public class Queen : Piece
    {
        public Queen(Colour colour, Position position) : base(colour, position)
        {
            ImageName = $"{colour.ToString().ToLowerInvariant()}-queen.png";
        }

        public override List<Position> GetMoves(PieceMoveRequest pieceMoveRequest)
        {
            var moveList = new List<Position>();
            foreach (var delta in Constants.QueenDeltas)
            {
                var position = Position.Clone(Position) + delta;
                while (position.IsValid())
                {
                    if (pieceMoveRequest.Pieces.ContainsKey(position) &&
                        pieceMoveRequest.Pieces[position].Colour == Colour)
                        break;

                    moveList.Add(position);
                    if (pieceMoveRequest.Pieces.ContainsKey(position)) break;
                    position += delta;
                }
            }

            return moveList;
        }
    }
}
