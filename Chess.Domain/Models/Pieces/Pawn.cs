using System;
using System.Collections.Generic;

namespace Chess.Domain.Models.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Colour colour, Position position) : base(colour, position)
        {
            ImageName = $"{colour.ToString().ToLowerInvariant()}-pawn.png";
        }

        public override List<Position> GetMoves(PieceMoveRequest pieceMoveRequest)
        {
            if (!(pieceMoveRequest is PawnMoveRequest pawnMoveRequest))
            {
                throw new ArgumentException("Expected pawn move request.");
            }

            var moveList = new List<Position>();
            var pawnDelta = Colour == Colour.White ? Constants.WhitePawnDelta : Constants.BlackPawnDelta;
            var position = Position + pawnDelta;

            if (!pawnMoveRequest.Pieces.ContainsKey(position))
            {
                moveList.Add(position);
                position += pawnDelta;
                if (NumberMoves == 0 && !pawnMoveRequest.Pieces.ContainsKey(position))
                {
                    moveList.Add(position);
                }
            }

            var attackDeltaLeft = Position + (Colour == Colour.White ? new Position(1, -1) : new Position(-1, -1));
            var attackDeltaRight = Position + (Colour == Colour.White ? new Position(1, 1) : new Position(-1, 1));
            var attackList = new List<Position> {attackDeltaRight, attackDeltaLeft};

            foreach (var attackPosition in attackList)
            {
                var enPassantDelta = Colour == Colour.White
                    ? new Position(attackPosition.Row - 1, attackPosition.Column)
                    : new Position(attackPosition.Row + 1, attackPosition.Column);

                if (pawnMoveRequest.Pieces.ContainsKey(attackPosition)
                    && pawnMoveRequest.Pieces[attackPosition].Colour != Colour ||
                    CanEnPassant(pawnMoveRequest, enPassantDelta))
                {
                    moveList.Add(attackPosition);
                }
            }
            return moveList;
        }

        private bool CanEnPassant(PawnMoveRequest pawnMoveRequest, Position enPassantPosition)
        {
            var enPassantRow = Colour == Colour.White ? 4 : 3;
            if (pawnMoveRequest.Pieces.ContainsKey(enPassantPosition) 
                && pawnMoveRequest.Pieces[enPassantPosition] is Pawn pawn)
            {
                if (pawn.Colour != Colour && 
                    Position.Row == enPassantRow && 
                    pawnMoveRequest.MoveNumber - pawn.LastMoved == 1)
                {
                    return true;
                }
            }

            return false;

        }
    }
}
