public class Board : IBoard
{
    public int Size { get; }
    public ICell[,] Cells { get; }

    public Board(int size)
    {
        Size = size;
        Cells = new ICell[size, size];

        for (int r = 0; r < size; r++)
        for (int c = 0; c < size; c++)
            Cells[r, c] = new Cell(new Position(r, c));
    }
}
