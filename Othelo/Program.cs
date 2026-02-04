using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;



// #region CONSOLE UI / ENTRY POINT

public class Program
{
    static void Main()
    {
                Console.Clear();
        Console.WriteLine(@"
        ██████╗ ████████╗██╗  ██╗███████╗██╗     ██╗      ██████╗ 
        ██╔═══██╗╚══██╔══╝██║  ██║██╔════╝██║     ██║     ██╔═══██╗
        ██║   ██║   ██║   ███████║█████╗  ██║     ██║     ██║   ██║
        ██║   ██║   ██║   ██╔══██║██╔══╝  ██║     ██║     ██║   ██║
        ╚██████╔╝   ██║   ██║  ██║███████╗███████╗███████╗╚██████╔╝
        ╚═════╝    ╚═╝   ╚═╝  ╚═╝╚══════╝╚══════╝╚══════╝ ╚═════╝ 
        ");
        Thread.Sleep(500);
        Console.WriteLine("                  =====================================");
        Console.WriteLine("                           W E L C O M E   T O");
        Console.WriteLine("                              O T H E L L O");
        Console.WriteLine("                  =====================================");
        
        Thread.Sleep(1000);
        Console.WriteLine("Rules:");
        Console.WriteLine("- Game dimainkan oleh 2 pemain");
        Console.WriteLine("- Black selalu jalan lebih dulu");
        Console.WriteLine("- Batu lawan akan dibalik jika terjepit");
        Console.WriteLine();
       while (!Console.KeyAvailable)
            {
                Console.Write("\rPress ENTER to start...   ");
                Thread.Sleep(500);

                Console.Write("\r                        ");
                Thread.Sleep(500);
            }

            Console.ReadKey(true); // ambil ENTER
            Console.Clear();;
        ;

        

        var board = new Board(8); // papan 8x8
        Console.Write("Input Name Player 1 : ");
        string Player1 = Console.ReadLine();

        Console.Write("Input Name Player 2 : ");
        string Player2 = Console.ReadLine();


        var players = new List<IPlayer>
        {
            new Player(Player1??"Player 1", PlayerColor.Black),
            new Player(Player2??"Player 2", PlayerColor.White)
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


