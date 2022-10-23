using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TenTen
{
    public class HUD : Panel
    {
        public event Action OnPauseButtonClicked;

        [SerializeField] private Button _pauseButton;
        [SerializeField] private TextMeshProUGUI _bestScore;
        [SerializeField] private TextMeshProUGUI _currentScore;

        public void SetCurrentScore(int score)
        {
            _currentScore.text = score.ToString();
        }

        public void SetBestScore(int score)
        {
            _bestScore.text = score.ToString();
        }

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
            _pauseButton.onClick.AddListener(RaisePauseEvent);
        }

        private void OnDisable()
        {
            _pauseButton.onClick.RemoveListener(RaisePauseEvent);
        }

        private void RaisePauseEvent()
        {
            OnPauseButtonClicked?.Invoke();
        }
    }
}