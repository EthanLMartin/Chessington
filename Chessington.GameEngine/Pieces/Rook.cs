using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Chessington.GameEngine.Pieces
{
    public class Rook : Piece
    {
        public Rook(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            List<Square> moves = new List<Square>();
            Square location = board.FindPiece(this);

            moves.AddRange(GetLateralMovements(location, board, 1, 0));
            moves.AddRange(GetLateralMovements(location, board, -1, 0));
            moves.AddRange(GetLateralMovements(location, board, 0, 1));
            moves.AddRange(GetLateralMovements(location, board, 0, -1));

            return moves;
        }

        private List<Square> GetLateralMovements(Square location, Board board, int rowDirection, int colDirection)
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