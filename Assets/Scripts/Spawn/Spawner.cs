using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TenTen
{
    public class Spawner : MonoBehaviour, ISaverProgress
    {
        public event Action<Tetromino> OnSpawnTetromino;
        public event Action<Tetromino> OnDespawnTetromino;

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
        }

        private Tetromino SpawnRandomTetrominoInSpawnSlot(int slotIndex)
        {
            var randomTetrominoType = RandomUtility.GetRandomFromList(_possibleTetrominoes);
            var tetromino = SpawnTetrominoInSpawnSlot(randomTetrominoType, slotIndex);
            return tetromino;
        }

        private Tetromino SpawnTetrominoInSpawnSlot(TetrominoType tetrominoType, int slotIndex)
        {
            var tetromino = _tetrominoFactory.GetTetromino(tetrominoType);
            PlaceTetrominoIntoSlot(tetromino, slotIndex);
            OnSpawnTetromino?.Invoke(tetromino);
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
            OnDespawnTetromino?.Invoke(tetromino);
            var index = Array.IndexOf(_liveTetrominoes, tetromino);
            _tetrominoFactory.ReturnTetromino(tetromino); 
            _liveTetrominoes[index] = null;
        }

        public void RemoveAll()
        {
            for (var i = 0; i < _liveTetrominoes.Length; i++)
            {
                var tetromino = _liveTetrominoes[i];
                if (tetromino == null)
                    continue;
                
                OnDespawnTetromino?.Invoke(tetromino);
                _tetrominoFactory.ReturnTetromino(tetromino);
                _liveTetrominoes[i] = null;
            }
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