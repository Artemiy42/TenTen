using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameOver : MonoBehaviour, ISaveable
{
    [SerializeField] private Grid _grid;
    [SerializeField] private SpawnTetromino _spawnTetromino;
    [SerializeField] private GameOverMenu _gameOverMenu;

    private List<GameObject> _liveTetrominoes;

    private void Start()
    {
        _liveTetrominoes = new List<GameObject>();
        _spawnTetromino.CreateTetrominoes(_liveTetrominoes);
    }

    private void OnEnable()
    {
        _grid.BlockAdded += OnBlockAdded;
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
        Debug.Log("Save GameOver");
    }

    public void Load()
    {
        Debug.Log("Load GameOver");
    }
}
