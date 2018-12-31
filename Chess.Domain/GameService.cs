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
            var piece = _gameState.Pieces[start];
            _gameState.Pieces.Remove(start);
            
            piece.Position = end;
            if (_gameState.Pieces.ContainsKey(end))
            {
                var capturedPiece = _gameState.Pieces[end];
                _gameState.Pieces[end] = piece;
                if (capturedPiece.Colour == Colour.White)
                {
                    _gameState.WhitePieces.Remove(capturedPiece);
                }
                else
                {
                    _gameState.BlackPieces.Remove(capturedPiece);
                }
            }
            else
            {
                // TODO Check for castling
                _gameState.Pieces.Add(end, piece);
            }

            return _gameState;
        }
    }
}
