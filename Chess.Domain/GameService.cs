using System;
using Chess.Domain.Models;

namespace Chess.Domain
{
    public class GameService : IGameService
    {
        private GameState _gameState;

        private readonly IMoveService _moveService;

        public GameService(IMoveService moveService)
        {
            _moveService = moveService;
        }

        public GameState CreateGame(Guid gameId)
        {
            _gameState = new GameState(gameId);
            return _gameState;
        }

        public GameState ProcessMove(Position start, Position end)
        {
            var movePiece = new MovePiece
            {
                End = end,
                Start = start,
                Pieces = _gameState.Pieces,
                BlackPieces = _gameState.BlackPieces,
                WhitePieces = _gameState.WhitePieces
            };

            var moveServiceResult = _moveService.MovePiece(movePiece);

            _gameState.Pieces = moveServiceResult.Pieces;
            _gameState.BlackPieces = moveServiceResult.BlackPieces;
            _gameState.WhitePieces = moveServiceResult.WhitePieces;

            // Update valid moves

            return _gameState;
        }
    }
}
