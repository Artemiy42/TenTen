using TenTen.Board;
using UnityEngine;

namespace TenTen.Factories
{
    public class TetrominoFactory : MonoBehaviour
    {
        [SerializeField] private SerializableDictionary<TetrominoType, Tetromino> _tetrominoes;
        
        public Tetromino Get(TetrominoType tetrominoType)
        {
            return Instantiate(_tetrominoes[tetrominoType]);
        }
    }
}