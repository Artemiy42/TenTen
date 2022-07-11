﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace TenTen.UI
{
    public class GameOverPanel : Panel
    {
        public event Action RestartButtonClicked;
        public event Action HomeButtonClicked;

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
            _restartButton.onClick.AddListener(RaiseRestartEvent);
            _homeButton.onClick.AddListener(RaiseHomeEvent);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(RaiseRestartEvent);
            _homeButton.onClick.RemoveListener(RaiseHomeEvent);
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