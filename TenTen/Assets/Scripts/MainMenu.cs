using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string _sceneGameName;

    public void Play()
    {
        SceneManager.LoadScene(_sceneGameName);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
