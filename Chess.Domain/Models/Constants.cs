using System;
using System.Collections.Generic;
using Chess.Domain.Models.Pieces;

namespace Chess.Domain.Models
{
    public static class Constants
    {
        public static List<Position> BishopDeltas => new List<Position>
        {
            new Position(1, 1),
            new Position(-1, 1),
            new Position(1, -1),
            new Position(-1, -1)
        };

        public static List<Position> KingDeltas => new List<Position>
        {
            new Position(1, 1),
            new Position(0, 1),
            new Position(-1, 1),
            new Position(-1, 0),
            new Position(-1, -1),
            new Position(0, -1),
            new Position(1, -1),
            new Position(1, 0)
        };

        public static List<Position> WhitePawnStartingPositions => new List<Position>
        {
            new Position(1, 0),
            new Position(1, 1),
            new Position(1, 2),
            new Position(1, 3),
            new Position(1, 4),
            new Position(1, 5),
            new Position(1, 6),
            new Position(1, 7)
        };

        public static List<Position> BlackPawnStartingPositions => new List<Position>
        {
            new Position(6, 0),
            new Position(6, 1),
            new Position(6, 2),
            new Position(6, 3),
            new Position(6, 4),
            new Position(6, 5),
            new Position(6, 6),
            new Position(6, 7)
        };

        public static List<Position> BlackRookStartingPositions => new List<Position>
        {
            new Position(7, 0),
            new Position(7, 7)
        };

        public static List<Position> WhiteRookStartingPositions => new List<Position>
        {
            new Position(0, 0),
            new Position(0, 7)
        };

        public static List<Position> BlackKnightStartingPositions => new List<Position>
        {
            new Position(7, 1),
            new Position(7, 6)
        };

        public static List<Position> WhiteKnightStartingPositions => new List<Position>
        {
            new Position(0, 1),
            new Position(0, 6)
        };

        public static List<Position> WhiteBishopStartingPositions => new List<Position>
        {
            new Position(0, 2),
            new Position(0, 5)
        };

        public static List<Position> BlackBishopStartingPositions => new List<Position>
        {
            new Position(7, 2),
            new Position(7, 5)
        };

        public static List<Position> BlackQueenStartingPositions => new List<Position>
        {
            new Position(7, 3)
        };

        public static List<Position> WhiteQueenStartingPositions => new List<Position>
        {
            new Position(0, 3)
        };

        public static List<Position> BlackKingStartingPositions => new List<Position>
        {
            new Position(7, 4)
        };

        public static List<Position> WhiteKingStartingPositions => new List<Position>
        {
            new Position(0, 4)
        };

        public static Dictionary<Position, Piece> StartingPiecePositions()
        {
            var pieces = new Dictionary<Position, Piece>();

            foreach (var collection in PieceCollections)
            {
                foreach (var position in collection.StartingPositions)
                {
                    switch (collection.PieceType)
                    {
                        case PieceType.Pawn: pieces.Add(position, new Pawn(collection.Colour, position));
                            break;
                        case PieceType.Rook: pieces.Add(position, new Rook(collection.Colour, position));
                            break;
                        case PieceType.Knight: pieces.Add(position, new Knight(collection.Colour, position));
                            break;
                        case PieceType.Bishop: pieces.Add(position, new Bishop(collection.Colour, position));
                            break;
                        case PieceType.Queen: pieces.Add(position, new Queen(collection.Colour, position));
                            break;
                        case PieceType.King: pieces.Add(position, new King(collection.Colour, position));
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            return pieces;
        }

        private static IEnumerable<PieceCollection> PieceCollections => new List<PieceCollection>
        {
            new PieceCollection { Colour = Colour.Black, PieceType = PieceType.Pawn, StartingPositions = BlackPawnStartingPositions},
            new PieceCollection { Colour = Colour.Black, PieceType = PieceType.Rook, StartingPositions = BlackRookStartingPositions},
            new PieceCollection { Colour = Colour.Black, PieceType = PieceType.Knight, StartingPositions = BlackKnightStartingPositions},
            new PieceCollection { Colour = Colour.Black, PieceType = PieceType.Bishop, StartingPositions = BlackBishopStartingPositions},
            new PieceCollection { Colour = Colour.Black, PieceType = PieceType.Queen, StartingPositions = BlackQueenStartingPositions},
            new PieceCollection { Colour = Colour.Black, PieceType = PieceType.King, StartingPositions = BlackKingStartingPositions},
            new PieceCollection { Colour = Colour.White, PieceType = PieceType.Pawn, StartingPositions = WhitePawnStartingPositions},
            new PieceCollection { Colour = Colour.White, PieceType = PieceType.Rook, StartingPositions = WhiteRookStartingPositions},
            new PieceCollection { Colour = Colour.White, PieceType = PieceType.Knight, StartingPositions = WhiteKnightStartingPositions},
            new PieceCollection { Colour = Colour.White, PieceType = PieceType.Bishop, StartingPositions = WhiteBishopStartingPositions},
            new PieceCollection { Colour = Colour.White, PieceType = PieceType.Queen, StartingPositions = WhiteQueenStartingPositions},
            new PieceCollection { Colour = Colour.White, PieceType = PieceType.King, StartingPositions = WhiteKingStartingPositions}
        };

        private class PieceCollection
        {
            public Colour Colour { get; set; }
            public PieceType PieceType { get; set; }
            public List<Position> StartingPositions { get; set; }
        }

        private enum PieceType
        {
            Pawn,
            Rook,
            Knight,
            Bishop,
            Queen,
            King
        }
    }
}
