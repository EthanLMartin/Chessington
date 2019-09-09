using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chessington.GameEngine.Pieces
{
    public abstract class TravelingPiece : Piece
    {
        protected TravelingPiece(Player player) 
            : base(player) { }

        protected List<Square> GetLineOffsetMovements(Square location, Board board, Direction direction)
        {
            List<Square> moves = new List<Square>();

            Square newLocation = location + direction;

            while (board.IsWithinBounds(newLocation))
            {
                if (!board.IsEmpty(newLocation))
                {
                    if (board.GetPiece(newLocation).Player != this.Player)
                    {
                        moves.Add(newLocation);
                    }
                    break;
                }

                moves.Add(newLocation);

                newLocation += direction;
            }

            return moves;
        }
    }
}