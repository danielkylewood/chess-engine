﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Domain.Models.Pieces
{
    public class King : Piece
    {
        public King(Colour colour, Position position) : base(colour, position)
        {
            ImageName = $"{colour.ToString().ToLowerInvariant()}-king.png";
        }

        public override List<Position> GetMoves(IDictionary<Position, Piece> pieces)
        {
            var moveList = new List<Position>();
            foreach (var delta in Constants.KingDeltas)
            {
                var position = Position.Clone(Position) + delta;
                if (position.IsValid() &&
                   !(pieces.ContainsKey(position) && pieces[position].Colour == Colour))
                {
                    moveList.Add(position);
                }
            }

            if (CanCastle(pieces, out var castleDirection))
            {
                switch (castleDirection)
                {
                    case CastleDirection.None:
                        break;
                    case CastleDirection.Left:
                        moveList.AddRange(GetLeftCastleSquares());
                        break;
                    case CastleDirection.Right:
                        moveList.AddRange(GetRightCastleSquares());
                        break;
                    case CastleDirection.Both:
                        moveList.AddRange(GetLeftCastleSquares());
                        moveList.AddRange(GetRightCastleSquares());
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return moveList;
        }

        private IEnumerable<Position> GetLeftCastleSquares()
        {
            return new List<Position>
            {
                new Position(Position.Row, Position.Column - 1),
                new Position(Position.Row, Position.Column - 2),
                new Position(Position.Row, Position.Column - 3)
            };
        }

        private IEnumerable<Position> GetRightCastleSquares()
        {
            return new List<Position>
            {
                new Position(Position.Row, Position.Column + 1),
                new Position(Position.Row, Position.Column + 2)
            };
        }

        private bool CanCastle(IDictionary<Position, Piece> pieces, out CastleDirection castleDirection)
        {
            castleDirection = CastleDirection.None;
            if (NumberMoves > 0)
            {
                return false;
            }

            var startingRookPositions = Colour == Colour.White
                ? Constants.WhiteRookStartingPositions
                : Constants.BlackRookStartingPositions;

            var castleBlockerLeft = false;
            var castleBlockerRight = false;

            var castleDeltaLeftOne = new Position(0, -1);
            var castleDeltaLeftTwo = new Position(0, -2);
            var castleDeltaLeftThree = new Position(0, -3);
            var castleDeltaRightOne = new Position(0, 1);
            var castleDeltaRightTwo = new Position(0, 2);

            if (pieces.ContainsKey(Position + castleDeltaLeftOne) ||
                pieces.ContainsKey(Position + castleDeltaLeftTwo) ||
                pieces.ContainsKey(Position + castleDeltaLeftThree))
            {
                castleBlockerLeft = true;
            }

            if (pieces.ContainsKey(Position + castleDeltaRightOne) ||
                pieces.ContainsKey(Position + castleDeltaRightTwo))
            {
                castleBlockerRight = true;
            }

            if (castleBlockerLeft && castleBlockerRight)
            {
                return false;
            }

            var validRooks = FetchValidCastlingRooks(pieces, startingRookPositions);

            if (!validRooks.Any())
            {
                return false;
            }

            if (castleBlockerLeft)
                castleDirection = CastleDirection.Right;
            else if (castleBlockerRight)
                castleDirection = CastleDirection.Left;
            else
                castleDirection = CastleDirection.Both;
            
            return true;
        }

        private IEnumerable<Rook> FetchValidCastlingRooks(IDictionary<Position, Piece> pieces, IEnumerable<Position> startingRookPositions)
        {
            var validRookList = new List<Rook>();
            foreach (var position in startingRookPositions)
            {
                if (pieces.ContainsKey(position) && 
                    pieces[position] is Rook rook &&
                    rook.NumberMoves == 0 &&
                    rook.Colour == Colour)
                {
                    validRookList.Add(rook);
                }
            }

            return validRookList;
        }

        private enum CastleDirection
        {
            None,
            Left,
            Right,
            Both
        }
    }
}
