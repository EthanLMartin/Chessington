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
            List<Square> moves = new List<Square>();

            if (this.Player == Player.White)
            {
                moves.Add(new Square(location.Row - 1, location.Col));

                if (!HasMoved)
                {
                    moves.Add(new Square(location.Row - 2, location.Col));
                }
            }

            if (this.Player == Player.Black)
            {
                moves.Add(new Square(location.Row + 1, location.Col));

                if (!HasMoved)
                {
                    moves.Add(new Square(location.Row + 2, location.Col));
                }
            }

            return moves;
        }
    }
}