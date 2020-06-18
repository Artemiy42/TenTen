using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour, ISaveable
{
    [SerializeField] private Grid _grid;
    [SerializeField] private Spawner _spawnTetromino;
    [SerializeField] private GameOverMenu _gameOverMenu;

    private List<GameObject> _liveTetrominoes;

    private void Awake()
    {
        _liveTetrominoes = new List<GameObject>();
    }

    private void Start()
    {
        if (_liveTetrominoes.Count == 0)
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
        foreach (GameObject tetromino in _liveTetrominoes)
        {
            if (tetromino.activeSelf == false)
            {
                Destroy(tetromino);
                _liveTetrominoes.Remove(tetromino);
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
        return _liveTetrominoes.Count != 0; 
    }

    private bool CheckFail()
    {
        foreach (GameObject tetromino in _liveTetrominoes)
        {
            if (_grid.CanAddTetromino(tetromino))
            {
                return false;
            }
        }

        return true;
    }

    public void Save()
    {
        for (int i = 0; i < 3; i++)
        {
            string name = "";

            if (i < _liveTetrominoes.Count)
            {
                name = _liveTetrominoes[i].name;
            }


            int index = name.IndexOf("(");
            if (index > 0)
            {
                name = name.Substring(0, index);
            }

            PlayerPrefs.SetString("Tetromino" + i, name);
            Debug.Log("Save Tetromino" + i + ": " + name);
        }
    }

    public void Load()
    {
        for (int i = 0; i < 3; i++)
        {
            string name = PlayerPrefs.GetString("Tetromino" + i);
            if (name != "")
            {
                Debug.Log("Load Tetromino" + i + ": " + name);
                _liveTetrominoes.Add(_spawnTetromino.CreateTetrominoByName(name, i));
            }
        }
    }
}
