using System;
using UnityEngine.Pool;

namespace TenTen
{
    public class TetrominoPool : IObjectPool<Tetromino>
    {
        private readonly ObjectPool<Tetromino> _pool;

        public TetrominoPool(
            Func<Tetromino> createFunc,
            Action<Tetromino> actionOnDestroy = null,
            bool collectionCheck = true,
            int defaultCapacity = 10,
            int maxSize = 10000)
        {
            _pool = new ObjectPool<Tetromino>(
                createFunc, 
                ActivateTetromino, 
                DeactivateTetromino,
                actionOnDestroy,
                collectionCheck,
                defaultCapacity,
                maxSize);
        }

        #region IObjectPool

        public int CountInactive => _pool.CountInactive;

        public Tetromino Get()
        {
            return _pool.Get();
        }

        public PooledObject<Tetromino> Get(out Tetromino element)
        {
            return _pool.Get(out element);
        }

        public void Release(Tetromino element)
        {
            _pool.Release(element);
        }

        public void Clear()
        {
            _pool.Clear();
        }

        #endregion
        
        private void ActivateTetromino(Tetromino tetromino)
        {
            tetromino.Activate();
        }

        private void DeactivateTetromino(Tetromino tetromino)
        {
            tetromino.Deactivate();
        }
    }
}