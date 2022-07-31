using CodeBase.Main;

namespace CodeBase.Infrastructure.PersistentProgress
{
    public interface ISaverProgress : IReaderProgress
    {
        void SaveProgress(PlayerProgress progress);
    }
}