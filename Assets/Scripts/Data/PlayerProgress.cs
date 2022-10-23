using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TenTen
{
    [Serializable]
    public class PlayerProgress
    {
        public BoardData BoardData;
        public List<TetrominoType> LiveTetrominoes;
        public int CurrentScore;
        public int BestScore;
        
        public PlayerProgress()
        {
            BoardData = new BoardData(BoardController.Height, BoardController.Width);
            LiveTetrominoes = new List<TetrominoType>();
            CurrentScore = 0;
            BestScore = 0;
        }
        
        [JsonConstructor]
        public PlayerProgress(BoardData boardData, List<TetrominoType> liveTetrominoes, int currentScore, int bestScore)
        {
            BoardData = boardData ?? new BoardData(BoardController.Height, BoardController.Width);
            LiveTetrominoes = liveTetrominoes ?? new List<TetrominoType>();
            CurrentScore = currentScore;
            BestScore = bestScore;
        }
    }
}