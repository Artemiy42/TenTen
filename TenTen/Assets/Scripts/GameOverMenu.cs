using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverMenuUI;

    public void GameOver()
    {
        _gameOverMenuUI.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
    }
}
