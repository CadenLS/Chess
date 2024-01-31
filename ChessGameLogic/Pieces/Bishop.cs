using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameLogic
{
    public class Bishop:Piece
    {
        public override PieceType Type => PieceType.Bishop;
        public override Player Color { get; }

        public static readonly Direction[] dirs = new Direction[]
        {
            Direction.NorthEast,
            Direction.NorthWest,
            Direction.Southeast,
            Direction.Southwest
        };
        public Bishop(Player color)
        {
            this.Color = color;
        }
        public override Piece Copy()
        {
            var copy = new Bishop(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return MovePositionsInDirs(from, board, dirs).Select(to => new NormalMove(from, to));
        }
    }
}
