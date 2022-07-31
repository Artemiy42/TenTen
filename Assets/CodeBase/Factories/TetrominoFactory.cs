using System.Collections.Generic;
using CodeBase.Board;
using CodeBase.Infrastructure.PersistentProgress;
using CodeBase.Themes;
using UnityEngine;

namespace CodeBase.Factories
{
    public class TetrominoFactory : MonoBehaviour, ITetrominoFactory
    {
        [SerializeField] private SerializableDictionary<TetrominoType, Tetromino> _tetrominoes;
        [SerializeField] private GameObject _slotPrefab;
        [SerializeField] private Block _blockPrefab;
        [SerializeField] private ThemeData _themeData;

        private List<IReaderProgress> _readerProgresses = new();
        private List<ISaverProgress> _saverProgresses = new();

        public IEnumerable<IReaderProgress> ReaderProgresses => _readerProgresses;
        public IEnumerable<ISaverProgress> SaverProgresses => _saverProgresses;

        public Tetromino GetTetrominoByType(TetrominoType tetrominoType)
        {
            var tetromino = Instantiate(_tetrominoes[tetrominoType]);
            tetromino.ChangeColor(_themeData.GetColor(tetromino.TetrominoType.ToColorType()));
            return tetromino;
        }

        public Block GetBlock(TetrominoType tetrominoType)
        {
            var block = Instantiate(_blockPrefab);
            block.ChangeColor(_themeData.GetColor(tetrominoType.ToColorType()));
            return block;
        }

        public GameObject GetSlot()
        {
            var slot = Instantiate(_slotPrefab);
            return slot;
        }
    }
}