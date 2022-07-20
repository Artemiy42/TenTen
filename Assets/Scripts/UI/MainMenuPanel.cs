using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TenTen.UI
{
    public class MainMenuPanel : Panel
    {
        public event Action OnPlayButtonClicked;
        public event Action OnExitButtonClicked;
        
        [SerializeField] private TMP_Text _bestScore;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _exitButton;

        private PlayerData _playerData;
        
        public void Init(PlayerData playerData)
        {
            _playerData = playerData;
        }
        
        public override void Show()
        {
            gameObject.SetActive(true);
            UpdateBestScroreText();
        }

        public void UpdateBestScroreText()
        {
            _bestScore.SetText(_playerData.BestScore.ToString());
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetBestScore(int bestScore)
        {
            _bestScore.text = bestScore.ToString();
        }

        private void OnEnable()
        {
            _playButton.onClick.AddListener(RaisePlayEvent);
            _exitButton.onClick.AddListener(RaiseExitEvent);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(RaisePlayEvent);
            _exitButton.onClick.RemoveListener(RaiseExitEvent);
        }

        private void RaisePlayEvent()
        {
            OnPlayButtonClicked?.Invoke();
        }

        private void RaiseExitEvent()
        {
            OnExitButtonClicked?.Invoke();
        }
    }
}
