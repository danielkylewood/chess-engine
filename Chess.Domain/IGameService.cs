﻿using System;
using Chess.Domain.Models;

namespace Chess.Domain
{
    public interface IGameService
    {
        GameState CreateGame(Guid gameId);
        GameState ProcessMove(Position start, Position end);
    }
}
