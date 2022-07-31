using System.Collections.Generic;
using CodeBase.Board;
using CodeBase.Infrastructure.PersistentProgress;
using UnityEngine;

namespace CodeBase.Factories
{
    public interface ITetrominoFactory
    {
        IEnumerable<IReaderProgress> ReaderProgresses { get; }
        IEnumerable<ISaverProgress> SaverProgresses { get; }

        Tetromino GetTetrominoByType(TetrominoType tetrominoType);
        Block GetBlock(TetrominoType tetrominoType);
        GameObject GetSlot();
    }
}