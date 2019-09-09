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
            Square location = board.FindPiece(this);
            List<Square> moves = new List<Square>();

            moves.AddRange(GetLineOffsetMovements(location, board, 1, 1));
            moves.AddRange(GetLineOffsetMovements(location, board, 1, -1));
            moves.AddRange(GetLineOffsetMovements(location, board, -1, 1));
            moves.AddRange(GetLineOffsetMovements(location, board, -1, -1));

            return moves;
        }
    }
}