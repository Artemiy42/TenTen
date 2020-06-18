using TMPro;
using UnityEngine;

public class BestScoreDisplayer : MonoBehaviour
{
    [SerializeField] private TMP_Text _textBestScore;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Best score"))
        {
            Display(PlayerPrefs.GetInt("Best score").ToString());
        }
        else
        {
            Display(0.ToString());
        }
    }

    public void Display(string bestScore)
    {
        _textBestScore.text = bestScore;
    }
}
