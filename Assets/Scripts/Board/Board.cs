using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace TenTen
{
    public class Board<TCell> where TCell : ICell, new()
    {
        public const int DefaultWidth = 10;
        public const int DefaultHeight = 10;

        public event Action<TCell> OnCellClear; 

        [JsonProperty]
        private TCell[,] _cells;

        public TCell this[int x, int y]
        {
            get => _cells[x, y];
            set => _cells[x, y] = value;
        }

        [JsonIgnore]
        public int Width => _cells.GetLength(0);
        [JsonIgnore]
        public int Height => _cells.GetLength(1);

        [JsonConstructor]
        public Board(TCell[,] cells)
        {
            _cells = cells;
        }

        public Board() : this(DefaultWidth, DefaultHeight)
        {
        }

        public Board(int width, int height)
        {
            _cells = new TCell[width, height];
            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    _cells[i, j] = new TCell();
                }
            }
        }

        public void Clear()
        {
            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    ClearCellIfNotEmpty(i, j);
                }
            }
        }

        public List<int> GetFullLines()
        {
            var fullLines = new List<int>();

            for (var i = 0; i < Width; i++)
            {
                if (HasLine(i))
                    fullLines.Add(i);
            }

            return fullLines;
        }

        public List<int> GetFullColumns()
        {
            var fullColumns = new List<int>();

            for (var i = 0; i < Height; i++)
            {
                if (HasColumn(i))
                    fullColumns.Add(i);
            }

            return fullColumns;
        }

        public bool IsCoordinateOnGrid(Vector2Int coord)
        {
            return coord.x >= 0 && coord.x < Height && coord.y >= 0 && coord.y < Width;
        }

        public void DeleteLine(int y)
        {
            for (var x = 0; x < Width; x++)
            {
                ClearCellIfNotEmpty((x, y));
            }
        }

        public void DeleteColumn(int x)
        {
            for (var y = 0; y < Height; y++)
            {
                ClearCellIfNotEmpty((x, y));
            }
        }

        private bool HasLine(int y)
        {
            for (var x = 0; x < Height; x++)
            {
                if (_cells[x, y].IsEmpty)
                    return false;
            }

            return true;
        }

        private bool HasColumn(int x)
        {
            for (var y = 0; y < Width; y++)
            {
                if (_cells[x, y].IsEmpty)
                    return false;
            }

            return true;
        }

        private void ClearCellIfNotEmpty(int x, int y)
        {
            ClearCellIfNotEmpty((x, y));
        }
        
        private void ClearCellIfNotEmpty((int x, int y) position)
        {
            var cell = _cells[position.x, position.y];
            if (cell.IsEmpty == false)
            {
                OnCellClear?.Invoke(cell);
                cell.Clear();
            }
        }
    }
}