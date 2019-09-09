using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : NonTravelingPiece
    {
        public Pawn(Player player) 
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            Square location = board.FindPiece(this);

            switch (this.Player)
            {
                case Player.White:
                    return GetMoves(location, board, new Direction(-1, 0));

                case Player.Black:
                    return GetMoves(location, board, new Direction(1, 0));

                default:
                    return new List<Square>();
            }
        }

        private List<Square> GetMoves(Square location, Board board, Direction direction)
        {
            List<Square> moves = new List<Square>();
            Square moveLocation = location + direction;

            if (board.IsWithinBounds(moveLocation) && board.IsEmpty(moveLocation))
            {
                moves.Add(moveLocation);
            }
            else
            {
                return moves;
            }

            moveLocation += direction;
            if (!HasMoved && board.IsWithinBounds(moveLocation) && board.IsEmpty(moveLocation))
            {
                moves.Add(moveLocation);
            }

            return moves;
        }
    }
}