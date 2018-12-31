using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Domain.Models.Pieces;

namespace Chess.Domain.Models
{
    public class GameState
    {
        public int MoveNumber { get; set; }

        public readonly Guid GameId;
        public IDictionary<Position, Piece> Pieces;
        public HashSet<Piece> WhitePieces;
        public HashSet<Piece> BlackPieces;
        public IDictionary<Piece, List<Position>> ValidMoves;

        public Colour CurrentPlayer;

        public GameState(Guid gameId)
        {
            MoveNumber = 0;
            GameId = gameId;
            CurrentPlayer = Colour.White;
            ValidMoves = new Dictionary<Piece, List<Position>>();
            Pieces = Constants.StartingPiecePositions();
            WhitePieces = Pieces.Select(x => x.Value).Where(x => x.Colour == Colour.White).ToHashSet();
            BlackPieces = Pieces.Select(x => x.Value).Where(x => x.Colour == Colour.Black).ToHashSet();
        }
    }
}
