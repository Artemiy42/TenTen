using UnityEngine;

public class BestScoreChanger : BestScoreDisplayer, ISaveable
{
    [SerializeField] private ScoreDisplayer _textScore;

    private int _bestScore;

    private void OnEnable()
    {
        _textScore.ScoreChange += OnScoreChange;
        Load();
    }

    private void OnDisable()
    {
        _textScore.ScoreChange -= OnScoreChange;
        Save();
    }

    private void Start()
    {
        Display(_bestScore.ToString());
    }

    private void OnScoreChange(int newScore)
    {
        if (newScore > _bestScore)
        {
            _bestScore = newScore;
        }

        Display(_bestScore.ToString());
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Best score", _bestScore);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("Best score"))
        {
            _bestScore = PlayerPrefs.GetInt("Best score");
        }
        else
        {
            _bestScore = 0;
        }
    }
}
