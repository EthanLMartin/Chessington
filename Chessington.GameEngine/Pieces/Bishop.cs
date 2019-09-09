using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            Square location = board.FindPiece(this);
            List<Square> moves = new List<Square>();

            moves.AddRange(GetDiagonalMoves(location, board, 1, 1));
            moves.AddRange(GetDiagonalMoves(location, board, 1, -1));
            moves.AddRange(GetDiagonalMoves(location, board, -1, 1));
            moves.AddRange(GetDiagonalMoves(location, board, -1, -1));

            return moves;
        }

        private List<Square> GetDiagonalMoves(Square location, Board board, int rowDirection, int colDirection)
        {
            List<Square> moves = new List<Square>();

            int rowOffset = rowDirection;
            int colOffset = colDirection;

            while (board.IsWithinBounds(new Square(location.Row + rowOffset, location.Col + colOffset)))
            {
                moves.Add(new Square(location.Row + rowOffset, location.Col + colOffset));
                if (!board.IsEmpty(location.Row + rowOffset, location.Col + colOffset))
                {
                    break;
                }
                rowOffset += rowDirection;
                colOffset += colDirection;
            }

            return moves;
        }

        private List<Square> ClipToBoard(List<Square> moves, Board board)
        {
            moves.RemoveAll(s => !board.IsWithinBounds(s));
            return moves;
        }
    }
}