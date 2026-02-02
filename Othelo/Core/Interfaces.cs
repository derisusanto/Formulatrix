public interface IPiece
{
    PieceColor Color { get; }
}

public interface ICell
{
    Position Position { get; }
    IPiece? Piece { get; set; }
}

public interface IBoard
{
    int Size { get; }
    ICell[,] Cells { get; }
}

public interface IPlayer
{
    string Name { get; }
    PlayerColor Color { get; }
}
