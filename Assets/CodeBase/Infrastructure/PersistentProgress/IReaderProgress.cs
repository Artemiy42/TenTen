using CodeBase.Main;

namespace CodeBase.Infrastructure.PersistentProgress
{
    public interface IReaderProgress
    {
        void LoadProgress(PlayerProgress progress);
    }
}