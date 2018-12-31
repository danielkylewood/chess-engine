using System.Collections.Generic;
using Chess.Domain.Models;
using Chess.Domain.Models.Pieces;

namespace Chess.Domain
{
    public interface IMoveService
    {
        MoveServiceResult MovePiece(MovePiece movePiece);
    }
}
