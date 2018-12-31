using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Domain.Models.Pieces;

namespace Chess.Domain.Models
{
    public class GameState
    {
        public readonly Guid GameId;
        public readonly Dictionary<Position, Piece> Pieces;
        public readonly HashSet<Piece> WhitePieces;
        public readonly HashSet<Piece> BlackPieces;
        public readonly Dictionary<Piece, List<Position>> ValidMoves;

        public Colour CurrentPlayer;

        public GameState(Guid gameId)
        {
            GameId = gameId;
            CurrentPlayer = Colour.White;
            ValidMoves = new Dictionary<Piece, List<Position>>();
            Pieces = Constants.StartingPiecePositions();
            WhitePieces = Pieces.Select(x => x.Value).Where(x => x.Colour == Colour.White).ToHashSet();
            BlackPieces = Pieces.Select(x => x.Value).Where(x => x.Colour == Colour.Black).ToHashSet();
        }
    }
}
