using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Bishop : TravelingPiece
    {
        public Bishop(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            List<Square> moves = new List<Square>();
            Square location = board.FindPiece(this);

            moves.AddRange(GetDiagonalMoves(location, board));

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
    }
}