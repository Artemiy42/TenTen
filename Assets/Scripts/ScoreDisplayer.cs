using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class ScoreDisplayer : MonoBehaviour
    {
        public event UnityAction<int> ScoreChange;
    
        [SerializeField] private GridView gridView;
        [SerializeField] private TMP_Text _textScore;

        private int _score;

        private void OnEnable()
        {
            gridView.BlockAdded += OnBlockAdded;    
            gridView.ClearLines += OnClearLines;
        }

        private void OnDisable()
        {
            gridView.BlockAdded -= OnBlockAdded;    
            gridView.ClearLines -= OnClearLines;
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
}