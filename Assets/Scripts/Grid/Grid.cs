using System.Collections.Generic;

namespace DefaultNamespace
{
    public class Grid
    {
        public const int Height = 10;
        public const int Width = 10;
        
        private Cell[,] _cells = new Cell[Height, Width];
        
        public Grid()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    _cells[i, j] = new Cell();
                }
            }
        }

        public Cell this[int x, int y]
        {
            get => _cells[x, y];
            set => _cells[x, y] = value;
        }

        public List<int> GetFullLines()
        {
            var fullLines = new List<int>();
            
            for (int i = 0; i < Height; i++)
            {
                if (HasLine(i))
                {
                    fullLines.Add(i);
                }
            }

            return fullLines;
        }

        private bool HasLine(int y)
        {
            for (int x = 0; x < Width; x++)
            {
                if (_cells[x, y].IsEmpty)
                {
                    return false;
                }
            }

            return true;
        }
        
        public List<int> GetFullColumns()
        {
            var fullColumns = new List<int>();

            for (int i = 0; i < Width; i++)
            {
                if (HasColumn(i))
                {
                    fullColumns.Add(i);
                }
            }
            
            return fullColumns;
        }
       
        private bool HasColumn(int x)
        {
            for (int y = 0; y < Height; y++)
            {
                if (_cells[x, y].IsEmpty)
                {
                    return false;
                }
            }
            
            return true;
        }
        
        public bool IsCoordinateOnGrid((int x, int y) coord)
        {
            if (coord.x < 0 || coord.x >= Grid.Width || coord.x < 0 || coord.y >= Grid.Height)
            {
                return false;
            }

            return true;
        }
    }
}