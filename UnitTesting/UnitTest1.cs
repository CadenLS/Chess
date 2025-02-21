using NUnit.Framework;
using ChessGameLogic;

namespace UnitTesting
{

    public class Tests
    {
        private GameTypes selectedGameType;
        private Board gameBoard;

        [SetUp]
        public void SetUp()
        {
            selectedGameType = GameTypes.Chess;
            gameBoard = new Board(selectedGameType);
        }

        [Test]
        public void AddStartPiecesWhiteView_ShouldSetCorrectPiecesForChess()
        {
            gameBoard.gameTypes = GameTypes.Chess;
            gameBoard.AddStartPiecesWhiteView();

            Assert.IsInstanceOf<Rook>(gameBoard[7, 0]);
            Assert.IsInstanceOf<Knight>(gameBoard[7, 1]);
            Assert.IsInstanceOf<Bishop>(gameBoard[7, 2]);
            Assert.IsInstanceOf<Queen>(gameBoard[7, 3]);
            Assert.IsInstanceOf<King>(gameBoard[7, 4]);
            Assert.IsInstanceOf<Bishop>(gameBoard[7, 5]);
            Assert.IsInstanceOf<Knight>(gameBoard[7, 6]);
            Assert.IsInstanceOf<Rook>(gameBoard[7, 7]);

            for (int i = 0; i <= 7; i++)
            {
                Assert.IsInstanceOf<Pawn>(gameBoard[6, i]);
            }
        }

        [Test]
        public void AddStartPiecesBlackView_ShouldSetCorrectPiecesForChess()
        {
            gameBoard.gameTypes = GameTypes.Chess;
            gameBoard.AddStartPiecesBlackView();

            Assert.IsInstanceOf<Rook>(gameBoard[0, 0]);
            Assert.IsInstanceOf<Knight>(gameBoard[0, 1]);
            Assert.IsInstanceOf<Bishop>(gameBoard[0, 2]);
            Assert.IsInstanceOf<Queen>(gameBoard[0, 3]);
            Assert.IsInstanceOf<King>(gameBoard[0, 4]);
            Assert.IsInstanceOf<Bishop>(gameBoard[0, 5]);
            Assert.IsInstanceOf<Knight>(gameBoard[0, 6]);
            Assert.IsInstanceOf<Rook>(gameBoard[0, 7]);

            for (int i = 0; i <= 7; i++)
            {
                Assert.IsInstanceOf<Pawn>(gameBoard[1, i]);
            }
        }

        [Test]
        public void AddStartPiecesWhiteView_ShouldSetCorrectPiecesForChess960()
        {
            gameBoard.gameTypes = GameTypes.Chess960;
            gameBoard.AddStartPiecesWhiteView();


            int whiteKingCol = -1;
            int whiteRook1Col = -1, whiteRook2Col = -1;

            for (int row = 6; row <= 7; row++)
            {
                for (int col = 0; col <= 7; col++)
                {
                    var piece = gameBoard[row, col];

                    if (piece is King)
                    {
                        whiteKingCol = col;
                    }
                    if (piece is Rook)
                    {
                        if (whiteRook1Col == -1)
                            whiteRook1Col = col;
                        else
                            whiteRook2Col = col;
                    }
                }
            }

            Assert.IsTrue(
                (whiteKingCol > whiteRook1Col && whiteKingCol < whiteRook2Col) ||
                (whiteKingCol < whiteRook1Col && whiteKingCol > whiteRook2Col),
                "The King must be placed between the two Rooks."
            );
        }

        [Test]
        public void AddStartPiecesBlackView_ShouldSetCorrectPiecesForChess960()
        {
            gameBoard.gameTypes = GameTypes.Chess960;
            gameBoard.AddStartPiecesBlackView();

            int blackKingCol = -1;
            int blackRook1Col = -1, blackRook2Col = -1;

            for (int row = 6; row <= 7; row++)
            {
                for (int col = 0; col <= 7; col++)
                {
                    var piece = gameBoard[row, col];

                    if (piece is King)
                    {
                        blackKingCol = col;
                    }
                    if (piece is Rook)
                    {
                        if (blackRook1Col == -1)
                            blackRook1Col = col;
                        else
                            blackRook2Col = col;
                    }
                }
            }

            Assert.IsTrue(
                (blackKingCol > blackRook1Col && blackKingCol < blackRook2Col) ||
                (blackKingCol < blackRook1Col && blackKingCol > blackRook2Col),
                "The King must be placed between the two Rooks."
            );
        }

        [Test]
        public void AddStartPieces_ShouldPlaceAllPiecesOnBoard()
        {
            gameBoard.gameTypes = GameTypes.Chess;
            gameBoard.AddStartPiecesWhiteView();

            // Check for total count of pieces
            int whitePieceCount = 0;
            int blackPieceCount = 0;

            // Loop through the board and count pieces for both sides
            for (int row = 0; row <= 7; row++)
            {
                for (int col = 0; col <= 7; col++)
                {
                    var piece = gameBoard[row, col];

                    if (piece is Pawn || piece is Rook || piece is Knight || piece is Bishop || piece is Queen || piece is King)
                    {
                        if (row == 6 || row == 7) // White side
                            whitePieceCount++;
                        if (row == 0 || row == 1) // Black side
                            blackPieceCount++;
                    }
                }
            }

            // Assert that both white and black sides have exactly 16 pieces
            Assert.AreEqual(16, whitePieceCount, "White pieces count is incorrect.");
            Assert.AreEqual(16, blackPieceCount, "Black pieces count is incorrect.");
        }
    }
    
}