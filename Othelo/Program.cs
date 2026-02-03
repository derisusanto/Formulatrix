using System;
using System.Collections.Generic;
using System.Linq;

#region ENUM & VALUE OBJECT

// public enum PieceColor
// {
//     Empty,
//     Black,
//     White
// }

// public enum PlayerColor
// {
//     Black,
//     White
// }

// public enum Direction
// {
//     North, South, East, West,
//     NorthEast, NorthWest, SouthEast, SouthWest
// }

// public struct Position
// {
//     public int Row { get; }
//     public int Col { get; }

//     public Position(int row, int col)
//     {
//         Row = row;
//         Col = col;
//     }
// }

// #endregion

// #region INTERFACES

// public interface IPiece
// {
//     PieceColor Color { get; }
// }

// public interface ICell
// {
//     Position Position { get; }
//     IPiece? Piece { get; set; }
// }

// public interface IBoard
// {
//     int Size { get; }
//     ICell[,] Cells { get; }
// }

// public interface IPlayer
// {
//     string Name { get; }
//     PlayerColor Color { get; }
// }

// #endregion

// #region IMPLEMENTATION

// public class Piece : IPiece
// {
//     public PieceColor Color { get; }

//     public Piece(PieceColor color)
//     {
//         Color = color;
//     }
// }

// public class Cell : ICell
// {
//     public Position Position { get; }
//     public IPiece? Piece { get; set; }

//     public Cell(Position position)
//     {
//         Position = position;
//         Piece = null;
//     }
// }

// public class Board : IBoard
// {
//     public int Size { get; }
//     public ICell[,] Cells { get; }

//     public Board(int size)
//     {
//         Size = size;
//         Cells = new ICell[size, size];

//         for (int r = 0; r < size; r++)
//         for (int c = 0; c < size; c++)
//             Cells[r, c] = new Cell(new Position(r, c));
//     }
// }

// public class Player : IPlayer
// {
//     public string Name { get; }
//     public PlayerColor Color { get; }

//     public Player(string name, PlayerColor color)
//     {
//         Name = name;
//         Color = color;
//     }
// }

#endregion

// #region GAME CONTROLLER

// public class GameController
// {
//     private readonly IBoard _board;
//     private readonly List<IPlayer> _players;
//     private int _currentPlayerIndex;
//     private bool _isGameOver;
//     private int _counterPasses;

//     public event Action<IPlayer>? TurnChanged;
//     public event Action<IBoard>? BoardUpdated;
//     public event Action<IPlayer?>? GameEnded;

//     public GameController(List<IPlayer> players, IBoard board)
//     {
//         _players = players;
//         _board = board;
//         _currentPlayerIndex = 0;
//         _isGameOver = false;
//         _counterPasses = 0;
//     }

//     public IPlayer CurrentPlayer => _players[_currentPlayerIndex];
//     public bool IsGameOver => _isGameOver;

//     #region PUBLIC API

//     public void StartGame()
//     {
//         InitializeBoard();
//         RaiseBoardUpdated();
//         RaiseTurnChanged();
//     }

//   public bool PlayAt(Position position)
// {
//     if (_isGameOver)
//         return false;

//     var player = CurrentPlayer;

//     if (!IsValidMove(position, player.Color))
//         return false;

//     // 1️⃣ Hitung piece yang bisa dibalik
//     var flippable = GetFlippablePositions(position, player.Color);

//     // 2️⃣ Lakukan move
//     MakeMove(position); // ✅ di dalam ini sudah PlacePiece + reset _counterPasses + SwitchTurn

//     // 3️⃣ Flip piece
//     FlipPieces(flippable);

//     // 4️⃣ Update board UI / event
//     RaiseBoardUpdated();

//     // 5️⃣ Jika giliran pemain baru tidak bisa move → skip turn
//     if (!HasAnyValidMove(CurrentPlayer.Color))
//     {
//         SwitchTurn();
//     }

//     // 6️⃣ Cek game over
//     if (CheckGameOver())
//     {
//         _isGameOver = true;
//         RaiseGameEnded(GetWinner());
//     }

//     return true;
// }

//     public void PassTurn()
//     {
//     // menhitung pass berturut-turut
//     _counterPasses++;

//     SwitchTurn();

//     // 3️⃣ Cek game over: 
//     // - board penuh atau
//     // - semua pemain pass berturut-turut
//     if (CheckGameOver())
//     {
//         _isGameOver = true;
//         RaiseGameEnded(GetWinner());
//     }
//     }

//     public int GetScore(IPlayer player)
//     {
//         return CountPieces(player.Color);
//     }

//     public IPlayer? GetWinner()
//     {
//         int black = CountPieces(PlayerColor.Black);
//         int white = CountPieces(PlayerColor.White);

//         if (black > white)
//             return _players.First(p => p.Color == PlayerColor.Black);
//         if (white > black)
//             return _players.First(p => p.Color == PlayerColor.White);
//         return null; // draw
//     }

//     #endregion

//     #region CORE LOGIC

//     private void InitializeBoard()
//     {
//         int mid = _board.Size / 2;

//         _board.Cells[mid - 1, mid - 1].Piece = new Piece(PieceColor.White);
//         _board.Cells[mid, mid].Piece = new Piece(PieceColor.White);
//         _board.Cells[mid - 1, mid].Piece = new Piece(PieceColor.Black);
//         _board.Cells[mid, mid - 1].Piece = new Piece(PieceColor.Black);
//     }

//     public bool IsValidMove(Position pos, PlayerColor color)
//     {
//         if (!IsInsideBoard(pos.Row, pos.Col))
//             return false;

//         if (_board.Cells[pos.Row, pos.Col].Piece != null)
//             return false;

//         return GetFlippablePositions(pos, color).Any();
//     }

//   public void MakeMove(Position pos)
// {
//     if (!IsValidMove(pos, CurrentPlayer.Color))
//         return;

//     PlacePiece(pos, CurrentPlayer.Color);

//     _counterPasses = 0; // reset pass karena ada move valid

//     SwitchTurn();
// }

//     private List<Position> GetFlippablePositions(Position pos, PlayerColor color)
//     {
//         var result = new List<Position>();

//         foreach (Direction dir in Enum.GetValues(typeof(Direction)))
//         {
//             var temp = new List<Position>();
//             var (dr, dc) = DirectionToDelta(dir);

//             int r = pos.Row + dr;
//             int c = pos.Col + dc;

//             while (IsInsideBoard(r, c))
//             {
//                 var piece = _board.Cells[r, c].Piece;
//                 if (piece == null) break;

//                 if (piece.Color != ToPieceColor(color))
//                 {
//                     temp.Add(new Position(r, c));
//                 }
//                 else
//                 {
//                     if (temp.Count > 0)
//                         result.AddRange(temp);
//                     break;
//                 }

//                 r += dr;
//                 c += dc;
//             }
//         }

//         return result;
//     }

//     private void PlacePiece(Position pos, PlayerColor color)
//     {
//         _board.Cells[pos.Row, pos.Col].Piece = new Piece(ToPieceColor(color));
//     }

//     private void FlipPieces(List<Position> positions)
//     {
//         foreach (var pos in positions)
//         {
//             var piece = _board.Cells[pos.Row, pos.Col].Piece;
//             if (piece == null) continue;

//             _board.Cells[pos.Row, pos.Col].Piece =
//                 new Piece(piece.Color == PieceColor.Black ? PieceColor.White : PieceColor.Black);
//         }
//     }

//     private int CountPieces(PlayerColor color)
//     {
//         PieceColor target = ToPieceColor(color);
//         int count = 0;

//         foreach (var cell in _board.Cells)
//         {
//             if (cell.Piece?.Color == target)
//                 count++;
//         }

//         return count;
//     }

//    private void SwitchTurn()
// {
//     _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;
//     RaiseTurnChanged();
//     RaiseBoardUpdated(); // <-- tambahan ini
// }


//     private bool HasAnyValidMove(PlayerColor color)
//     {
//         for (int r = 0; r < _board.Size; r++)
//         for (int c = 0; c < _board.Size; c++)
//         {
//             if (IsValidMove(new Position(r, c), color))
//                 return true;
//         }
//         return false;
//     }

//     private bool CheckGameOver()
//     {
//         return IsBoardFull() || (!HasAnyValidMove(PlayerColor.Black) && !HasAnyValidMove(PlayerColor.White)) || (_counterPasses >= _players.Count);
//     }

//     private bool IsBoardFull()
//     {
//         foreach (var cell in _board.Cells)
//             if (cell.Piece == null)
//                 return false;
//         return true;
//     }

//     #endregion

//     #region HELPERS & EVENTS

//     private bool IsInsideBoard(int r, int c) => r >= 0 && r < _board.Size && c >= 0 && c < _board.Size;

//     private PieceColor ToPieceColor(PlayerColor color) => color == PlayerColor.Black ? PieceColor.Black : PieceColor.White;

//     private (int, int) DirectionToDelta(Direction dir)
//     {
//         return dir switch
//         {
//             Direction.North => (-1, 0),
//             Direction.South => (1, 0),
//             Direction.East => (0, 1),
//             Direction.West => (0, -1),
//             Direction.NorthEast => (-1, 1),
//             Direction.NorthWest => (-1, -1),
//             Direction.SouthEast => (1, 1),
//             Direction.SouthWest => (1, -1),
//             _ => (0, 0)
//         };
//     }
    

//     private void RaiseTurnChanged() => TurnChanged?.Invoke(CurrentPlayer);
//     private void RaiseBoardUpdated() => BoardUpdated?.Invoke(_board);
//     private void RaiseGameEnded(IPlayer? winner) => GameEnded?.Invoke(winner);

//     #endregion
// }

// #endregion

#region CONSOLE UI / ENTRY POINT

public class Program
{
    static void Main()
    {
        var board = new Board(8); // papan 8x8

        var players = new List<IPlayer>
        {
            new Player("Player 1", PlayerColor.Black),
            new Player("Player 2", PlayerColor.White)
        };

        var game = new GameController(players, board);

        // game.TurnChanged += p =>
        // {
        //     Console.WriteLine($"\nTurn: {p.Name} ({p.Color})");
        // };

        game.BoardUpdated += b =>
        {
            PrintBoard(b, game);
        };

        game.GameEnded += winner =>
        {
            Console.WriteLine("\nGAME OVER");
            if (winner == null)
                Console.WriteLine("Draw!");
            else
            Console.WriteLine($"Winner: {winner.Name}");
            Console.WriteLine($"Black: {game.GetScore(players[0])}");
            Console.WriteLine($"White: {game.GetScore(players[1])}");
        };

        game.StartGame();

        while (!game.IsGameOver)
        {

            Console.WriteLine($"Turn :{game.CurrentPlayer.Name} ({game.CurrentPlayer.Color})");
            Console.WriteLine($"Piece: {game.GetScore(game.CurrentPlayer)}");
            Console.Write($"\nInput move (row col) or 'p' to pass: ");
            var input = Console.ReadLine();

            if (input == "p")
            {
                game.PassTurn();
                continue;
            }

            var parts = input?.Split(' ');
            Console.WriteLine(parts.Length);
            Console.WriteLine(parts);
            if (parts == null || parts.Length != 2) continue;

            if (!int.TryParse(parts[0], out int r) || !int.TryParse(parts[1], out int c)) continue;

            if (!game.PlayAt(new Position(r, c)))
            {
                Console.WriteLine("Invalid move!");
            }
        }

        Console.ReadKey();
    }

    // Print board dengan tanda '*' untuk valid moves
static void PrintBoard(IBoard board, GameController game)
{
    Console.Clear();
    int size = board.Size;

    // Header kolom
    Console.Write("   ");
    for (int c = 0; c < size; c++)
        Console.Write($" {c}  ");
    Console.WriteLine();

    // Garis atas
    Console.Write("  ┌");
    for (int c = 0; c < size; c++)
        Console.Write("───" + (c < size - 1 ? "┬" : "┐"));
    Console.WriteLine();

    for (int r = 0; r < size; r++)
    {
        Console.Write($"{r} │");
        for (int c = 0; c < size; c++)
        {
            var cell = board.Cells[r, c];

            if (cell.Piece == null && game.IsValidMove(new Position(r, c), game.CurrentPlayer.Color))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" * "); // valid move
            }
            else if (cell.Piece != null && cell.Piece.Color == PieceColor.Black)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" ● ");
            }
            else if (cell.Piece != null && cell.Piece.Color == PieceColor.White)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" ● ");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(" . "); // empty cell
            }

            Console.ResetColor();
            Console.Write("│");
        }
        Console.WriteLine();

        if (r < size - 1)
        {
            Console.Write("  ├");
            for (int c = 0; c < size; c++)
                Console.Write("───" + (c < size - 1 ? "┼" : "┤"));
            Console.WriteLine();
        }
    }

    // Garis bawah
    Console.Write("  └");
    for (int c = 0; c < size; c++)
        Console.Write("───" + (c < size - 1 ? "┴" : "┘"));
    Console.WriteLine();
}
}

#endregion
