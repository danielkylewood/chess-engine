using System;
using System.Collections.Generic;

namespace Chess.Domain.Models.Pieces
{
    public abstract class Piece
    {
        protected readonly Guid Id;
        public readonly Colour Colour;

        public int LastMoved;
        public string ImageName;
        public int NumberMoves;
        public Position Position;

        protected Piece(Colour colour, Position position)
        {
            Id = Guid.NewGuid();
            Colour = colour;
            Position = position;
            NumberMoves = 0;
            LastMoved = 0;
        }

        public abstract List<Position> GetMoves(PieceMoveRequest pieceMoveRequest);

        public void MovePiece(Position position, int moveNumber)
        {
            Position = position;
            NumberMoves += 1;
            LastMoved = moveNumber;
        }

        public override string ToString()
        {
            return Position.ToString();
        }

        public override bool Equals(object other)
        {
            if (!(other is Piece otherPiece))
            {
                return false;
            }

            return Id == otherPiece.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
