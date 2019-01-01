using System.Collections.Generic;

namespace Chess.Domain.Models.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Colour colour, Position position) : base(colour, position)
        {
            ImageName = $"{colour.ToString().ToLowerInvariant()}-pawn.png";
        }

        public override List<Position> GetMoves(IDictionary<Position, Piece> pieces)
        {
            // TODO En Passant

            var moveList = new List<Position>();
            var pawnDelta = Colour == Colour.White ? Constants.WhitePawnDelta : Constants.BlackPawnDelta;
            var position = Position + pawnDelta;

            if (!pieces.ContainsKey(position))
            {
                moveList.Add(position);
                position += pawnDelta;
                if (NumberMoves == 0 && !pieces.ContainsKey(position))
                {
                    moveList.Add(position);
                }
            }

            var attackDeltaLeft = Position + (Colour == Colour.White ? new Position(1, -1) : new Position(-1, -1));
            var attackDeltaRight = Position + (Colour == Colour.White ? new Position(1, 1) : new Position(-1, 1));

            if (pieces.ContainsKey(attackDeltaLeft) && pieces[attackDeltaLeft].Colour != Colour)
            {
                moveList.Add(attackDeltaLeft);
            }

            if (pieces.ContainsKey(attackDeltaRight) && pieces[attackDeltaRight].Colour != Colour)
            {
                moveList.Add(attackDeltaRight);
            }

            return moveList;
        }
    }
}
