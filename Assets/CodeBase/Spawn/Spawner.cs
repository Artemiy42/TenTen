using System;
using System.Collections.Generic;
using System.Linq;
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

        private readonly Tetromino[] _liveTetrominoes = new Tetromino[3];

        private ITetrominoFactory _tetrominoFactory;

        public bool HasLiveTetrominoes => _liveTetrominoes.Count(t => t != null) != 0;
        public IEnumerable<Tetromino> LiveTetrominoes => _liveTetrominoes.Where(t => t != null);

        public void Init(ITetrominoFactory tetrominoFactory)
        {
            _tetrominoFactory = tetrominoFactory;
        }
        
        public void CreateTetrominoes()
        {
            for (var i = 0; i < _spawnSlots.Length; i++)
            {
                var tetromino = SpawnRandomTetrominoInSpawnSlot(i);
                _liveTetrominoes[i] = tetromino;
            }

            OnCreateTetrominoes?.Invoke(LiveTetrominoes);
        }

        private Tetromino SpawnRandomTetrominoInSpawnSlot(int slotIndex)
        {
            var tetromino = GetRandomTetromino();
            PlaceTetrominoIntoSlot(tetromino, slotIndex);
            return tetromino;
        }

        private Tetromino SpawnTetrominoInSpawnSlot(TetrominoType tetrominoType, int slotIndex)
        {
            var tetromino = _tetrominoFactory.GetTetrominoByType(tetrominoType);
            PlaceTetrominoIntoSlot(tetromino, slotIndex);
            return tetromino;
        }

        private void PlaceTetrominoIntoSlot(Tetromino tetromino, int slotIndex)
        {
            tetromino.transform.position = _spawnSlots[slotIndex].position;
            tetromino.transform.parent = transform;
            tetromino.CacheResetPosition();
            tetromino.ReduceScale();
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
            var index = Array.IndexOf(_liveTetrominoes, tetromino);
            _liveTetrominoes[index] = null;
            Destroy(tetromino.gameObject);
        }

        public void RemoveAll()
        {
            foreach (var liveTetromino in LiveTetrominoes)
            {
                Destroy(liveTetromino.gameObject);
            }
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
                    _liveTetrominoes[i] = SpawnTetrominoInSpawnSlot(tetrominoType, i);
                }
            }
            
            OnCreateTetrominoes?.Invoke(LiveTetrominoes);
        }

        public void SaveProgress(PlayerProgress progress)
        {
            progress.LiveTetrominoes.Clear();
            foreach (var tetromino in _liveTetrominoes)
            {
                progress.LiveTetrominoes.Add(tetromino == null ? TetrominoType.None: tetromino.TetrominoType);
            }
        }
    }
}