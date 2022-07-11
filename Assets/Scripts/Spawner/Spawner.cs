using System;
using System.Collections.Generic;
using TenTen.Factories;
using TenTen.Board;
using TenTen.Utilities;
using UnityEngine;

namespace TenTen
{
    public class Spawner : MonoBehaviour
    {
        public event Action<IEnumerable<Tetromino>> OnCreateTetrominoes;

        [SerializeField] private TetrominoFactory _tetrominoFactory;
        [SerializeField] private Transform[] _spawnSlots;
        [SerializeField] private List<TetrominoType> _possibleTetrominoes = new List<TetrominoType>();

        private readonly List<Tetromino> _liveTetrominoes = new List<Tetromino>();

        public bool HasLiveTetrominoes => _liveTetrominoes.Count != 0;
        public IEnumerable<Tetromino> LiveTetrominoes => _liveTetrominoes;

        public void CreateTetrominoes()
        {
            for (var i = 0; i < _spawnSlots.Length; i++)
            {
                var tetromino = GetRandomTetromino();
                tetromino.transform.position = _spawnSlots[i].position;
                tetromino.transform.parent = transform;
                tetromino.CacheResetPosition();
                tetromino.ReduceScale();
                _liveTetrominoes.Add(tetromino);
            }

            OnCreateTetrominoes?.Invoke(LiveTetrominoes);
        }

        public bool TryCreateTetrominoes()
        {
            if (!HasLiveTetrominoes)
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
            var tetrominoType = RandomUtility.GetRandomFromList(_possibleTetrominoes);
            return _tetrominoFactory.Get(tetrominoType);
        }
    }
}