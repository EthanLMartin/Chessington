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

            moves.AddRange(GetLineOffsetMovements(location, board, 1, 0));
            moves.AddRange(GetLineOffsetMovements(location, board, -1, 0));
            moves.AddRange(GetLineOffsetMovements(location, board, 0, 1));
            moves.AddRange(GetLineOffsetMovements(location, board, 0, -1));

            return moves;
        }
    }
}