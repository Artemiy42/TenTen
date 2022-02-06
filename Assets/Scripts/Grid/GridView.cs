using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class GridView : MonoBehaviour
    {
        public event Action<int> BlockAdded;
        public event Action<int> ClearLines;
        
        [SerializeField] private GameObject _slot;

        private Grid _grid;

        public void Init(Grid grid)
        {
            _grid = grid;
        }

        public void CreateGrid()
        {
            for (int y = 0; y < Grid.Height; y++)
            {
                for (int x = 0; x < Grid.Width; x++)
                {
                    GameObject tile = Instantiate(_slot, gameObject.transform);
                    tile.transform.position = new Vector2(transform.position.x + x, transform.position.y + y);
                    _grid[x, y].Background = tile;
                }
            }
        }

        public bool CanAddToGrid(Tetromino tetromino)
        {
            foreach (Transform block in tetromino.Blocks)
            {
                int roundedX = Mathf.RoundToInt(block.position.x);
                int roundedY = Mathf.RoundToInt(block.position.y);

                if (roundedX < 0 || roundedX >= Grid.Width || roundedY < 0 || roundedY >= Grid.Height)
                {
                    return false;
                }

                if (_grid[roundedX, roundedY].IsEmpty == false)
                {
                    return false;
                }
            }

            return true;
        }

        public void AddTetrominoToGrid(Tetromino tetromino)
        {
            foreach (Transform block in tetromino.Blocks)
            {
                int roundedX = Mathf.RoundToInt(block.position.x);
                int roundedY = Mathf.RoundToInt(block.position.y);

                _grid[roundedX, roundedY].Block = block.gameObject;
            }

            TryClearFullLinesAndColumns();
            BlockAdded?.Invoke(tetromino.GetScore());
        }

        public bool CanAddTetromino(Tetromino tetromino)
        {
            for (int y = 0; y < Grid.Height; y++)
            {
                for (int x = 0; x < Grid.Width; x++)
                {
                    if (CanAddTetrominoByPosition(tetromino, x, y))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CanAddTetrominoByPosition(Tetromino tetromino, int x, int y)
        {
            foreach (Transform block in tetromino.Blocks)
            {
                float posX = x + block.localPosition.x;
                float posY = y + block.localPosition.y;

                int roundedX = Mathf.RoundToInt(posX);
                int roundedY = Mathf.RoundToInt(posY);

                if (roundedX < 0 || roundedX >= Grid.Width || roundedY < 0 || roundedY >= Grid.Height)
                {
                    return false;
                }

                if (_grid[roundedX, roundedY].IsEmpty == false)
                {
                    return false;
                }
            }

            return true;
        }

        private bool TryClearFullLinesAndColumns()
        {
            List<int> lines = _grid.GetFullLines();
            List<int> columns = _grid.GetFullColumns();

            foreach (int index in lines)
            {
                DeleteLine(index);
            }

            foreach (int index in columns)
            {
                DeleteColumn(index);
            }

            int countClears = lines.Count + columns.Count;
 
            if (countClears > 0)
            {
                ClearLines?.Invoke(countClears);
                return true;
            }

            return false;
        }

        private void DeleteLine(int y)
        {
            for (int x = 0; x < Grid.Width; x++)
            {
                Cell cell = _grid[x, y];

                if (cell.IsEmpty == false)
                {
                    cell.Clear();
                }
            }
        }

        private void DeleteColumn(int x)
        {
            for (int y = 0; y < Grid.Height; y++)
            {
                Cell cell = _grid[x, y];

                if (cell.IsEmpty == false)
                {
                    cell.Clear();
                }
            }
        }
    }
}
