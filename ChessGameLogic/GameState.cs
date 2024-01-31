using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameLogic
{
    public class GameState
    {
        public Board Board { get; }
        public Player CurrentPalyer { get; private set; }
        public Result Result { get; private set; } = null;
        public int noCaptureOrPawnMoves{ get; set; } = 0;

        private string CurrentState;
        private readonly Dictionary<string,int>StateHistory = new Dictionary<string,int>();
        public GameState(Player p,Board b)
        {
            CurrentPalyer = p;
            Board = b;
            CurrentState = new StateString(CurrentPalyer, Board).ToString();
            StateHistory[CurrentState] = 1;

        }
        public IEnumerable<Move>LegalMovesForPiece(Position pos)
        {
            if (Board.IsEmpty(pos) || Board[pos].Color!=CurrentPalyer)
            {
                return Enumerable.Empty<Move>();
            }
            Piece piece = Board[pos];
            IEnumerable<Move>moveCandidates = piece.GetMoves(pos, Board);
            return moveCandidates.Where(move => move.IsLegal(Board));
        }
        public void MakeMove(Move move)
        {
            Board.SetPawnSkipPosition(CurrentPalyer, null);
            bool capture = move.Execute(Board);
            if(capture)
            {
                noCaptureOrPawnMoves = 0;
                StateHistory.Clear();
            }
            else
            {
                noCaptureOrPawnMoves++;
            }
            CurrentPalyer = CurrentPalyer.PlayerOpponent();
            UpdateStateString();
            CheckForGameOver();

        }
        public IEnumerable<Move>AllLegalMovesFor(Player player)
        {
            IEnumerable<Move> moveCandidates = Board.PiecePositionFor(player).SelectMany(pos =>
            {
                Piece p = Board[pos];
                return p.GetMoves(pos, Board);
            });
            return moveCandidates.Where(move => move.IsLegal(Board));
        }
        public void CheckForGameOver()
        {
            if (!AllLegalMovesFor(CurrentPalyer).Any())
            {
                if (Board.IsInCheck(CurrentPalyer))
                {
                    Result = Result.Win(CurrentPalyer.PlayerOpponent());
                }
                else
                {
                    Result = Result.Draw(EndReason.Stalemate);
                }
            }
            else if (Board.InsufficientMaterial())
            {
                Result = Result.Draw(EndReason.InsufficientMaterial);
            }else if (FiftyMoveRule())
            {
                Result = Result.Draw(EndReason.FiftyMoveRule);
            }else if (CheckForThreeFoldRepetation())
            {
                Result = Result.Draw(EndReason.ThreefoldRepetition);
            }
        }
        public bool IsGameOver()
        {
            return Result != null;
        }
        public bool FiftyMoveRule()
        {
            return noCaptureOrPawnMoves == 100;
        }
        private void UpdateStateString()
        {
            CurrentState = new StateString(CurrentPalyer, Board).ToString();
            if(!StateHistory.ContainsKey(CurrentState))
            {
                StateHistory[CurrentState] = 1;
            }
            else
            {
                StateHistory[CurrentState]++;
            }
        }
        private bool CheckForThreeFoldRepetation()
        {
            return StateHistory[CurrentState] == 3;
        }
    }
}
