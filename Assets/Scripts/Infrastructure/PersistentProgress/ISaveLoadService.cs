namespace TenTen
{
    public interface ISaveLoadService
    {
        void Save(PlayerProgress playerProgress);
        PlayerProgress Load();
    }
}