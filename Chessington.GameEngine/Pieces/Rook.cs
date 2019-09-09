using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Chessington.GameEngine.Pieces
{
    public class Rook : TravelingPiece
    {
        public Rook(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            List<Square> moves = new List<Square>();
            Square location = board.FindPiece(this);

            moves.AddRange(GetLateralMoves(location, board));

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