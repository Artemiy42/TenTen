using System;
using Newtonsoft.Json;
using TenTen.Board;

namespace TenTen
{
    [Serializable]
    public class PlayerData
    {
        public Board<Cell> BoardData;
        public int CurrentScore;
        public int BestScore;
        
        public PlayerData()
        {
            BoardData = new Board<Cell>();
            CurrentScore = 0;
            BestScore = 0;
        }
        
        [JsonConstructor]
        public PlayerData(Board<Cell> boardData, int currentScore, int bestScore)
        {
            BoardData = boardData;
            CurrentScore = currentScore;
            BestScore = bestScore;
        }
    }
}