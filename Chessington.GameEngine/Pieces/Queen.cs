using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Queen : TravelingPiece
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

            moves.AddRange(GetLineOffsetMovements(location, board, new Direction(1, 1)));
            moves.AddRange(GetLineOffsetMovements(location, board, new Direction(1, -1)));
            moves.AddRange(GetLineOffsetMovements(location, board, new Direction(-1, 1)));
            moves.AddRange(GetLineOffsetMovements(location, board, new Direction(-1, -1)));

            return moves;
        }

        private List<Square> GetLateralMoves(Square location, Board board)
        {
            List<Square> moves = new List<Square>();

            moves.AddRange(GetLineOffsetMovements(location, board, new Direction(1, 0)));
            moves.AddRange(GetLineOffsetMovements(location, board, new Direction(-1, 0)));
            moves.AddRange(GetLineOffsetMovements(location, board, new Direction(0, 1)));
            moves.AddRange(GetLineOffsetMovements(location, board, new Direction(0, -1)));

            return moves;
        }
    }
}