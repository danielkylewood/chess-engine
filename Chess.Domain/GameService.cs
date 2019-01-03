using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Domain.Models;
using Chess.Domain.Models.Pieces;

namespace Chess.Domain
{
    public class GameService : IGameService
    {
        public GameState _gameState { get; set; }

        private readonly IMoveService _moveService;

        public GameService(IMoveService moveService)
        {
            _moveService = moveService;
        }

        public GameState CreateGame(Guid gameId)
        {
            _gameState = new GameState(gameId);
            _gameState.ValidMoves = GenerateValidMoves();
            return _gameState;
        }

        public GameState ProcessMove(Position start, Position end)
        {
            var movePiece = new MoveServiceRequest
            {
                End = end,
                Start = start,
                Pieces = _gameState.Pieces,
                BlackPieces = _gameState.BlackPieces,
                WhitePieces = _gameState.WhitePieces
            };

            var moveServiceResult = _moveService.MovePiece(movePiece, _gameState.MoveNumber);

            _gameState.MoveNumber += 1;
            _gameState.IsCheck = IsCheck(_gameState);
            _gameState.Pieces = moveServiceResult.Pieces;
            _gameState.BlackPieces = moveServiceResult.BlackPieces;
            _gameState.WhitePieces = moveServiceResult.WhitePieces;
            _gameState.ValidMoves = GenerateValidMoves();
            return _gameState;
        }

        private Dictionary<Piece, List<Position>> GenerateValidMoves()
        {
            var validMoves = new Dictionary<Piece, List<Position>>();
            var piecesToProcess = _gameState.CurrentPlayer == Colour.White ? _gameState.WhitePieces : _gameState.BlackPieces;
            foreach (var piece in piecesToProcess)
            {
                var pieceMoveRequest = PieceMoveFactory.GetPieceMoveRequest(piece, _gameState.Pieces,
                    _gameState.MoveNumber, _gameState.IsCheck);

                var moves = piece.GetMoves(pieceMoveRequest);
                if (moves.Any())
                {
                    validMoves.Add(piece, moves);
                }
            }

            return validMoves;
        }

        private bool IsCheck(GameState gameState)
        {
            var pieces = gameState.CurrentPlayer == Colour.White ? gameState.WhitePieces : gameState.BlackPieces;
            var king = gameState.CurrentPlayer == Colour.White
                ? gameState.WhitePieces.First(x => x is King) as King
                : gameState.BlackPieces.First(x => x is King) as King;

            return king != null && king.IsCheck(_gameState.Pieces).Any();
        }
    }
}
