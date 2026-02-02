public class Cell : ICell
{
    public Position Position { get; }
    public IPiece? Piece { get; set; }

    public Cell(Position position)
    {
        Position = position;
    }
}
