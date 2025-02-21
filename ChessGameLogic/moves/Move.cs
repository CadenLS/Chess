using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameLogic
{
    public abstract class Move
    {
        //base class for all the concrent moves;
        public abstract MoveType Type { get; }
        public abstract Position FromPos { get; }
        public abstract Position ToPos { get; }
        public abstract bool Execute(Board board);
        public virtual bool IsLegal(Board board)
        {
            GameTypes gameType = board.gameTypes;
            Player player = board[FromPos].Color;
            Board boardCopy = board.Copy(gameType);
            Execute(boardCopy);
            return !boardCopy.IsInCheck(player);
        }
    }
}
