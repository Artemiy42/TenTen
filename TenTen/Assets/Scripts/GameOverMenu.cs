using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverMenuUI;

    public void GameOver()
    {
        _gameOverMenuUI.SetActive(true);
        SaveLoad.Instance().DeleteSave();
        SaveLoad.Instance().Clear();
    }

    public void Restart()
    {
        SaveLoad.Instance().DeleteSave();
        SaveLoad.Instance().Clear();
        SceneManager.LoadScene("Game");
    }

    public void Home()
    {
        SaveLoad.Instance().Save();
        SaveLoad.Instance().Clear();
        SceneManager.LoadScene(0);
    }
}
