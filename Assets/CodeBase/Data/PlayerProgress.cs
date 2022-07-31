using System;
using System.Collections.Generic;
using CodeBase.Board;
using Newtonsoft.Json;

namespace CodeBase.Main
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
            BoardData = new BoardData();
            LiveTetrominoes = new List<TetrominoType>();
            CurrentScore = 0;
            BestScore = 0;
        }
        
        [JsonConstructor]
        public PlayerProgress(BoardData boardData, List<TetrominoType> liveTetrominoes, int currentScore, int bestScore)
        {
            BoardData = boardData ?? new BoardData();
            LiveTetrominoes = liveTetrominoes ?? new List<TetrominoType>();
            CurrentScore = currentScore;
            BestScore = bestScore;
        }
    }
}