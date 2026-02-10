using OthelloAPI.Services;
using OthelloAPI.Models;
using OthelloAPI.Common;
namespace OthelloAPI;

public class Tests
{
    private GameController _game;
    [SetUp]
    public void Setup()
    {
      // Buat players
    var players = new List<IPlayer>
    {
        new Player("Alice", PlayerColor.Black),
        new Player("Bob", PlayerColor.White)
    };

    // Buat board kosong
    var board = new Board(8);
    for (int r = 0; r < 8; r++)
        for (int c = 0; c < 8; c++)
            board.Cells[r, c] = new Cell(new Position(r, c));

    // Set pieces awal Othello
    int mid = board.Size / 2;
    board.Cells[mid - 1, mid - 1].Piece = new Piece(PieceColor.White);
    board.Cells[mid, mid].Piece = new Piece(PieceColor.White);
    board.Cells[mid - 1, mid].Piece = new Piece(PieceColor.Black);
    board.Cells[mid, mid - 1].Piece = new Piece(PieceColor.Black);

    // Buat GameController
    _game = new GameController();
    _game.StartNewGame(players, board);
    }

    [Test]
    public void PlayAt_InputValid_ReturnTrue()
    {
        var pos = new Position(3,2);
        ServiceResult<bool> result = _game.PlayAt(pos);

        Assert.That(result.Success, Is.True, "Move Data Benar");
    }

    [TestCase(1,1)]
    [TestCase(1,2)]
    [TestCase(0,1)]
    public void PlayAt_InputValid_ReturnFalse(int row, int col)
    {   
        
        var pos = new Position(row,col);
        //  _game.GetBoard().Cells[pos.Row, pos.Col].Piece = new Piece(PieceColor.Black); // cell sudah terisi
        ServiceResult<bool> invalidResul = _game.PlayAt(pos);

        Assert.That(invalidResul.Success, Is.False, "Tidak ada kolom");
    }


    [Test]
    public void IsValidMove_InputValid_ReturnTrue()
    {
        var pos = new Position(3,2);
        var result = _game.IsValidMove(pos, PlayerColor.Black);

         Assert.That(result, Is.True);

    }
    [Test]
    public void IsValidMove_InputInvalid_ReturnFalse()
    {
        var pos = new Position(3,3);

        var result = _game.IsValidMove(pos, PlayerColor.Black);
         _game.GetBoard().Cells[3,3].Piece = new Piece(PieceColor.Black);

         Assert.That(result, Is.False);
    }
    [Test]
    public void GetScore_EmptyBoard_ShouldFail()
    {
         var players = new List<IPlayer>
    {
        new Player("A", PlayerColor.Black),
        new Player("B", PlayerColor.White)
    };

    var board = new Board(0); // board 0
    var game = new GameController();
    game.StartNewGame(players, board);

    var result = game.GetScore();

    Assert.That(result.Success, Is.True);
    Assert.That(result.Data.Black, Is.EqualTo(0));
    Assert.That(result.Data.White, Is.EqualTo(0));
    }

   [Test]
    public void GetScore_Input2_ReturnScore()
    {

        var result = _game.GetScore();

        Assert.That(result.Success, Is.True);
        Assert.That(result.Data.Black, Is.EqualTo(2));
        Assert.That(result.Data.White, Is.EqualTo(2));
    }
  
}