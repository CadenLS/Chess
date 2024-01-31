using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameLogic
{
    public class Knight:Piece
    {
        public override PieceType Type => PieceType.Knight;
        public override Player Color { get; }
        
        public Knight(Player color)
        {
            this.Color = color;
        }
        public override Piece Copy()
        {
            var copy = new Knight(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
        private static IEnumerable<Position>PotentialToPositions (Position from)
        {
            foreach (var Dir1 in new Direction []{ Direction.North , Direction.South })
            {
                foreach (var Dir2 in new Direction[] { Direction.West, Direction.East })
                {
                    yield return from + 2 * Dir1 + Dir2;
                    yield return from + 2 * Dir2 + Dir1;
                }
            }
        }
        private IEnumerable<Position> MovePositions (Position from, Board board)
        {
            return PotentialToPositions(from).Where(pos=>Board.IsInside(pos) && (board.IsEmpty(pos) || board[pos].Color!=Color));
        }
        public override IEnumerable<Move> GetMoves (Position from,Board board)
        {
            return MovePositions(from, board).Select(to => new NormalMove(from, to));
        }
        
    }
}