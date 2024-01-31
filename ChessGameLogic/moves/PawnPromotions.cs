using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameLogic
{
    public class PawnPromotions : Move
    {
        public override MoveType Type => MoveType.PawnPromotion;
        public override Position FromPos {get;}
        public override Position ToPos  {get;}
        private readonly PieceType newType;

        public PawnPromotions(Position from , Position to, PieceType newtype)
        {
            FromPos = from;
            ToPos = to; 
            newType = newtype; 
        }
        private Piece CreatePromotionPiece(Player color)
        {
            if (newType == PieceType.Knight)
            {
                return new Knight(color);
            }
            if (newType == PieceType.Rook)
            {
                return new Rook(color);
            }
            if (newType == PieceType.Queen)
            {
                return new Queen(color);
            }
            return new Bishop(color);
        }
        public override bool Execute(Board board)
        {
            Piece pawn = board[FromPos];
            board[FromPos] = null;
            Piece PromotionPiece = CreatePromotionPiece(pawn.Color);
            PromotionPiece.HasMoved = true;
            board[ToPos] = PromotionPiece;
            return true;
        }
    }
}
