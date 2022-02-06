using System.Collections.Generic;
using UI;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private GridView _gridView;
        [SerializeField] private Spawner _spawner;

        private void Start()
        {
            _mainMenu.PlayButtonClicked += OnPlayButtonClicked;
            _mainMenu.ExitButtonClicked += OnExitButtonClicked;
        }

        private void StartGame()
        {
            _gridView.Init(new Grid());
            _gridView.CreateGrid();
            _gridView.gameObject.SetActive(true);
            _spawner.CreateTetrominoes();
            SubscribeLiveTetrominoes(_spawner.LiveTetrominoes);

        }

        private void SubscribeLiveTetrominoes(IEnumerable<Tetromino> liveTetrominoes)
        {
            foreach (Tetromino tetromino in liveTetrominoes)
            {
                tetromino.BlockEndMove += OnBlockEndMove;
            }
        }

        private void OnBlockEndMove(Tetromino tetromino)
        {
            if (_gridView.CanAddToGrid(tetromino))
            {
                tetromino.FinishMove();
                _gridView.AddTetrominoToGrid(tetromino);
                tetromino.BlockEndMove -= OnBlockEndMove;
            }
            else
            {
                tetromino.Reset();
            }
        }

        private void ExitGame()
        {
            Application.Quit();
        }
        
        private void OnPlayButtonClicked()
        {
            _mainMenu.Hide();
            StartGame();
        }

        private void OnExitButtonClicked()
        {
            ExitGame();
        }
    }
}