using System;
using UnityEngine;
using UnityEngine.UI;

namespace TenTen
{
    public class PauseMenu : Panel
    {
        public event Action OnResumeButtonClicked;
        public event Action OnRestartButtonClicked;
        public event Action OnHomeButtonClicked;

        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _homeButton;

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _resumeButton.onClick.AddListener(RaiseResumeEvent);
            _restartButton.onClick.AddListener(RaiseRestartEvent);
            _homeButton.onClick.AddListener(RaiseHomeEvent);
        }

        private void OnDisable()
        {
            _resumeButton.onClick.RemoveListener(RaiseResumeEvent);
            _restartButton.onClick.RemoveListener(RaiseRestartEvent);
            _homeButton.onClick.RemoveListener(RaiseHomeEvent);
        }

        private void RaiseResumeEvent()
        {
            OnResumeButtonClicked?.Invoke();
        }

        private void RaiseRestartEvent()
        {
            OnRestartButtonClicked?.Invoke();
        }

        private void RaiseHomeEvent()
        {
            OnHomeButtonClicked?.Invoke();
        }
    }
}