using System.Collections.Generic;
using UnityEngine;

namespace TenTen
{
    public interface ITetrominoFactory
    {
        IEnumerable<IReaderProgress> ReaderProgresses { get; }
        IEnumerable<ISaverProgress> SaverProgresses { get; }

        Tetromino GetTetromino(TetrominoType tetrominoType);
        Block GetBlock(TetrominoType tetrominoType);
        GameObject GetSlot();
        void ReturnTetromino(Tetromino tetromino);
        void ReturnBlock(Block block);
    }
}