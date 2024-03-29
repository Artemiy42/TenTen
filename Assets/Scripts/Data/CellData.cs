using System;
using Newtonsoft.Json;

namespace TenTen
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