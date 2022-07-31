namespace CodeBase.Board.Cells
{
    public interface ICell
    {
        bool IsEmpty { get; }
        void Clear();
    }
}