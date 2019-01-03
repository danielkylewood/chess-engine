using Chess.Domain.Models;

namespace Chess.Domain
{
    public interface IMoveService
    {
        MoveServiceResult MovePiece(MoveServiceRequest moveRequest, int moveNumber);
    }
}
