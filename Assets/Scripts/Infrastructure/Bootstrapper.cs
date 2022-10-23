using UnityEngine;
using UnityEngine.SceneManagement;

namespace TenTen
{
    public class Bootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private const string GameScene = "Game";
        
        private GameController _gameController;
        private ISaveLoadService _saveLoadService;
        private PlayerProgress _playerProgress;

        public void Awake()
        {
            _saveLoadService = new SaveLoadService();
            
            var sceneLoader = new SceneLoader(this);    
            sceneLoader.Load(GameScene, onLoaded: StartGame);
            DontDestroyOnLoad(this);
        }

        private void StartGame(Scene loadedScene)
        {
            _playerProgress = _saveLoadService.Load();

            _gameController = loadedScene.FindComponentOfType<GameController>();
            _gameController.Init();
            _gameController.LoadProgress(_playerProgress);
            _gameController.StartGame();
        }
        
        private void OnApplicationQuit()
        {
            _gameController.SaveProgress(_playerProgress);
            _saveLoadService.Save(_playerProgress);
        }
    }
}