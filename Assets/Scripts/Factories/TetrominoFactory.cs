using System.Collections.Generic;
using UnityEngine;

namespace TenTen
{
    public class TetrominoFactory : MonoBehaviour, ITetrominoFactory
    {
        [SerializeField] private Tetromino _baseTetrominoPrefab;
        [SerializeField] private SerializableDictionary<TetrominoType, Tetromino> _tetrominoes;
        [SerializeField] private GameObject _slotPrefab;
        [SerializeField] private Block _blockPrefab;
        [SerializeField] private ThemeData _themeData;
        [SerializeField] private Transform _blockContainer;
        [SerializeField] private Transform _tetrominoContainer;
        
        private TetrominoPool _tetrominoPool;
        private BlockPool _blockPool;
        
        private List<IReaderProgress> _readerProgresses = new();
        private List<ISaverProgress> _saverProgresses = new();

        public IEnumerable<IReaderProgress> ReaderProgresses => _readerProgresses;
        public IEnumerable<ISaverProgress> SaverProgresses => _saverProgresses;

        public void Init()
        {
            _tetrominoPool = new TetrominoPool(CreateTetromino);
            _blockPool = new BlockPool(CreateBlock);
        }

        public Tetromino GetTetromino(TetrominoType tetrominoType)
        {
            Debug.Log("Get tetromino!");
            var tetrominoTemplate = _tetrominoes[tetrominoType];
            var tetromino = _tetrominoPool.Get();
            tetromino.Init(tetrominoTemplate);
            tetromino.ChangeColor(_themeData.GetColor(tetromino.TetrominoType.ToColorType()));
            return tetromino;
        }

        public Block GetBlock(TetrominoType tetrominoType)
        {
            Debug.Log("Get block!");
            var block = _blockPool.Get();
            block.ChangeColor(_themeData.GetColor(tetrominoType.ToColorType()));
            return block;
        }

        public GameObject GetSlot()
        {
            var slot = Instantiate(_slotPrefab);
            return slot;
        }

        public void ReturnTetromino(Tetromino tetromino)
        {
            Debug.Log("Return tetromino!");
            tetromino.transform.parent = _tetrominoContainer;
            _tetrominoPool.Release(tetromino);   
        }
        
        public void ReturnBlock(Block block)
        {
            Debug.Log("Return block!");
            block.transform.parent = _blockContainer;
            _blockPool.Release(block);   
        }

        private Tetromino CreateTetromino()
        {
            return Instantiate(_baseTetrominoPrefab, _tetrominoContainer);
        }

        private Block CreateBlock()
        {
            return Instantiate(_blockPrefab, _blockContainer);
        }
    }
}