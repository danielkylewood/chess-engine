using System;
using Chess.Domain.Models;

namespace Chess.Domain
{
    public interface IGameService
    {
        GameState _gameState { get; set; }
        GameState CreateGame(Guid gameId);
        GameState ProcessMove(Position start, Position end);
    }
}
