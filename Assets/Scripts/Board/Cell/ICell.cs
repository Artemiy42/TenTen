namespace TenTen.Board
{
    public interface ICell
    {
        bool IsEmpty { get; }
        void Clear();
    }
}