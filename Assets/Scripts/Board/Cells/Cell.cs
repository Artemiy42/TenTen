﻿using UnityEngine;

namespace TenTen
{
    public class Cell : ICell
    {
        public GameObject Background { get; set; }
        public Block Block { get; set; }

        public TetrominoType TetrominoType { get; set; } = TetrominoType.None;
        public bool IsEmpty => TetrominoType == TetrominoType.None;

        public void AddBlock(Block block, TetrominoType tetrominoType)
        {
            Block = block;
            TetrominoType = tetrominoType;
        }
        
        public void Clear()
        {
            if (Block != null)
            {
                TetrominoType = TetrominoType.None;
                Block = null;
            }
        }

        public override string ToString()
        {
            return $"{nameof(TetrominoType)}: {TetrominoType}, {nameof(IsEmpty)}: {IsEmpty}";
        }
    }
}