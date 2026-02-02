using System;

namespace DisposalPatternDemo
{
    /// <summary>
    /// Simulasi database connection untuk demonstrasi Close() vs Dispose()
    /// </summary>
    public class DatabaseConnection : IDisposable
    {
        private string? _connectionString;
        private bool _isOpen = false;
        private bool _disposed = false;

        // Properti untuk cek state
        public string State => _disposed ? "Disposed" : (_isOpen ? "Open" : "Closed");

        public DatabaseConnection(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            Console.WriteLine($"🔗 DatabaseConnection: Created with connection string");
        }

        public void Open()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(DatabaseConnection), 
                    "Cannot open a disposed connection. Once disposed, it's gone forever!");
            }

            if (_isOpen)
            {
                Console.WriteLine("ℹ Connection is already open");
                return;
            }

            _isOpen = true;
            Console.WriteLine("✅ Database connection opened");
        }

        public void Close()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(DatabaseConnection), 
                    "Cannot close a disposed connection.");
            }

            if (!_isOpen)
            {
                Console.WriteLine("ℹ Connection is already closed");
                return;
            }

            _isOpen = false;
            Console.WriteLine("⏸ Database connection closed (but can be reopened)");
        }

        public void Dispose()
        {
            if (_disposed)
            {
                Console.WriteLine("🔄 Dispose() called again - safely ignored");
                return;
            }

            if (_isOpen)
            {
                _isOpen = false;
                Console.WriteLine("⏸ Closing open connection during disposal");
            }

            _connectionString = null;
            _disposed = true;

            Console.WriteLine("🧹 DatabaseConnection: Permanently disposed - cannot be reopened");

            GC.SuppressFinalize(this);
        }
    }
}
