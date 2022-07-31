using CodeBase.Main;

namespace CodeBase.Infrastructure.PersistentProgress
{
    public interface ISaveLoadService
    {
        void Save(PlayerProgress playerProgress);
        PlayerProgress Load();
    }
}