using UnityEngine;

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

    public void SwitchPause()
    {
        _pauseMenuUI.SetActive(!_pauseMenuUI.activeSelf);
    }
}
