using System;
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
    }
}