using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitManager : MonoBehaviour
{
    private PauseMenu _pauseMenu;

    private void Start()
    {
        _pauseMenu = FindObjectOfType<PauseMenu>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "Game")
            {
                _pauseMenu.SwtichPause();
            }
        }
    }
}
