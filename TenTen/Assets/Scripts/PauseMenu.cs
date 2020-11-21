using UnityEngine;
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

    public void SwtichPause()
    {
        _pauseMenuUI.SetActive(!_pauseMenuUI.activeSelf);
    }
}
