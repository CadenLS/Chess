using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameLogic
{
    public class StateString
    {
        //build string of the given state help in threefold repetetion rule
        private readonly StringBuilder sb = new StringBuilder();
        public StateString(Player currentPlayer, Board board)
        {
            AddPiecePlacement(board);
            sb.Append(' ');
            AddCurrentPlayer(currentPlayer);
            sb.Append(' ');
            CastlingRights(board);
            sb.Append(' ');
            AddEnPassant(board, currentPlayer);
        }
        public override string ToString()
        {
            return sb.ToString();
        }
        private static char PieceChar(Piece piece)
        {
            if (piece.Color == Player.white)
            {
                if (piece.Type == PieceType.Pawn) return 'p';
                if (piece.Type == PieceType.King) return 'k';
                if (piece.Type == PieceType.Knight) return 'n';
                if (piece.Type == PieceType.Rook) return 'r';
                if (piece.Type == PieceType.Bishop) return 'b';
                if (piece.Type == PieceType.Queen) return 'q';
                return 'd';//dummy not ever reached
            }
            else
            {
                if (piece.Type == PieceType.Pawn) return 'P';
                if (piece.Type == PieceType.King) return 'K';
                if (piece.Type == PieceType.Knight) return 'N';
                if (piece.Type == PieceType.Rook) return 'R';
                if (piece.Type == PieceType.Bishop) return 'B';
                if (piece.Type == PieceType.Queen) return 'Q';
                return 'd';//dummy not ever reached
            }
        }
        private void AddRowData(Board board,int row)
        {
            int empty = 0;
            for(int i = 0; i < 8; i++)
            {
                if (board[row, i] == null)
                {
                    empty++;
                    continue;
                }
                if (empty > 0)
                {
                    sb.Append(empty); empty = 0;
                }
                sb.Append(PieceChar(board[row, i]));
            }
            if (empty > 0)
                sb.Append(empty);

        }
        private void AddPiecePlacement(Board board)
        {
            for(int i=0;i<8;i++)
            {
                if (i != 0)
                {
                    sb.Append('/');
                }
                AddRowData(board, i);
            }       
        }
        private void AddCurrentPlayer(Player CurrentPlayer)
        {
            if(CurrentPlayer == Player.white)
            {
                sb.Append('w');
            }
            else
            {
                sb.Append('b');
            }
        }
        private void CastlingRights(Board board)
        {
            bool CastleKsW = board.CastleRightKs(Player.white);
            bool CastleQsW = board.CastleRightQs(Player.white);
            bool CastleKsB = board.CastleRightKs(Player.Black);
            bool CastleQsB = board.CastleRightQs(Player.Black);
            if (!(CastleKsB || CastleKsW || CastleQsB || CastleQsW))
            {
                sb.Append('-');
                return;
            }
            if (CastleKsW)
            {
                sb.Append('K');
            }
            if (CastleQsW)
            {
                sb.Append('Q');
            }
            if (CastleKsB)
            {
                sb.Append('k');
            }
            if (CastleQsB)
            {
                sb.Append('q');
            }

        }
        private void AddEnPassant(Board board,Player currentPlayer)
        {
            if (!board.CanCaptureEnPassant(currentPlayer))
            {
                sb.Append('-');
                return;
            }
            Position pos = board.GetPawnSkipPosition(currentPlayer.PlayerOpponent());
            char file = (char)('a' + pos.Column);
            int rank = 8 - pos.Row;
            sb.Append(file);
            sb.Append(rank);
        }
    }
}
