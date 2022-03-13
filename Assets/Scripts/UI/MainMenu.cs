using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        public event Action OnPlayButtonClicked;
        public event Action OnExitButtonClicked;

        [SerializeField] private Button _playButton;
        [SerializeField] private Button _exitButton;
    
    
        private void Start()
        {
            _playButton.onClick.AddListener(RaisePlayEvent);
            _exitButton.onClick.AddListener(RaiseExitEvent);
        }

        public void Show()
        {
            gameObject.SetActive(true);    
        }
    
        public void Hide()
        {
            gameObject.SetActive(false);
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
