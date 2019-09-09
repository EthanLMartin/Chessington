using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class King : NonTravelingPiece
    {
        public King(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            Square location = board.FindPiece(this);
            List<Square> moves = GetAdjacentMoves(location);

            moves = board.ClipToBoard(moves);
            moves = RemoveFriendlyTakes(moves, board);

            return moves;
        }

        private List<Square> GetAdjacentMoves(Square location)
        {
            List<Square> moves = new List<Square>
            {
                location + new Direction(1, 1),
                location + new Direction(1, -1),
                location + new Direction(-1, 1),
                location + new Direction(-1, -1),
                location + new Direction(0, 1),
                location + new Direction(0, -1),
                location + new Direction(1, 0),
                location + new Direction(-1, 0)
            };

            return moves;
        }
    }
}