using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace DefaultNamespace
{
    public class GridController : MonoBehaviour
    {
        public event Action OnGameOver;
        public event Action<int> BlockAdded;
        public event Action<int> ClearLines;

        [SerializeField] private GridView _gridView;
        [SerializeField] private Spawner _spawner;

        private Grid _grid;
        
        private void OnEnable()
        {
            _spawner.OnCreateTetrominoes += SubscribeLiveTetrominoes;
        }

        private void OnDisable()
        {
            _spawner.OnCreateTetrominoes -= SubscribeLiveTetrominoes;
        }

        public void StartGame()
        {
            _grid = new Grid();
            _gridView.Init(_grid);
            _gridView.CreateBackgroundSlots();
            _gridView.Show();
            _spawner.CreateTetrominoes();
        }
        
        // TODO maybe need unsubscribe from live tetrominoes after leave app
        private void SubscribeLiveTetrominoes(IEnumerable<Tetromino> liveTetrominoes)
        {
            foreach (Tetromino tetromino in liveTetrominoes)
            {
                tetromino.OnBlockEndMove += BlockEndMoveHandler;
            }
        }
        
        private void BlockEndMoveHandler(Tetromino tetromino)
        {
            if (CanAddToGrid(tetromino))
            {
                tetromino.FinishMove();
                tetromino.OnBlockEndMove -= BlockEndMoveHandler;
                _spawner.Remove(tetromino);
                
                AddTetrominoToGrid(tetromino);
                // TODO i deleted destroyed tetromino after adding to grid then need think about what do with tetromino after add and HOW add 
                
                // TODO made clearing after ending adding to grid animation
                TryClearFullLinesAndColumns();
                
                // TODO create new tetrominoes after ending animation of clearing 
                _spawner.TryCreateTetrominoes();

                if (!CanAddAtLeastOneLiveTetrominoToGrid())
                {
                    OnGameOver?.Invoke();
                }
            }
            else
            {
                tetromino.Reset();
            }
        }
        
        public bool CanAddToGrid(Tetromino tetromino)
        {
            foreach (Transform block in tetromino.Blocks)
            {
                (int x, int y) coord = _gridView.GetCoordinatesOnGrid(block.position);

                if (!_grid.IsCoordinateOnGrid(coord)) return false;
                if (_grid[coord.y, coord.x].IsEmpty == false) return false;
            }

            return true;
        }
        
        public void AddTetrominoToGrid(Tetromino tetromino)
        {
            foreach (Transform block in tetromino.Blocks)
            {                
                // TODO move this code to gridView
                (int x, int y) coord = _gridView.GetCoordinatesOnGrid(block.position);
                block.position = new Vector3(coord.x, coord.y, block.position.z);
                block.parent = _gridView.transform; 
                tetromino.SetSortingLayerForAllBlocks(SortingLayerConstants.PieceLayer); 
                _grid[coord.x, coord.y].Block = block.gameObject;
            }
            
            BlockAdded?.Invoke(tetromino.GetScore());
        }

        private bool CanAddAtLeastOneLiveTetrominoToGrid()
        {
            foreach (Tetromino liveTetromino in _spawner.LiveTetrominoes)
            {
                if (HasPlaceForTetromino(liveTetromino))
                {
                    return false;
                }
            }
            
            return true;
        }
        
        public bool HasPlaceForTetromino(Tetromino tetromino)
        {
            for (int y = 0; y < Grid.Height; y++)
            {
                for (int x = 0; x < Grid.Width; x++)
                {
                    if (CanAddToGridByPosition(tetromino, (x, y)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        
        private bool CanAddToGridByPosition(Tetromino tetromino, (int x, int y) gridPosition)
        {
            foreach (Transform block in tetromino.Blocks)
            {
                Vector3 blockLocalPosition = block.localPosition;
                float blockPositionX = gridPosition.x + blockLocalPosition.x;
                float blockPositionY = gridPosition.y + blockLocalPosition.y;
                (int x, int y) coord = _gridView.GetCoordinatesOnGrid((blockPositionX, blockPositionY));

                if (!_grid.IsCoordinateOnGrid(coord)) return false;
                if (_grid[coord.y, coord.x].IsEmpty == false) return false;
            }

            return true;
        }
        
        public bool TryClearFullLinesAndColumns()
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
                ClearCellIfEmpty((x, y));
            }
        }

        private void DeleteColumn(int x)
        {
            for (int y = 0; y < Grid.Height; y++)
            {
                ClearCellIfEmpty((x, y));
            }
        }

        private void ClearCellIfEmpty((int x, int y) position)
        {
            Cell cell = _grid[position.x, position.y];

            if (cell.IsEmpty == false)
            {
                cell.Clear();
            }
        }
    }
}