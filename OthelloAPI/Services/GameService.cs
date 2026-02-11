using OthelloAPI.Models;
using OthelloAPI.Common;
using OthelloAPI.DTOs.Response;
using Microsoft.Extensions.Logging;


namespace OthelloAPI.Services
{
    public class GameController
    {
        private IBoard _board;
        private List<IPlayer> _players;

        private int _currentPlayerIndex = 0;
        private int _counterPasses = 0;

        public bool IsGameOver { get; private set; } = false;
        public IPlayer CurrentPlayer => _players[_currentPlayerIndex];

        // Events untuk UI / API
        public event Action<IPlayer>? TurnChanged;
        public event Action<IBoard>? BoardUpdated;
        public event Action<IPlayer?>? GameEnded;
        private readonly ILogger<GameController> _logger;

        private readonly Position[] directions = new Position[]
        {
            new Position(-1,0), new Position(-1,1), new Position(0,1), new Position(1,1),
            new Position(1,0), new Position(1,-1), new Position(0,-1), new Position(-1,-1)
        };

        // Constructor
        public GameController(ILogger<GameController> logger)
        {
            _players = new List<IPlayer>();
            _board = new Board(8);
            _logger = logger;


        }


        // ------------------ BOARD INITIALIZATION ------------------

        public void StartNewGame(List<IPlayer> players, IBoard board)
            {
                _players = players;
                _board = board;
                _currentPlayerIndex = 0;
                _counterPasses = 0;
                IsGameOver = false;

                InitializeBoardCells();
                InitializeBoard();

                RaiseBoardUpdated();
                RaiseTurnChanged();
            }

        private void InitializeBoardCells()
        {
            for (int r = 0; r < _board.Size; r++)
                for (int c = 0; c < _board.Size; c++)
                    _board.Cells[r, c] = new Cell(new Position(r, c));
        }

        private void InitializeBoard()
        {
            int mid = _board.Size / 2;
            _board.Cells[mid - 1, mid - 1].Piece = new Piece(PieceColor.White);
            _board.Cells[mid, mid].Piece = new Piece(PieceColor.White);
            _board.Cells[mid - 1, mid].Piece = new Piece(PieceColor.Black);
            _board.Cells[mid, mid - 1].Piece = new Piece(PieceColor.Black);

            RaiseBoardUpdated();
        }

        public IBoard GetBoard() => _board;

        private PieceColor ToPieceColor(PlayerColor playerColor)
            => playerColor == PlayerColor.Black ? PieceColor.Black : PieceColor.White;

        // ------------------ GAMEPLAY ------------------
    public ServiceResult<bool> PlayAt(Position pos)
    {
        _logger.LogInformation("Player {Player} attempting to play at {@Position}", CurrentPlayer?.Name, pos);

        if (IsGameOver)
        {
            _logger.LogWarning("Move rejected: Game is already over. Player {Player} tried to play at {@Position}", CurrentPlayer?.Name, pos);
            return ServiceResult<bool>.Fail("Game is already over.");
        }

        if (!IsValidMove(pos, CurrentPlayer.Color))
        {
            _logger.LogWarning("Invalid move by {Player} at {@Position}", CurrentPlayer?.Name, pos);
            return ServiceResult<bool>.Fail("Invalid move at this position.");
        }

        var toFlip = GetFlippablePositions(pos, CurrentPlayer.Color);
        _logger.LogDebug("Flipping {Count} opponent pieces for move at {@Position} by {Player}", toFlip.Count, pos, CurrentPlayer?.Name);

        _board.Cells[pos.Row, pos.Col].Piece = new Piece(ToPieceColor(CurrentPlayer.Color));
        FlipPieces(toFlip);

        _counterPasses = 0;

        RaiseBoardUpdated();
        _logger.LogInformation("Board updated after {Player}'s move at {@Position}", CurrentPlayer?.Name, pos);

        SwitchTurn();
        _logger.LogInformation("Turn changed to {Player}", CurrentPlayer?.Name);

        if (!HasAnyValidMove(CurrentPlayer.Color))
        {
            _counterPasses++;
            _logger.LogInformation("Player {Player} has no valid moves, skipping turn", CurrentPlayer?.Name);
            SwitchTurn();
            _logger.LogInformation("Turn changed to {Player} after skip", CurrentPlayer?.Name);
        }

        if (CheckGameOver())
        {
            IsGameOver = true;
            var winner = GetWinner();
            _logger.LogInformation("Game Over! Winner: {Winner}", winner?.Name ?? "Draw");
            RaiseGameEnded(winner);
        }

        _logger.LogInformation("Move at {@Position} by {Player} completed successfully", pos, CurrentPlayer?.Name);

        return ServiceResult<bool>.Ok(true);
    }
   
    public void PassTurn()
     {
        _counterPasses++;
        _logger.LogInformation("Player {Player} passed. CounterPasses: {Count}", CurrentPlayer?.Name, _counterPasses);

        SwitchTurn();
        _logger.LogInformation("Turn changed to {Player}", CurrentPlayer?.Name);

        //  Cek apakah pemain baru punya valid move
        if (!HasAnyValidMove(CurrentPlayer.Color))
            {
                // Jika tidak ada valid move → increment counter lagi
                _counterPasses++;
                _logger.LogInformation("No valid moves for {Player}, counter incremented: {Count}", CurrentPlayer?.Name, _counterPasses);

                // Ganti giliran lagi
                SwitchTurn();
                _logger.LogInformation("Turn changed again to {Player}", CurrentPlayer?.Name);
            }

        // 4️⃣ Cek apakah game over (misal counter pass 2 atau board penuh)
        if (CheckGameOver())
        {
            IsGameOver = true;
            RaiseGameEnded(GetWinner());
            _logger.LogInformation("Game Over! Winner: {Winner}", GetWinner()?.Name ?? "Draw");
        }

            _logger.LogInformation("End of PassTurn. CurrentPlayer: {Player}, CounterPasses: {Count}, IsGameOver: {IsOver}",
            CurrentPlayer?.Name, _counterPasses, IsGameOver);
     }

        private void FlipPieces(List<Position> positions)
        {
            foreach (var p in positions)
            {
                var cell = _board.Cells[p.Row, p.Col];
                if (cell.Piece != null)
                    cell.Piece.Color = cell.Piece.Color == PieceColor.Black
                        ? PieceColor.White
                        : PieceColor.Black;
            }
        }

        public bool IsValidMove(Position pos, PlayerColor color)
        {
            if (_board.Cells[pos.Row, pos.Col].Piece != null) return false;
            return GetFlippablePositions(pos, color).Count > 0;
        }

        private List<Position> GetFlippablePositions(Position pos, PlayerColor color)
        {
            var flippable = new List<Position>();
            var myColor = ToPieceColor(color);

            foreach (var dir in directions)
            {
                var temp = new List<Position>();
                int r = pos.Row + dir.Row;
                int c = pos.Col + dir.Col;

                while (r >= 0 && r < _board.Size && c >= 0 && c < _board.Size)
                {
                    var piece = _board.Cells[r, c].Piece;
                    if (piece == null || piece.Color == PieceColor.Empty)
                    {
                        temp.Clear();
                        break;
                    }
                    else if (piece.Color == myColor)
                    {
                        if (temp.Count > 0)
                            flippable.AddRange(temp);
                        break;
                    }
                    else
                    {
                        temp.Add(new Position(r, c));
                    }

                    r += dir.Row;
                    c += dir.Col;
                }
            }

            return flippable;
        }

        private void SwitchTurn()
        {
            _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;
            RaiseTurnChanged();
            RaiseBoardUpdated();
        }

        private bool HasAnyValidMove(PlayerColor color)
        {
            for (int r = 0; r < _board.Size; r++)
            for (int c = 0; c < _board.Size; c++)
                if (IsValidMove(new Position(r, c), color))
                    return true;

            return false;
        }

        private bool CheckGameOver()
        {
            // Board penuh
            if (_board.Cells.Cast<Cell>().All(c => c.Piece != null))
            {
                 _logger.LogInformation("Board sudah penuh");
                 return true;

            }
                
            // Semua pemain tidak punya move
            if (!HasAnyValidMove(PlayerColor.Black) && !HasAnyValidMove(PlayerColor.White))
            {
                _logger.LogInformation("Setiap player tidak memiliki langkah lagi");
                return true;   
            }
             

            // Semua pemain pass berturut-turut
            if (_counterPasses >= _players.Count)
                {
                _logger.LogInformation("Counter pass udah berjumlah {_counterPasses}",_counterPasses);
                return true;   
            }
             

            return false;
        }

        public ServiceResult<ScoreDto>  GetScore()
        {
            if (_board == null || _board.Cells == null)
            {
                  _logger.LogWarning("GetScore dipanggil tapi board belum diinisialisasi");
                 return ServiceResult<ScoreDto>.Fail("Board belum diinisialisasi");
            }
               

            var score = new ScoreDto();

            foreach (var cell in _board.Cells)
            {
                if (cell.Piece == null) continue;

                if (cell.Piece.Color == PieceColor.Black) score.Black++;
                else if (cell.Piece.Color == PieceColor.White) score.White++;
            }
               _logger.LogInformation("Score dihitung: Black {Black} - White {White}", score.Black, score.White);

            return ServiceResult<ScoreDto>.Ok(score);
        }
    public IPlayer? GetWinner()
        {
            var result = GetScore();

            if (!result.Success)
            {
                // Log kalau gagal ambil score
                _logger.LogWarning("GetWinner dipanggil tapi gagal mendapatkan score. Reason: {Message}", result.Message);
                return null;
            }

            var score = result.Data; // ScoreDto

            if (score.Black > score.White)
            {
                _logger.LogInformation("Pemenang: Black dengan skor {Black} vs {White}", score.Black, score.White);
                return _players.First(p => p.Color == PlayerColor.Black);
            }

            if (score.White > score.Black)
            {
                _logger.LogInformation("Pemenang: White dengan skor {White} vs {Black}", score.White, score.Black);
                return _players.First(p => p.Color == PlayerColor.White);
            }

            // Seri
            _logger.LogInformation("Hasil seri: Black {Black} - White {White}", score.Black, score.White);
            return null;
        }


        // ------------------ EVENT RAISERS ------------------
        private void RaiseTurnChanged() => TurnChanged?.Invoke(CurrentPlayer);
        private void RaiseBoardUpdated() => BoardUpdated?.Invoke(_board);
        private void RaiseGameEnded(IPlayer? winner) => GameEnded?.Invoke(winner);
    }
}
