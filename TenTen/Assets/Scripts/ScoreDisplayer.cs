using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreDisplayer : MonoBehaviour, ISaveable
{
    [SerializeField] private Grid _grid;
    [SerializeField] private TMP_Text _textScore;
    private int _score;

    public event UnityAction<int> ScoreChange;

    private void OnEnable()
    {
        _grid.BlockAdded += OnBlockAdded;    
        _grid.ClearLines += OnClearLines;
        SaveLoad.Instance().AddToList(this);
    }

    private void OnDisable()
    {
        _grid.BlockAdded -= OnBlockAdded;    
        _grid.ClearLines -= OnClearLines;
    }

    private void OnBlockAdded(int scoreCount)
    {
        _score += scoreCount;
        _textScore.text = _score.ToString();
        ScoreChange.Invoke(_score);
    }

    private void OnClearLines(int linesCount)
    {
        int an = 10 + (linesCount - 1) * 10;
        int sum = (10 + an) / 2 * linesCount;
        
        _score += sum;
        _textScore.text = _score.ToString();
        ScoreChange.Invoke(_score);
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Score", _score);
        Debug.Log("Save Score " + _score);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            _score = PlayerPrefs.GetInt("Score", _score);
        }
        else
        {
            _score = 0;
        }

        _textScore.text = _score.ToString();

        Debug.Log("Load Score " + _score);
    }
}
