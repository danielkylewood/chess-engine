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
            var enPassantDeltaLeft = new Position(0, -1);
            var enPassantDeltaRight = new Position(0, 1);

            if (pawnMoveRequest.Pieces.ContainsKey(attackDeltaLeft)
                && pawnMoveRequest.Pieces[attackDeltaLeft].Colour != Colour ||
                CanEnPassant(pawnMoveRequest, enPassantDeltaLeft))
            {
                moveList.Add(attackDeltaLeft);
            }

            if (pawnMoveRequest.Pieces.ContainsKey(attackDeltaRight) &&
                pawnMoveRequest.Pieces[attackDeltaRight].Colour != Colour ||
                CanEnPassant(pawnMoveRequest, enPassantDeltaRight))
            {
                moveList.Add(attackDeltaRight);
            }

                return moveList;
        }

        private bool CanEnPassant(PawnMoveRequest pawnMoveRequest, Position enPassantDelta)
        {
            var enPassantRow = Colour == Colour.White ? 4 : 3;
            if (pawnMoveRequest.Pieces.ContainsKey(Position + enPassantDelta) 
                && pawnMoveRequest.Pieces[Position + enPassantDelta] is Pawn pawn)
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
