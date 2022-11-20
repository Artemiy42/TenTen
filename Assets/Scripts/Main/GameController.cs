using UnityEngine;

namespace TenTen
{
    public class GameController : MonoBehaviour, ISaverProgress
    {
        [SerializeField] private MainMenuPanel _mainMenuPanel;
        [SerializeField] private PauseMenu _pauseMenu;
        [SerializeField] private GameOverPanel _gameOverPanel;
        [SerializeField] private HUD _hud;
        [SerializeField] private BoardController _boardController;
        [SerializeField] private TetrominoFactory _tetrominoFactory;

        private PlayerProgress _playerProgress;
        
        public void Init()
        {
            _tetrominoFactory.Init();
            _boardController.Init(_tetrominoFactory);
        }

        public void StartGame()
        {
            _mainMenuPanel.SetBestScore(_playerProgress.BestScore);
            _mainMenuPanel.Show();
        }
        
        public void LoadProgress(PlayerProgress progress)
        {
            _playerProgress = progress;
            _boardController.LoadProgress(progress);
        }

        public void SaveProgress(PlayerProgress progress)
        {
            _boardController.SaveProgress(progress);
        }

        private void OnEnable()
        {
            _mainMenuPanel.OnPlayButtonClicked += PlayButtonClickedHandler;
            _mainMenuPanel.OnExitButtonClicked += ExitButtonClickedHandler;
            _hud.OnPauseButtonClicked += PauseButtonClickedHandler;
            _pauseMenu.OnResumeButtonClicked += ResumeButtonClickedHandler;
            _pauseMenu.OnHomeButtonClicked += HomeButtonClickedHandler;
            _pauseMenu.OnRestartButtonClicked += RestartButtonClickedHandler;
            _boardController.OnTetrominoAdded += TetrominoAddedHandler;
            _boardController.OnGameOver += GameOverHandler;     
            _boardController.OnClearLines += ClearLinesHandler;
            _gameOverPanel.OnHomeButtonClicked += GameOverHomeButtonClickedHandler;
            _gameOverPanel.OnRestartButtonClicked += RestartButtonClickedHandler;
        }

        private void OnDisable()
        {
            _mainMenuPanel.OnPlayButtonClicked -= PlayButtonClickedHandler;
            _mainMenuPanel.OnExitButtonClicked -= ExitButtonClickedHandler;
            _hud.OnPauseButtonClicked -= PauseButtonClickedHandler;
            _pauseMenu.OnResumeButtonClicked -= ResumeButtonClickedHandler;
            _pauseMenu.OnHomeButtonClicked -= HomeButtonClickedHandler;
            _pauseMenu.OnRestartButtonClicked -= RestartButtonClickedHandler;
            _boardController.OnTetrominoAdded -= TetrominoAddedHandler;
            _boardController.OnGameOver -= GameOverHandler;
            _boardController.OnClearLines -= ClearLinesHandler;
            _gameOverPanel.OnHomeButtonClicked -= GameOverHomeButtonClickedHandler;
            _gameOverPanel.OnRestartButtonClicked -= RestartButtonClickedHandler;
        }

        private void ResetGame()
        {
            _boardController.ResetGame();
            _playerProgress.CurrentScore = 0;
        }
        
        private void PlayButtonClickedHandler()
        {
            _mainMenuPanel.Hide();
            _hud.SetCurrentScore(_playerProgress.CurrentScore);
            _hud.SetBestScore(_playerProgress.BestScore);
            _hud.Show();
            _boardController.StartGame();
        }

        private void ExitButtonClickedHandler()
        {
            Application.Quit();
        }

        private void TetrominoAddedHandler(int amountBlock)
        {
            _playerProgress.CurrentScore += amountBlock;
            _hud.SetCurrentScore(_playerProgress.CurrentScore);
            UpdateBestScore();
        }

        private void GameOverHandler()
        {
            _gameOverPanel.Show();
        }

        private void ClearLinesHandler(int amountClearedLines)
        {
            int an = 10 + (amountClearedLines - 1) * 10;
            int sum = (10 + an) / 2 * amountClearedLines;
        
            _playerProgress.CurrentScore += sum;
            _hud.SetCurrentScore(_playerProgress.CurrentScore);
            UpdateBestScore();
        }

        private void UpdateBestScore()
        {
            if (_playerProgress.CurrentScore > _playerProgress.BestScore)
            {
                _playerProgress.BestScore = _playerProgress.CurrentScore;
                _hud.SetBestScore(_playerProgress.BestScore);
            }
        }

        private void PauseButtonClickedHandler()
        {
            _pauseMenu.Show();
        }

        private void ResumeButtonClickedHandler()
        {
            _pauseMenu.Hide();
        }

        private void HomeButtonClickedHandler()
        {
            _pauseMenu.Hide();
            _hud.Hide();
            _mainMenuPanel.SetBestScore(_playerProgress.BestScore);
            _mainMenuPanel.Show();
        }
        
        private void GameOverHomeButtonClickedHandler()
        {
            _hud.Hide();
            _gameOverPanel.Hide();

            ResetGame();
            
            _mainMenuPanel.Show();
        }

        private void RestartButtonClickedHandler()
        {
            _pauseMenu.Hide();
            _gameOverPanel.Hide();

            ResetGame();
            _boardController.StartGame();

            _hud.SetCurrentScore(_playerProgress.CurrentScore);
            _hud.SetBestScore(_playerProgress.BestScore);
        }
    }
}