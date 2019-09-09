using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Queen : Piece
    {
        public Queen(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            List<Square> moves = new List<Square>();
            Square location = board.FindPiece(this);

            moves.AddRange(GetDiagonalMoves(location, board));
            moves.AddRange(GetLateralMoves(location, board));

            return moves;
        }

        private List<Square> GetDiagonalMoves(Square location, Board board)
        {
            List<Square> moves = new List<Square>();

            moves.AddRange(GetLineOffsetMovements(location, board, 1, 1));
            moves.AddRange(GetLineOffsetMovements(location, board, 1, -1));
            moves.AddRange(GetLineOffsetMovements(location, board, -1, 1));
            moves.AddRange(GetLineOffsetMovements(location, board, -1, -1));

            return moves;
        }

        private List<Square> GetLateralMoves(Square location, Board board)
        {
            List<Square> moves = new List<Square>();

            moves.AddRange(GetLineOffsetMovements(location, board, 1, 0));
            moves.AddRange(GetLineOffsetMovements(location, board, -1, 0));
            moves.AddRange(GetLineOffsetMovements(location, board, 0, 1));
            moves.AddRange(GetLineOffsetMovements(location, board, 0, -1));

            return moves;
        }

        private List<Square> GetLineOffsetMovements(Square location, Board board, int rowDirection, int colDirection)
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
    }
}