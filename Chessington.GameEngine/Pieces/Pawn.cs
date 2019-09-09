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
                    return GetMovesWhite(location, board);

                case Player.Black:
                    return GetMovesBlack(location, board);

                default:
                    return new List<Square>();
            }
        }

        private List<Square> GetMovesWhite(Square location, Board board)
        {
            List<Square> moves = new List<Square>();

            if (board.IsEmpty(location.Row - 1, location.Col))
            {
                moves.Add(new Square(location.Row - 1, location.Col));
            }
            else
            {
                return moves;
            }

            if (!HasMoved && board.IsEmpty(location.Row - 2, location.Col))
            {
                moves.Add(new Square(location.Row - 2, location.Col));
            }

            return moves;
        }

        private List<Square> GetMovesBlack(Square location, Board board)
        {
            List<Square> moves = new List<Square>();

            if (board.IsEmpty(location.Row + 1, location.Col))
            {
                moves.Add(new Square(location.Row + 1, location.Col));
            }
            else
            {
                return moves;
            }

            if (!HasMoved && board.IsEmpty(location.Row + 2, location.Col))
            {
                moves.Add(new Square(location.Row + 2, location.Col));
            }

            return moves;
        }
    }
}