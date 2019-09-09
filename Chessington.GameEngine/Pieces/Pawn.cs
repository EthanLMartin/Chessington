using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Player player) 
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            Square location = board.FindPiece(this);

            switch (this.Player)
            {
                case Player.White:
                    return GetMoves(location, board, -1);

                case Player.Black:
                    return GetMoves(location, board, 1);

                default:
                    return new List<Square>();
            }
        }

        private List<Square> GetMoves(Square location, Board board, int direction)
        {
            List<Square> moves = new List<Square>();
            Square moveLocation = new Square(location.Row + direction, location.Col);

            if (board.IsWithinBounds(moveLocation) && board.IsEmpty(moveLocation))
            {
                moves.Add(new Square(location.Row + direction, location.Col));
            }
            else
            {
                return moves;
            }

            moveLocation = new Square(location.Row + (direction * 2), location.Col);
            if (!HasMoved && board.IsWithinBounds(moveLocation) && board.IsEmpty(moveLocation))
            {
                moves.Add(new Square(location.Row + direction * 2, location.Col));
            }

            return moves;
        }
    }
}