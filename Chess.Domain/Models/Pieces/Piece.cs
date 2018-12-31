using System;
using System.Collections.Generic;

namespace Chess.Domain.Models.Pieces
{
    public abstract class Piece
    {
        protected readonly Guid Id;
        public readonly Colour Colour;
        
        public string ImageName;
        public int NumberMoves;

        private Position _position;
        public Position Position
        {
            get => _position;
            set
            {
                _position = value;
                NumberMoves += 1;
            }
        }

        protected Piece(Colour colour, Position position)
        {
            Id = Guid.NewGuid();
            Colour = colour;
            Position = position;
            NumberMoves = 0;
        }

        public abstract List<Position> GetMoves(IDictionary<Position, Piece> pieces);

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
