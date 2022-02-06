using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        public event Action ResumeButtonClicked; 
        public event Action RestartButtonClicked; 
        public event Action HomeButtonClicked; 
        
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _homeButton;

        private void Start()
        {
            _resumeButton.onClick.AddListener(RaiseResumeEvent);
            _restartButton.onClick.AddListener(RaiseRestartEvent);
            _homeButton.onClick.AddListener(RaiseHomeEvent);
        }

        private void RaiseResumeEvent()
        {
            ResumeButtonClicked?.Invoke();
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