using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenuUI;

    public void Resume()
    {
        _pauseMenuUI.SetActive(false);
    }

    public void Pause()
    {
        _pauseMenuUI.SetActive(true);
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
