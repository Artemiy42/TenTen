using System;
using System.Collections.Generic;
using DefaultNamespace;
using Factories;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public event Action<IEnumerable<Tetromino>> OnCreateTetrominoes; 
        
    [SerializeField] private TetrominoFactory _tetrominoFactory;
    [SerializeField] private GameObject[] _spawnSlots;
    [SerializeField] private List<TetrominoType> _possibleTetrominoes = new List<TetrominoType>();

    private readonly List<Tetromino> _liveTetrominoes = new List<Tetromino>();

    public bool IsEmpty => _liveTetrominoes.Count == 0;
    public IEnumerable<Tetromino> LiveTetrominoes => _liveTetrominoes;
    
    public void CreateTetrominoes()
    {
        for (int i = 0; i < _spawnSlots.Length; i++)
        {
            Tetromino tetromino = GetRandomTetromino();
            tetromino.transform.position = _spawnSlots[i].transform.position;
            tetromino.transform.parent = transform;
            tetromino.CacheResetPosition();
            tetromino.ReduceScale();
            _liveTetrominoes.Add(tetromino);
        }
        
        OnCreateTetrominoes?.Invoke(LiveTetrominoes);
    }

    public bool TryCreateTetrominoes()
    {
        if (IsEmpty)
        {
            CreateTetrominoes();
            return true;
        }
        
        return false;
    }

    public void Remove(Tetromino tetromino)
    {
        _liveTetrominoes.Remove(tetromino);
    }

    private Tetromino GetRandomTetromino()
    {
        TetrominoType tetrominoType = RandomUtility.GetRandomFromList(_possibleTetrominoes);
        return _tetrominoFactory.Get(tetrominoType);
    }
}
