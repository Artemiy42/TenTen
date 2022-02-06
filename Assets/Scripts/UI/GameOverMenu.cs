using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameOverMenu : MonoBehaviour
    {
        public event Action RestartButtonClicked;
        public event Action HomeButtonClicked; 
        
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _homeButton;

        private void Start()
        {
            _restartButton.onClick.AddListener(RaiseRestartEvent);
            _homeButton.onClick.AddListener(RaiseHomeEvent);
        }

        private void RaiseRestartEvent()
        {
            RestartButtonClicked?.Invoke();
        }

        private void RaiseHomeEvent()
        {
            HomeButtonClicked?.Invoke();
        }
    }
}