using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameLogic
{
    public class Queen:Piece
    {
        public override PieceType Type => PieceType.Queen;
        public override Player Color { get; }
        public static readonly Direction[] dirs = new Direction[]
        {
            Direction.North,
            Direction.South,
            Direction.West,
            Direction.East,
            Direction.NorthEast,
            Direction.NorthWest,
            Direction.Southeast,
            Direction.Southwest
        };
        public Queen(Player color)
        {
            this.Color = color;
        }
        public override Piece Copy()
        {
            Queen copy = new Queen(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return MovePositionsInDirs(from, board, dirs).Select(to => new NormalMove(from, to));
        }
    }
}
