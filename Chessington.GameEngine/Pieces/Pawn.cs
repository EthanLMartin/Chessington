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
                if (board.GetPiece(new Square(location.Row - 1, location.Col)) == null)
                {
                    moves.Add(new Square(location.Row - 1, location.Col));
                }
                else
                {
                    return moves;
                }

                if (!HasMoved)
                {
                    if (board.GetPiece(new Square(location.Row - 2, location.Col)) == null)
                    {
                        moves.Add(new Square(location.Row - 2, location.Col));
                    }
                }
            }

            if (this.Player == Player.Black)
            {
                if (board.GetPiece(new Square(location.Row + 1, location.Col)) == null)
                {
                    moves.Add(new Square(location.Row + 1, location.Col));
                }
                else
                {
                    return moves;
                }

                if (!HasMoved)
                {
                    if (board.GetPiece(new Square(location.Row + 2, location.Col)) == null)
                    {
                        moves.Add(new Square(location.Row + 2, location.Col));
                    }
                }
            }

            return moves;
        }
    }
}