using System.Collections.Generic;
using UnityEngine;

namespace TenTen.Board
{
    public class Board
    {
        private readonly ICell[,] _cells;
        
        public ICell this[int x, int y]
        {
            get => _cells[x, y];
            set => _cells[x, y] = value;
        }

        public Board(ICell[,] cells)
        {
            _cells = cells;
        }

        public void Clear()
        {
            for (var i = 0; i < _cells.GetLength(0); i++)
            {
                for (var j = 0; j < _cells.GetLength(1); j++)
                {
                    _cells[i, j].Clear();
                }
            } 
        }

        public List<int> GetFullLines()
        {
            var fullLines = new List<int>();

            for (var i = 0; i < _cells.GetLength(0); i++)
            {
                if (HasLine(i))
                    fullLines.Add(i);
            }

            return fullLines;
        }

        public List<int> GetFullColumns()
        {
            var fullColumns = new List<int>();

            for (var i = 0; i < _cells.GetLength(1); i++)
            {
                if (HasColumn(i))
                    fullColumns.Add(i);
            }

            return fullColumns;
        }

        public bool IsCoordinateOnGrid(Vector2Int coord)
        {
            return coord.x >= 0 && coord.x < _cells.GetLength(1) && coord.y >= 0 && coord.y < _cells.GetLength(0);
        }

        private bool HasLine(int y)
        {
            for (var x = 0; x < _cells.GetLength(1); x++)
            {
                if (_cells[x, y].IsEmpty)
                    return false;
            }

            return true;
        }

        private bool HasColumn(int x)
        {
            for (var y = 0; y < _cells.GetLength(0); y++)
            {
                if (_cells[x, y].IsEmpty)
                    return false;
            }

            return true;
        }
    }
}