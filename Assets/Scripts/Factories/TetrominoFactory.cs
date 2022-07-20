using TenTen.Board;
using UnityEngine;

namespace TenTen.Factories
{
    public class TetrominoFactory : MonoBehaviour
    {
        [SerializeField] private SerializableDictionary<TetrominoType, Tetromino> _tetrominoes;
        [SerializeField] private GameObject _slotPrefab;
        [SerializeField] private Block _blockPrefab;
        [SerializeField] private ThemeData _themeData;

        public Tetromino GetTetrominoByType(TetrominoType tetrominoType)
        {
            var tetromino = Instantiate(_tetrominoes[tetrominoType]);
            tetromino.ChangeColor(_themeData.GetColor(tetromino.ColorType));
            return tetromino;
        }
        
        public Block GetBlock(ColorType colorType)
        {
            var block = Instantiate(_blockPrefab);
            block.ChangeColor(_themeData.GetColor(colorType));
            return block;
        }

        public GameObject GetSlot()
        {
            var slot = Instantiate(_slotPrefab);
            return slot;
        }
    }
}