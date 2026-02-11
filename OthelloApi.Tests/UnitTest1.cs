
using NUnit.Framework;
using OthelloAPI.Services;
using OthelloAPI.Models;
using OthelloAPI.Common;
using Moq;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace OthelloAPI.Tests
{
    // [TestFixture]
    public class GameControllerTests
    {
        private GameController _game;
        private Mock<ILogger<GameController>> _mockLogger;

        [SetUp]
        public void Setup()
        {
            // Mock logger
            _mockLogger = new Mock<ILogger<GameController>>();

            // Buat players
            var players = new List<IPlayer>
            {
                new Player("Alice", PlayerColor.Black),
                new Player("Bob", PlayerColor.White)
            };

            // Buat board 4x4 untuk test cepat
            var board = new Board(4);
            for (int r = 0; r < 4; r++)
                for (int c = 0; c < 4; c++)
                    board.Cells[r, c] = new Cell(new Position(r, c));

            // Set posisi awal Othello
            int mid = board.Size / 2;
            board.Cells[mid - 1, mid - 1].Piece = new Piece(PieceColor.White);
            board.Cells[mid, mid].Piece = new Piece(PieceColor.White);
            board.Cells[mid - 1, mid].Piece = new Piece(PieceColor.Black);
            board.Cells[mid, mid - 1].Piece = new Piece(PieceColor.Black);

            // Buat GameController
            _game = new GameController(_mockLogger.Object);
            _game.StartNewGame(players, board);
        }

        // [Test]
        // public void StartNewGame_InitializeBoardAndPlayers()
        // //check initialisasi
        // {
        //     var board = _game.GetBoard();
        //     Assert.That(_game.CurrentPlayer, Is.Not.Null);
        //     Assert.That(board.Cells.Cast<Cell>().Count(c => c.Piece != null), Is.EqualTo(4), "Board harus punya 4 pion awal");
        //     Assert.That(_game.IsGameOver, Is.False, "Game harus belum over saat start");
        // }

        [Test]
        public void PlayAt_ValidMove_ReturnTrue()
        //move valid
        {
            var pos = new Position(3, 2);
            // var playerBeforeMove = _game.CurrentPlayer;

            var result = _game.PlayAt(pos);

            Assert.That(result.Success, Is.True, "Move valid seharusnya sukses");
            // Assert.That(_game.GetBoard().Cells[pos.Row, pos.Col].Piece, Is.EqualTo((Piece)playerBeforeMove.Color));
        }

      
        [TestCase(1,1)]
        [TestCase(1,2)]
        [TestCase(0,0)]
        public void PlayAt_InvalidMove_ShouldFail(int row, int col)
        //move tidak valid
        {
            var pos = new Position(row, col);
            var result = _game.PlayAt(pos);

            Assert.That(result.Success, Is.False, "Move invalid seharusnya gagal");
        }

      [Test]
    public void PlayAt_SetFullBoard_ReturnFalse()
    //play at dengan board penuh
    {
        var board = _game.GetBoard();

        // Isi seluruh board
        for (int r = 0; r < 4; r++)
            for (int c = 0; c < 4; c++)
                board.Cells[r, c].Piece = new Piece(PieceColor.Black);

        // Optional: cek board penuh
            Assert.That(board.Cells.Cast<Cell>().All(c => c.Piece != null), Is.True);

            var invalidPos = new Position(3, 2);
            var result =  _game.PlayAt(invalidPos);
    
        Assert.That(result.Success, Is.False, "Game over karena kedua pemain harus pass");
    
    }
        [Test]
        public void IsValidMove_InputValid_ReturnTrue()
        //taruh di piece di posisi valid
        {
            var pos = new Position(3, 2);
            _game.GetBoard().Cells[pos.Row, pos.Col].Piece = null;
            var result = _game.IsValidMove(pos, PlayerColor.Black);

            Assert.That(result, Is.True, "Posisi kosong seharusnya valid move");
        }

        [Test]
        public void IsValidMove_InputInvalid_ReturnFalse()
        //taruh piece di tempat yang sudah ada piecenya
        {
            var pos = new Position(1, 1);
            _game.GetBoard().Cells[pos.Row, pos.Col].Piece =new Piece(PieceColor.Black);

            var result = _game.IsValidMove(pos, PlayerColor.Black);

            Assert.That(result, Is.False, "Posisi sudah diisi seharusnya invalid move");
        }

        [Test]
        public void GetScore_ReturnCorrectScore()
        {
            var result = _game.GetScore();

            Assert.That(result.Success, Is.True);
            Assert.That(result.Data.Black, Is.EqualTo(2));
            Assert.That(result.Data.White, Is.EqualTo(2));
        }
       [Test]


public void GetScore_EmptyBoard_FailOrReturnZero()
//retun 0/gagal jika board belum ada
{
    // Arrange
    var mockLogger = new Mock<ILogger<GameController>>();
    var game = new GameController(mockLogger.Object);

    // Buat board kosong 4x4 tanpa pion
    var board = new Board(4);
    for (int r = 0; r < 4; r++)
        for (int c = 0; c < 4; c++)
            board.Cells[r, c] = new Cell(new Position(r, c));

    // Start game dengan board kosong
    var players = new List<IPlayer>
    {
        new Player("Alice", PlayerColor.Black),
        new Player("Bob", PlayerColor.White)
    };
    game.StartNewGame(players, board);

    for (int r = 0; r < board.Size; r++)
    for (int c = 0; c < board.Size; c++)
        board.Cells[r, c].Piece = null;

    // Act
    var result = game.GetScore();
    // Assert
    Assert.That(result.Success, Is.True, "Game berjalan tapi board kosong → sukses");
    Assert.That(result.Data.Black, Is.EqualTo(0), "Tidak ada pion Black");
    Assert.That(result.Data.White, Is.EqualTo(0), "Tidak ada pion White");
    }

    [Test]
public void GetWinner_BlackHasMorePoints_ReturnsBlack()
//return black winner
{
  
    // var board = new Board(4);
    // for (int r = 0; r < 4; r++)
    //     for (int c = 0; c < 4; c++)
    //         board.Cells[r, c] = new Cell(new Position(r, c)); // semua null

    // var players = new List<IPlayer>
    // {
    //     new Player("Alice", PlayerColor.Black),
    //     new Player("Bob", PlayerColor.White)
    // };

    // _game.StartNewGame(players, board);

    // Set pion sesuai skenario Black menang
    _game.GetBoard().Cells[0,0].Piece = new Piece(PieceColor.Black);
    _game.GetBoard().Cells[0,1].Piece = new Piece(PieceColor.Black);
    _game.GetBoard().Cells[0,2].Piece = new Piece(PieceColor.White);

    
    var winner = _game.GetWinner();

 
    Assert.That(winner, Is.Not.Null);
    Assert.That(winner!.Color, Is.EqualTo(PlayerColor.Black));
    Assert.That(winner.Name, Is.EqualTo("Alice"));
}

 [Test]
public void GetWinner_EmptyBoard_ReturnNull()
{
    // Arrange
    // var board = new Board(4);
    // for (int r = 0; r < 4; r++)
    //     for (int c = 0; c < 4; c++)
    //         board.Cells[r, c] = new Cell(new Position(r, c)); 

    // var players = new List<IPlayer>
    // {
    //     new Player("Alice", PlayerColor.Black),
    //     new Player("Bob", PlayerColor.White)
    // };

    // _game.StartNewGame(players, board);

    var winner = _game.GetWinner();

    Assert.That(winner, Is.Null, "Seharusnya null jika board kosong / tidak ada pion");
}


    [Test]
    public void GetWinner_Draw_ReturnsNull()
    {
        var _board = new Board(4);
            for (int r = 0; r < 4; r++)
                for (int c = 0; c < 4; c++)
                    _board.Cells[r, c] = new Cell(new Position(r, c));
        _board.Cells[0,0].Piece = new Piece(PieceColor.Black);
        _board.Cells[0,1].Piece = new Piece(PieceColor.White);

        var winner = _game.GetWinner();

        Assert.That(winner, Is.Null, "Seharusnya null jika seri (draw)");
    }

    }
}
