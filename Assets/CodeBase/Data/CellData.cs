using System;
using CodeBase.Board;
using Newtonsoft.Json;

namespace CodeBase.Main
{
    [Serializable]
    public class CellData
    {
        public TetrominoType TetrominoType = TetrominoType.None;

        [JsonConstructor]
        public CellData(TetrominoType tetrominoType)
        {
            TetrominoType = tetrominoType;
        }
        
        public CellData()
        {
        }
    }
}