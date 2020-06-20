using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameOver : MonoBehaviour, ISaveable
{
    [SerializeField] private Grid _grid;
    [SerializeField] private Spawner _spawnTetromino;
    [SerializeField] private GameOverMenu _gameOverMenu;

    private GameObject[] _liveTetrominoes;

    private void Awake()
    {
        _liveTetrominoes = new GameObject[3];
    }

    private void Start()
    {
        if (!hasTetrominoes())
        {
            _spawnTetromino.CreateTetrominoes(_liveTetrominoes);
        }
    }

    private void OnEnable()
    {
        _grid.BlockAdded += OnBlockAdded;
        SaveLoad.Instance().AddToList(this);
    }

    private void OnDisable()
    {
        _grid.BlockAdded -= OnBlockAdded;
    }

    private void OnBlockAdded(int _)
    {
        for (int i = 0; i < _liveTetrominoes.Length; i++)
        {
            if (_liveTetrominoes[i] != null && _liveTetrominoes[i].activeSelf == false)
            {
                Destroy(_liveTetrominoes[i]);
                _liveTetrominoes[i] = null;
                break;
            }
        }

        if (!hasTetrominoes())
        {
            _spawnTetromino.CreateTetrominoes(_liveTetrominoes);
        }

        if (CheckFail())
        {
            _gameOverMenu.GameOver();
        }
    }

    public bool hasTetrominoes()
    {
        Debug.Log("HasTetrominoes = " + _liveTetrominoes.Where(c => c != null).Count());
        return _liveTetrominoes.Where(c => c != null).Count() != 0; 
    }

    private bool CheckFail()
    {
        for (int i = 0; i < _liveTetrominoes.Length; i++)
        {
            if (_liveTetrominoes[i] != null)
            {
                if (_grid.CanAddTetromino(_liveTetrominoes[i].transform.GetChild(0)))
                {
                    return false;
                }
            }
        }
        
        return true;
    }

    public void Save()
    {
        for (int i = 0; i < _liveTetrominoes.Length; i++)
        {
            string name = "";

            if (_liveTetrominoes[i] != null)
            {
                name = _liveTetrominoes[i].name;
                int index = name.IndexOf("(");
                if (index > 0)
                {
                    name = name.Substring(0, index);
                }

                Debug.Log("Save Tetromino" + i + ": " + name);
            }
            
            PlayerPrefs.SetString("Tetromino" + i, name);
        }
    }

    public void Load()
    {
        for (int i = 0; i < _liveTetrominoes.Length; i++)
        {
            string name = PlayerPrefs.GetString("Tetromino" + i);
            if (name != "")
            {
                Debug.Log("Load Tetromino" + i + ": " + name);
                _liveTetrominoes[i] = _spawnTetromino.CreateTetrominoByName(name, i);
            }
        }
    }
}
