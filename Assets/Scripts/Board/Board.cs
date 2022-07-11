using System.Collections.Generic;
using UnityEngine;

namespace TenTen.Board
{
    public class Board
    {
        public const int Height = 10;
        public const int Width = 10;

        private readonly Cell[,] _cells = new Cell[Height, Width];
        
        public Cell this[int x, int y]
        {
            get => _cells[x, y];
            set => _cells[x, y] = value;
        }

        public Board()
        {
            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    _cells[i, j] = new Cell();
                }
            }
        }

        public List<int> GetFullLines()
        {
            var fullLines = new List<int>();

            for (var i = 0; i < Height; i++)
            {
                if (HasLine(i))
                    fullLines.Add(i);
            }

            return fullLines;
        }

        public List<int> GetFullColumns()
        {
            var fullColumns = new List<int>();

            for (var i = 0; i < Width; i++)
            {
                if (HasColumn(i))
                    fullColumns.Add(i);
            }

            return fullColumns;
        }

        public bool IsCoordinateOnGrid(Vector2Int coord)
        {
            return coord.x >= 0 && coord.x < Width && coord.y >= 0 && coord.y < Height;
        }

        private bool HasLine(int y)
        {
            for (var x = 0; x < Width; x++)
            {
                if (_cells[x, y].IsEmpty)
                    return false;
            }

            return true;
        }

        private bool HasColumn(int x)
        {
            for (var y = 0; y < Height; y++)
            {
                if (_cells[x, y].IsEmpty)
                    return false;
            }

            return true;
        }
    }
}