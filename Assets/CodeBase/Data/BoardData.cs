using System;
using CodeBase.Board;
using Newtonsoft.Json;

namespace CodeBase.Main
{
    [Serializable]
    public class BoardData
    {
        public CellData[,] CellDatas;
        
        [JsonConstructor]
        public BoardData(CellData[,] cellDatas)
        {
            CellDatas = cellDatas;
        }
        
        public BoardData(int width, int height)
        {
            CellDatas = new CellData[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    CellDatas[i, j] = new CellData();
                }
            }
        }

        public void Clear()
        {
            for (int i = 0; i < CellDatas.GetLength(0); i++)
            {
                for (int j = 0; j < CellDatas.GetLength(1); j++)
                {
                    CellDatas[i, j].TetrominoType = TetrominoType.None;
                }
            }   
        }
    }
}