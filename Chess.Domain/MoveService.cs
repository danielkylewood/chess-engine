using Chess.Domain.Models;
using Chess.Domain.Models.Pieces;

namespace Chess.Domain
{
    public class MoveService : IMoveService
    {
        public MoveServiceResult MovePiece(MoveServiceRequest moveRequest, int moveNumber)
        { 
            var piece = moveRequest.Pieces[moveRequest.Start];
            if (moveRequest.Pieces.ContainsKey(moveRequest.End))
            {
                RemovePieceFromBoard(moveRequest, moveRequest.End);
            }
            else
            {
                if (IsCastleMove(moveRequest.Start, moveRequest.End, piece))
                {
                    ProcessCastleMove(moveRequest, piece.Colour, moveNumber);
                }
                else if (piece is Pawn && moveRequest.Start.Column != moveRequest.End.Column)
                {
                    var enPassantPawnPosition = new Position(moveRequest.Start.Row, moveRequest.End.Column);
                    RemovePieceFromBoard(moveRequest, enPassantPawnPosition);
                }
            }

            moveRequest.Pieces.Remove(moveRequest.Start);
            moveRequest.Pieces.Add(moveRequest.End, piece);
            piece.MovePiece(moveRequest.End, moveNumber);

            return new MoveServiceResult
            {
                Pieces = moveRequest.Pieces,
                BlackPieces = moveRequest.BlackPieces,
                WhitePieces = moveRequest.WhitePieces
            };
        }

        public void RemovePieceFromBoard(MoveServiceRequest moveRequest, Position piecePosition)
        {
            var capturedPiece = moveRequest.Pieces[piecePosition];
            moveRequest.Pieces.Remove(piecePosition);
            if (capturedPiece.Colour == Colour.White)
            {
                moveRequest.WhitePieces.Remove(capturedPiece);
            }
            else
            {
                moveRequest.BlackPieces.Remove(capturedPiece);
            }
        }

        private static bool IsCastleMove(Position start, Position end, Piece piece)
        {
            if (!(piece is King))
            {
                return false;
            }

            var castleDelta = start.Column - end.Column;
            return castleDelta == 2 || castleDelta == -2;
        }

        private static void ProcessCastleMove(MoveServiceRequest moveServiceRequest, Colour pieceColour, int moveNumber)
        {
            if (pieceColour == Colour.White)
            {
                if (moveServiceRequest.Start.Column - moveServiceRequest.End.Column < 0)
                {
                    var rook = moveServiceRequest.Pieces[new Position(0, 7)];
                    moveServiceRequest.Pieces.Remove(new Position(0, 7));
                    rook.MovePiece(new Position(0, 5), moveNumber);
                    moveServiceRequest.Pieces.Add(new Position(0, 5), rook);
                }
                else
                {
                    var rook = moveServiceRequest.Pieces[new Position(0, 0)];
                    moveServiceRequest.Pieces.Remove(new Position(0, 0));
                    rook.MovePiece(new Position(0, 3), moveNumber);
                    moveServiceRequest.Pieces.Add(new Position(0, 3), rook);
                }
            }
            else
            {
                if (moveServiceRequest.Start.Column - moveServiceRequest.End.Column < 0)
                {
                    var rook = moveServiceRequest.Pieces[new Position(7, 7)];
                    moveServiceRequest.Pieces.Remove(new Position(7, 7));
                    rook.MovePiece(new Position(7, 5), moveNumber);
                    moveServiceRequest.Pieces.Add(new Position(7, 5), rook);
                }
                else
                {
                    var rook = moveServiceRequest.Pieces[new Position(7, 0)];
                    moveServiceRequest.Pieces.Remove(new Position(7, 0));
                    rook.MovePiece(new Position(7, 3), moveNumber);
                    moveServiceRequest.Pieces.Add(new Position(7, 3), rook);
                }
            }
        }
    }
}
