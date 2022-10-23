using System;
using UnityEngine.Pool;

namespace TenTen
{
    public class BlockPool : IObjectPool<Block>
    {
        private readonly ObjectPool<Block> _pool;

        public BlockPool(
            Func<Block> createFunc,
            Action<Block> actionOnDestroy = null,
            bool collectionCheck = true,
            int defaultCapacity = 10,
            int maxSize = 10000)
        {
            _pool = new ObjectPool<Block>(
                createFunc, 
                ActivateBlock, 
                DeactivateBlock,
                actionOnDestroy,
                collectionCheck,
                defaultCapacity,
                maxSize);
        }

        #region IObjectPool

        public int CountInactive => _pool.CountInactive;

        public Block Get()
        {
            return _pool.Get();
        }

        public PooledObject<Block> Get(out Block element)
        {
            return _pool.Get(out element);
        }

        public void Release(Block element)
        {
            _pool.Release(element);
        }

        public void Clear()
        {
            _pool.Clear();
        }

        #endregion
        
        private void ActivateBlock(Block block)
        {
            block.Activate();
        }

        private void DeactivateBlock(Block block)
        {
            block.Deactivate();
        }
    }
}