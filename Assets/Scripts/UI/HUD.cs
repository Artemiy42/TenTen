using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HUD : MonoBehaviour
    {
        public event Action PauseButtonClicked;
        
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

        private void Start()
        {
            _pauseButton.onClick.AddListener(RaisePauseEvent);
        }

        private void RaisePauseEvent()
        {
            PauseButtonClicked?.Invoke();
        }
    }
}
