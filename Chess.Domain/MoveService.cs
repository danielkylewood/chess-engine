using Chess.Domain.Models;
using Chess.Domain.Models.Pieces;

namespace Chess.Domain
{
    public class MoveService : IMoveService
    {

        public MoveServiceResult MovePiece(MovePiece movePiece)
        { 
            var piece = movePiece.Pieces[movePiece.Start];
            movePiece.Pieces.Remove(movePiece.Start);

            piece.Position = movePiece.End;
            if (movePiece.Pieces.ContainsKey(movePiece.End))
            {
                var capturedPiece = movePiece.Pieces[movePiece.End];
                movePiece.Pieces[movePiece.End] = piece;
                if (capturedPiece.Colour == Colour.White)
                {
                    movePiece.WhitePieces.Remove(capturedPiece);
                }
                else
                {
                    movePiece.BlackPieces.Remove(capturedPiece);
                }
            }
            else
            {
                if (IsCastleMove(movePiece.Start, movePiece.End, movePiece.Pieces[movePiece.Start]))
                {
                    ProcessCastleMove(movePiece, piece.Colour);
                }

                movePiece.Pieces.Add(movePiece.End, piece);
            }

            return new MoveServiceResult
            {
                Pieces = movePiece.Pieces,
                BlackPieces = movePiece.BlackPieces,
                WhitePieces = movePiece.WhitePieces
            };
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

        private static void ProcessCastleMove(MovePiece movePiece, Colour pieceColour)
        {
            if (pieceColour == Colour.White)
            {
                if (movePiece.Start.Column - movePiece.End.Column < 0)
                {
                    var rook = movePiece.Pieces[new Position(0, 0)];
                    movePiece.Pieces.Remove(new Position(0, 0));
                    movePiece.Pieces.Add(new Position(0, 3), rook);
                }
                else
                {
                    var rook = movePiece.Pieces[new Position(0, 0)];
                    movePiece.Pieces.Remove(new Position(0, 0));
                    movePiece.Pieces.Add(new Position(0, 3), rook);
                }
            }
            else
            {
                if (movePiece.Start.Column - movePiece.End.Column < 0)
                {
                    var rook = movePiece.Pieces[new Position(7, 0)];
                    movePiece.Pieces.Remove(new Position(7, 0));
                    movePiece.Pieces.Add(new Position(7, 3), rook);
                }
                else
                {
                    var rook = movePiece.Pieces[new Position(7, 0)];
                    movePiece.Pieces.Remove(new Position(7, 0));
                    movePiece.Pieces.Add(new Position(7, 3), rook);
                }
            }
        }
    }
}
