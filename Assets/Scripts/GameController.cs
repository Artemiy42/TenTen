using UI;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private GridController _gridController;

        private void Start()
        {
            _mainMenu.OnPlayButtonClicked += PlayButtonClickedHandler;
            _mainMenu.OnExitButtonClicked += ExitButtonClickedHandler;
        }
        
        private void PlayButtonClickedHandler()
        {
            _mainMenu.Hide();
            _gridController.StartGame();
        }

        private void ExitButtonClickedHandler()
        {
            ExitGame();
        }
        
        private void ExitGame()
        {
            Application.Quit();
        }
    }
}