using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Knight : NonTravelingPiece
    {
        public Knight(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            Square location = board.FindPiece(this);
            List<Square> moves = GetLMoves(location);

            moves = board.ClipToBoard(moves);
            moves = RemoveFriendlyTakes(moves, board);

            return moves;
        }

        private List<Square> GetLMoves(Square location)
        {
            List<Square> moves = new List<Square>
            {
                location + new Direction(1, 2),
                location + new Direction(1, -2),
                location + new Direction(-1, 2),
                location + new Direction(-1, -2),
                location + new Direction(2, 1),
                location + new Direction(2, -1),
                location + new Direction(-2, 1),
                location + new Direction(-2, -1)
            };

            return moves;
        }
    }
}