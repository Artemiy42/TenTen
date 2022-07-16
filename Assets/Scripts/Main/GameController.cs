using TenTen.Board;
using TenTen.UI;
using UnityEngine;

namespace TenTen
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private MainMenuPanel _mainMenuPanel;
        [SerializeField] private GameOverPanel _gameOverPanel;
        [SerializeField] private HUD _hud;
        [SerializeField] private BoardController _boardController;
        [SerializeField] private PauseMenu _pauseMenu;
        [SerializeField] private PlayerData _playerData;
        
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
        }

        private void PlayButtonClickedHandler()
        {
            _mainMenuPanel.Hide();
            _boardController.StartGame();
        }

        private void ExitButtonClickedHandler()
        {
            Application.Quit();
        }

        private void TetrominoAddedHandler(int amountBlock)
        {
            _playerData.CurrentScore += amountBlock;
            _hud.SetCurrentScore(_playerData.CurrentScore);
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
        
            _playerData.CurrentScore += sum;
            _hud.SetCurrentScore(_playerData.CurrentScore);
            UpdateBestScore();
        }

        private void UpdateBestScore()
        {
            if (_playerData.CurrentScore > _playerData.BestScore)
            {
                _hud.SetBestScore(_playerData.BestScore);
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
            _mainMenuPanel.Show();
        }

        private void RestartButtonClickedHandler()
        {
            throw new System.NotImplementedException();
        }
    }
}