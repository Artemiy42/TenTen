using System;
using System.Collections.Generic;
using CodeBase.Board;
using CodeBase.Factories;
using CodeBase.Infrastructure.PersistentProgress;
using CodeBase.Main;
using CodeBase.Utilities;
using UnityEngine;

namespace CodeBase.Spawn
{
    public class Spawner : MonoBehaviour, ISaverProgress
    {
        public event Action<IEnumerable<Tetromino>> OnCreateTetrominoes;

        [SerializeField] private Transform[] _spawnSlots;
        [SerializeField] private List<TetrominoType> _possibleTetrominoes = new();

        private ITetrominoFactory _tetrominoFactory;
        private List<Tetromino> _liveTetrominoes;

        public bool HasLiveTetrominoes => _liveTetrominoes.Count != 0;
        public IEnumerable<Tetromino> LiveTetrominoes => _liveTetrominoes;

        public void Init(ITetrominoFactory tetrominoFactory)
        {
            _tetrominoFactory = tetrominoFactory;
        }
        
        public void CreateTetrominoes()
        {
            for (var i = 0; i < _spawnSlots.Length; i++)
            {
                var tetromino = SpawnTetrominoInSpawnSlot(i);
                _liveTetrominoes.Add(tetromino);
            }

            OnCreateTetrominoes?.Invoke(LiveTetrominoes);
        }

        private Tetromino SpawnTetrominoInSpawnSlot(int i)
        {
            var tetromino = GetRandomTetromino();
            tetromino.transform.position = _spawnSlots[i].position;
            tetromino.transform.parent = transform;
            tetromino.CacheResetPosition();
            tetromino.ReduceScale();
            return tetromino;
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
            Destroy(tetromino.gameObject);
        }

        public void RemoveAll()
        {
            foreach (var liveTetromino in LiveTetrominoes)
            {
                Destroy(liveTetromino.gameObject);
            }

            _liveTetrominoes.Clear();
        }

        private Tetromino GetRandomTetromino()
        {
            var tetrominoType = RandomUtility.GetRandomFromList(_possibleTetrominoes);
            return _tetrominoFactory.GetTetrominoByType(tetrominoType);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            for (var i = 0; i < progress.LiveTetrominoes.Count; i++)
            {
                var tetrominoType = progress.LiveTetrominoes[i];
                if (tetrominoType != TetrominoType.None)
                {
                    _liveTetrominoes[i] = SpawnTetrominoInSpawnSlot(i);
                }
            }
        }

        public void SaveProgress(PlayerProgress progress)
        {
            progress.LiveTetrominoes.Clear();
            
            for (var i = 0; i < _liveTetrominoes.Count; i++)
            {
                progress.LiveTetrominoes[i] = _liveTetrominoes[i].TetrominoType;
            }
        }
    }
}