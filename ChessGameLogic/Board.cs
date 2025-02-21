using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameLogic
{
    public class Board
    {
        //the board class will store all the active pieces of the game and provide various helper methods
        private readonly Piece[,] pieces = new Piece[8, 8];
        public GameTypes gameTypes;

        private readonly Dictionary<Player, Position> pawnSkipPositions = new Dictionary<Player, Position>
        {
            {Player.white,null },
            {Player.Black,null }
        };
        public Piece this[int row, int column]
        {
            get
            {
                return this.pieces[row, column];
                //null if position is empty
            }
            set
            {
                this.pieces[row, column] = value;
            }
        }
        public Piece this[Position pos]
        {
            get
            {
                return this[pos.Row, pos.Column];
            }
            set
            {
                this[pos.Row, pos.Column] = value;
            }
        }
        public Position GetPawnSkipPosition(Player player)
        {
            return pawnSkipPositions[player];
        }
        public void SetPawnSkipPosition(Player player, Position pos)
        {
            pawnSkipPositions[player] = pos;
        }

        public Board(GameTypes selectedGameType)
        {
            gameTypes = selectedGameType;
        }

        public static Board Initial(GameTypes selectedGameType, bool whiteView = true)
        {
            Board board = new Board(selectedGameType);
            if (whiteView)
            {
                board.AddStartPiecesWhiteView();
            }
            else
            {
                board.AddStartPiecesBlackView();
            }

            return board;
        }

        public void AddPawnStructureWhiteView()
        {
            for (int i = 0; i <= 7; i++)
            {
                this[1, i] = new Pawn(Player.Black);
            }
            for (int i = 0; i <= 7; i++)
            {
                this[6, i] = new Pawn(Player.white);
            }
        }
        public void AddPawnStructureBlackView()
        {
            for (int i = 0; i <= 7; i++)
            {
                this[1, i] = new Pawn(Player.white);
            }
            for (int i = 0; i <= 7; i++)
            {
                this[6, i] = new Pawn(Player.Black);
            }
        }

        public void RegularChessStructureWhiteView()
        {
            this[0, 0] = new Rook(Player.Black);
            this[0, 1] = new Knight(Player.Black);
            this[0, 2] = new Bishop(Player.Black);
            this[0, 3] = new Queen(Player.Black);
            this[0, 4] = new King(Player.Black);
            this[0, 5] = new Bishop(Player.Black);
            this[0, 6] = new Knight(Player.Black);
            this[0, 7] = new Rook(Player.Black);
            AddPawnStructureWhiteView();
            this[7, 0] = new Rook(Player.white);
            this[7, 1] = new Knight(Player.white);
            this[7, 2] = new Bishop(Player.white);
            this[7, 3] = new Queen(Player.white);
            this[7, 4] = new King(Player.white);
            this[7, 5] = new Bishop(Player.white);
            this[7, 6] = new Knight(Player.white);
            this[7, 7] = new Rook(Player.white);
        }
        public void RegularChessStructureBlackView()
        {
            this[0, 0] = new Rook(Player.white);
            this[0, 1] = new Knight(Player.white);
            this[0, 2] = new Bishop(Player.white);
            this[0, 3] = new Queen(Player.white);
            this[0, 4] = new King(Player.white);
            this[0, 5] = new Bishop(Player.white);
            this[0, 6] = new Knight(Player.white);
            this[0, 7] = new Rook(Player.white);
            AddPawnStructureBlackView();
            this[7, 0] = new Rook(Player.Black);
            this[7, 1] = new Knight(Player.Black);
            this[7, 2] = new Bishop(Player.Black);
            this[7, 3] = new Queen(Player.Black);
            this[7, 4] = new King(Player.Black);
            this[7, 5] = new Bishop(Player.Black);
            this[7, 6] = new Knight(Player.Black);
            this[7, 7] = new Rook(Player.Black);
        }

        public void Chess960StructureWhiteView()
        {
            Random randomGen = new Random();

            List<int> positions = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7 };
            List<int> bisheven = new List<int>{ 0, 2, 4, 6 };
            List<int> bish2odds = new List<int>{ 1, 3, 5, 7 };

            int kingPos = randomGen.Next(1, 6);
            positions.Remove(kingPos);
            this[7, kingPos] = new King(Player.white);
            this[0, kingPos] = new King(Player.Black);

            int leftRookPos = randomGen.Next(0, kingPos);
            positions.Remove(leftRookPos);
            this[7, leftRookPos] = new Rook(Player.white);
            this[0, leftRookPos] = new Rook(Player.Black);

            int rightRookPos = randomGen.Next(kingPos + 1, 8);
            positions.Remove(rightRookPos);
            this[7, rightRookPos] = new Rook(Player.white);
            this[0, rightRookPos] = new Rook(Player.Black);

            List<int> availableEven = bisheven.Where(pos => positions.Contains(pos)).ToList();
            int bishPos1 = availableEven[randomGen.Next(availableEven.Count)];
            positions.Remove(bishPos1);
            this[7, bishPos1] = new Bishop(Player.white);
            this[0, bishPos1] = new Bishop(Player.Black);

            List<int> availableOdd = bish2odds.Where(pos => positions.Contains(pos)).ToList();
            int bishPos2 = availableOdd[randomGen.Next(availableOdd.Count)];
            positions.Remove(bishPos2);
            this[7, bishPos2] = new Bishop(Player.white);
            this[0, bishPos2] = new Bishop(Player.Black);


            int queenPos = positions[randomGen.Next(positions.Count)];
            positions.Remove(queenPos);
            this[7, queenPos] = new Queen(Player.white);
            this[0, queenPos] = new Queen(Player.Black);

            foreach (int pos in positions)
            {
                this[7, pos] = new Knight(Player.white);
                this[0, pos] = new Knight(Player.Black);
            }

            AddPawnStructureWhiteView();
        }
        public void Chess960StructureBlackView()
        {
            Random randomGen = new Random();

            List<int> positions = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };
            List<int> bisheven = new List<int> { 0, 2, 4, 6 };
            List<int> bish2odds = new List<int> { 1, 3, 5, 7 };

            int kingPos = randomGen.Next(1, 6);
            positions.Remove(kingPos);
            this[7, kingPos] = new King(Player.Black);
            this[0, kingPos] = new King(Player.white);

            int leftRookPos = randomGen.Next(0, kingPos);
            positions.Remove(leftRookPos);
            this[7, leftRookPos] = new Rook(Player.Black);
            this[0, leftRookPos] = new Rook(Player.white);

            int rightRookPos = randomGen.Next(kingPos + 1, 8);
            positions.Remove(rightRookPos);
            this[7, rightRookPos] = new Rook(Player.Black);
            this[0, rightRookPos] = new Rook(Player.white);

            List<int> availableEven = bisheven.Where(pos => positions.Contains(pos)).ToList();
            int bishPos1 = availableEven[randomGen.Next(availableEven.Count)];
            positions.Remove(bishPos1);
            this[7, bishPos1] = new Bishop(Player.Black);
            this[0, bishPos1] = new Bishop(Player.white);

            List<int> availableOdd = bish2odds.Where(pos => positions.Contains(pos)).ToList();
            int bishPos2 = availableOdd[randomGen.Next(availableOdd.Count)];
            positions.Remove(bishPos2);
            this[7, bishPos2] = new Bishop(Player.Black);
            this[0, bishPos2] = new Bishop(Player.white);


            int queenPos = positions[randomGen.Next(positions.Count)];
            positions.Remove(queenPos);
            this[7, queenPos] = new Queen(Player.Black);
            this[0, queenPos] = new Queen(Player.white);

            foreach (int pos in positions)
            {
                this[7, pos] = new Knight(Player.white);
                this[0, pos] = new Knight(Player.white);
            }

            AddPawnStructureBlackView();
        }

        public void AddStartPiecesWhiteView()
        {
            if (gameTypes == GameTypes.Chess) RegularChessStructureWhiteView();
            else if (gameTypes == GameTypes.Chess960) Chess960StructureWhiteView();
        }

        public void AddStartPiecesBlackView()
        {
            if (gameTypes == GameTypes.Chess) RegularChessStructureBlackView();
            else if (gameTypes == GameTypes.Chess960) Chess960StructureBlackView();
        }

        public static bool IsInside(Position pos)
        {
            return pos.Row >= 0 && pos.Row < 8 && pos.Column >= 0 && pos.Column < 8;
        }
        public bool IsEmpty(Position pos)
        {
            return this[pos] == null;
        }

        public IEnumerable<Position> PiecePositions()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Position pos = new Position(i, j);
                    if (!IsEmpty(pos)) yield return pos;
                }
            }
        }
        public IEnumerable<Position> PiecePositionFor(Player player)
        {
            return PiecePositions().Where(pos => this[pos].Color == player);
        }
        public bool IsInCheck(Player player)
        {
            return PiecePositionFor(player.PlayerOpponent()).Any(pos =>
            {
                Piece piece = this[pos];
                return piece.CanCaptureOpponentKing(pos, this);
            });
        }
        public Board Copy(GameTypes selectedGameType)
        {
            gameTypes = selectedGameType;
            Board copy = new Board(selectedGameType);
            foreach (var pos in PiecePositions())
            {
                copy[pos] = this[pos].Copy();
            }
            return copy;
        }
        public Counting CountPieces()
        {
            Counting counting = new Counting();
            foreach (Position pos in PiecePositions())
            {
                Piece piece = this[pos];
                counting.Increment(piece.Color, piece.Type);
            }
            return counting;
        }
        public bool InsufficientMaterial()
        {
            Counting counting = CountPieces();

            return IsKingVsKing(counting)||IsKingBishopVsKing(counting)||IsKingKnightVsKing(counting)||IsKingBishopVsKingBishop(counting) ;
        }

        private bool IsKingVsKing(Counting counting)
        {
            return counting.TotalCount == 2;
        }
        private bool IsKingBishopVsKing(Counting counting)
        {
            return counting.TotalCount == 3 && (counting.White(PieceType.Bishop)==1||counting.Black(PieceType.Bishop)==1);
        }
        private bool IsKingKnightVsKing(Counting counting)
        {
            return counting.TotalCount == 3 && (counting.White(PieceType.Knight) == 1 || counting.Black(PieceType.Knight) == 1);
        }
        private bool IsKingBishopVsKingBishop(Counting counting)
        {
            if (counting.TotalCount != 4) return false;
            if (counting.White(PieceType.Bishop) != 1) return false;
            if (counting.Black(PieceType.Bishop) != 1) return false;
            Position p1 = FindPiece(Player.white, PieceType.Bishop);
            Position p2 = FindPiece(Player.Black, PieceType.Bishop);
            return p1.SquareColor() == p2.SquareColor();
        }
        private Position FindPiece(Player color,PieceType type)
        {
            return PiecePositionFor(color).First(pos => this[pos].Type == type);    
        }
        private bool IsUnMovedKingAndRook(Position KingPos,Position rookPos)
        {
            if (IsEmpty(KingPos) || IsEmpty(rookPos)) return false;
            Piece king = this[KingPos];
            Piece rook = this[rookPos];

            return king.Type == PieceType.King && rook.Type == PieceType.Rook && king.HasMoved == false && rook.HasMoved == false;
        }
        public bool CastleRightKs(Player player)
        {
            if (player == Player.Black)
            {
                return IsUnMovedKingAndRook(new Position(0, 4), new Position(0, 7));
            }
            else if(player == Player.white)
            {
                return IsUnMovedKingAndRook(new Position(7, 4), new Position(7, 7));
            }
            else
            {
                return false;//unreachable case
            }
        }
        public bool CastleRightQs(Player player)
        {
            if (player == Player.Black)
            {
                return IsUnMovedKingAndRook(new Position(0, 4), new Position(0, 0));
            }
            else if (player == Player.white)
            {
                return IsUnMovedKingAndRook(new Position(7, 4), new Position(7, 0));
            }
            else
            {
                return false;//unreachable case
            }
        }

        public bool CanCaptureEnPassant(Player player)
        {
            Position SkipPos = GetPawnSkipPosition(player.PlayerOpponent());
            if(SkipPos == null)
            {
                return false;
            }
            Position[] pawnPositions = player switch
            {
                Player.white => new Position[] { SkipPos + Direction.Southwest, SkipPos + Direction.Southeast },
                Player.Black => new Position[] { SkipPos + Direction.NorthEast, SkipPos + Direction.NorthWest },
                _ => Array.Empty<Position>()
            };
            return HasPawnInPosition(player, pawnPositions, SkipPos);

        }
        public bool HasPawnInPosition(Player player,Position[] PawnPositions,Position skipPos)
        {
            foreach(Position pos in PawnPositions.Where(IsInside))
            {
                Piece piece = this[pos];
                if (piece == null || piece.Color != player || piece.Type != PieceType.Pawn) continue;

                EnPassant move = new EnPassant(pos, skipPos);
                if (move.IsLegal(this)) return true;
            }
            return false;
        }
    }

}
