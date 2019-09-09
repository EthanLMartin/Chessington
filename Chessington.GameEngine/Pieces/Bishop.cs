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

        private List<Square> GetDiagonalMoves(Square location, Board board, int rowOffset, int colOffset)
        {
            List<Square> moves = new List<Square>();

            int curRowOffset = rowOffset;
            int curColOffset = colOffset;

            Square newLocation = new Square(location.Row + curRowOffset, location.Col + curColOffset);

            while (board.IsWithinBounds(newLocation))
            {
                moves.Add(newLocation);

                if (!board.IsEmpty(location.Row + curRowOffset, location.Col + curColOffset))
                {
                    break;
                }

                curRowOffset += rowOffset;
                curColOffset += colOffset;
                newLocation = new Square(location.Row + curRowOffset, location.Col + curColOffset);
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