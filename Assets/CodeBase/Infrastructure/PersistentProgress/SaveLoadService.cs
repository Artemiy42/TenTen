using CodeBase.Main;
using Newtonsoft.Json;
using UnityEngine;

namespace CodeBase.Infrastructure.PersistentProgress
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string PlayerData = "PlayerData";

        public void Save(PlayerProgress playerProgress)
        {
            string playerJson = JsonConvert.SerializeObject(playerProgress);
            Debug.Log("Save: " + playerJson);
            PlayerPrefs.SetString(PlayerData, playerJson);
            PlayerPrefs.Save();           
        }

        public PlayerProgress Load()
        {
            string playerJson = PlayerPrefs.GetString(PlayerData);
            Debug.Log("Load: " + playerJson);
            return JsonConvert.DeserializeObject<PlayerProgress>(playerJson) ?? new PlayerProgress();
        }
    }
}