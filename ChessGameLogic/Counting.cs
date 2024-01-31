using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ChessGameLogic
{
    public class Counting
    {
        private readonly Dictionary<PieceType,int>whiteCount = new Dictionary<PieceType,int>();
        private readonly Dictionary<PieceType,int>blackCount = new Dictionary<PieceType,int>();
        public int TotalCount { get; private set; } = 0;
        public Counting()
        {
            foreach(PieceType type in Enum.GetValues(typeof(PieceType)))
            {
                whiteCount[type] = 0;
                blackCount[type] = 0;
            }
        }
        public void Increment(Player color,PieceType type)
        {
            if(color == Player.white)
            {
                whiteCount[type]++;
            }
            else
            {
                blackCount[type]++;
            }
            TotalCount++;
        }
        public int White(PieceType type)
        {
            return whiteCount[type];
        }
        public int Black(PieceType type)
        {
            return blackCount[type];
        }
    }
}
