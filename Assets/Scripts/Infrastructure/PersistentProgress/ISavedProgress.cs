namespace TenTen
{
    public interface ISaverProgress : IReaderProgress
    {
        void SaveProgress(PlayerProgress progress);
    }
}