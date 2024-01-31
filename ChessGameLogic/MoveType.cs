using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameLogic
{
    public enum MoveType
    {
         Normal,
         CastleKs,
         CastleQs,
         DoublePawn,
         EnPassant,
         PawnPromotion
    }
}
