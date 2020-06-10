using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplayer : MonoBehaviour
{
    [SerializeField] private Grid _grid;
    [SerializeField] private TMP_Text _textScore;
    [SerializeField] private PauseMenu _pauseMenu;
    private int _score;

    private void OnEnable()
    {
        _grid.BlockAdded += OnBlockAdded;    
        _grid.ClearLines += OnClearLines;
    //    _pauseMenu.SaveState += SaveScore;
    //    _textScore.text = LoadScore().ToString();
    }

    private void OnDisable()
    {
        _grid.BlockAdded -= OnBlockAdded;    
        _grid.ClearLines -= OnClearLines;
    //   _pauseMenu.SaveState -= SaveScore;
    }

    private void OnBlockAdded(int scoreCount)
    {
        _score += scoreCount;
        _textScore.text = _score.ToString();
    }

    private void OnClearLines(int linesCount)
    {
        int an = 10 + (linesCount - 1) * 10;
        int sum = (10 + an) / 2 * linesCount;
        
        _score += sum;
        _textScore.text = _score.ToString();
    }

    private int LoadScore()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            _score = PlayerPrefs.GetInt("Score");
            return _score;
        }

        return 0;
    }

    private void SaveScore()
    {
    //    PlayerPrefs.SetInt("Score", _score);
    }
}
