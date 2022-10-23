using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace TenTen
{
    public class BoardController : MonoBehaviour, ISaverProgress
    {
        public const int Height = 10;
        public const int Width = 10;

        public event Action OnGameOver;
        public event Action<int> OnTetrominoAdded;
        public event Action<int> OnClearLines;

        [SerializeField] private BoardView boardView;
        [SerializeField] private Spawner _spawner;

        private ITetrominoFactory _tetrominoFactory;
        private Board<Cell> _board = new(Width, Height);

        public bool IsGameOver { get; private set; }
        
        public void Init(ITetrominoFactory tetrominoFactory)
        {
            _tetrominoFactory = tetrominoFactory;
            _spawner.Init(tetrominoFactory);
            boardView.Init(_board, tetrominoFactory);
            
            _board.OnCellClear += OnCellClearHandler;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            var cellDatas = progress.BoardData.CellDatas;
            for (int i = 0; i < cellDatas.GetLength(0); i++)
            {
                for (int j = 0; j < cellDatas.GetLength(1); j++)
                {
                    var tetrominoType = cellDatas[i, j].TetrominoType;
                    var cell = _board[i, j];
                    cell.TetrominoType = tetrominoType;
                    
                    if (!cell.IsEmpty)
                    {
                        var block = _tetrominoFactory.GetBlock(tetrominoType);
                        block.SetSortingLayer(SortingLayerConstants.PieceLayer);
                        block.transform.position = new Vector3(i, j, 0);
                        cell.Block = block;
                    }
                }
            }
            _spawner.LoadProgress(progress);
        }

        public void SaveProgress(PlayerProgress progress)
        {
            if (IsGameOver)
            {
                progress.BoardData.Clear();
                progress.LiveTetrominoes.Clear();
                return;
            }
            
            var cellDatas = progress.BoardData.CellDatas;
            for (int i = 0; i < cellDatas.GetLength(0); i++)
            {
                for (int j = 0; j < cellDatas.GetLength(1); j++)
                {
                    cellDatas[i, j].TetrominoType = _board[i, j].TetrominoType;
                }
            }
            _spawner.SaveProgress(progress);
        }

        public void StartGame()
        {
            boardView.Show();
            _spawner.TryCreateTetrominoes();
            IsGameOver = false;
        }

        public void ResetGame()
        {
            _board.Clear();
            _spawner.RemoveAll();
            IsGameOver = false;
        }

        private void OnEnable()
        {
            _spawner.OnSpawnTetromino += SpawnTetrominoHandle;
            _spawner.OnDespawnTetromino += DespawnTetrominoHandle;
        }

        private void OnDisable()
        {
            _spawner.OnSpawnTetromino -= SpawnTetrominoHandle;
            _spawner.OnDespawnTetromino -= DespawnTetrominoHandle;
        }

        private void SpawnTetrominoHandle(Tetromino tetromino)
        {
            tetromino.OnBlockEndMove += BlockEndMoveHandler;
        }

        private void DespawnTetrominoHandle(Tetromino tetromino)
        {
            tetromino.OnBlockEndMove -= BlockEndMoveHandler;
        }

        private bool CanAddToBoard(Tetromino tetromino)
        {
            foreach (var block in tetromino.Blocks)
            {
                var coord = boardView.GetCoordinatesOnGrid(block.transform.position);

                if (!_board.IsCoordinateOnGrid(coord))
                    return false;

                if (!_board[coord.x, coord.y].IsEmpty)
                    return false;
            }

            return true;
        }

        private void AddTetrominoToBoard(Tetromino tetromino)
        {
            var sequence = DOTween.Sequence();
            foreach (var block in tetromino.Blocks)
            {
                var replacementBlock = _tetrominoFactory.GetBlock(tetromino.TetrominoType);
                replacementBlock.SetSortingLayer(SortingLayerConstants.PieceLayer);
                
                var replacementBlockTransform = replacementBlock.transform;
                replacementBlockTransform.position = block.transform.position;
                replacementBlockTransform.parent = boardView.transform;
                
                var coord = boardView.GetCoordinatesOnGrid(replacementBlockTransform.position);
                sequence.Join(replacementBlockTransform.DOMove(new Vector3(coord.x, coord.y, replacementBlockTransform.position.z), 0.1f));
                
                var cell = _board[coord.x, coord.y];
                cell.AddBlock(replacementBlock, tetromino.TetrominoType);
            }

            sequence.OnComplete(() =>
            {
                ClearFullLinesAndColumns();

                OnTetrominoAdded?.Invoke(tetromino.AmountBlock);

                if (!CanAddAtLeastOneLiveTetrominoToBoard())
                {
                    IsGameOver = true;
                    OnGameOver?.Invoke();
                }
            });
        }

        private void ClearFullLinesAndColumns()
        {
            var lines = _board.GetFullLines();
            var columns = _board.GetFullColumns();

            foreach (var index in lines)
            {
                _board.DeleteLine(index);
            }

            foreach (var index in columns)
            {
                _board.DeleteColumn(index);
            }

            var countClears = lines.Count + columns.Count;
            if (countClears > 0)
                OnClearLines?.Invoke(countClears);
        }
        
        private void BlockEndMoveHandler(Tetromino tetromino)
        {
            if (CanAddToBoard(tetromino))
            {
                tetromino.OnBlockEndMove -= BlockEndMoveHandler;

                AddTetrominoToBoard(tetromino);
                
                _spawner.Remove(tetromino);
                _spawner.TryCreateTetrominoes();
            }
            else
            {
                tetromino.ReturnToStartState();
            }
        }
        
        private void OnCellClearHandler(Cell cell)
        {
            _tetrominoFactory.ReturnBlock(cell.Block);
        }

        private bool CanAddAtLeastOneLiveTetrominoToBoard()
        {
            foreach (var liveTetromino in _spawner.LiveTetrominoes)
            {
                if (HasPlaceForTetromino(liveTetromino))
                    return true;
            }

            return false;
        }

        private bool HasPlaceForTetromino(Tetromino tetromino)
        {
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    if (CanAddToGridByPosition(tetromino, new Vector2Int(x, y)))
                        return true;
                }
            }

            return false;
        }

        private bool CanAddToGridByPosition(Tetromino tetromino, Vector2Int gridPosition)
        {
            foreach (var block in tetromino.Blocks)
            {
                var blockLocalPosition = block.transform.localPosition;
                var blockGridPosition = gridPosition + (Vector2) blockLocalPosition;
                var coord = boardView.GetCoordinatesOnGrid(blockGridPosition);

                if (!_board.IsCoordinateOnGrid(coord))
                    return false;

                if (_board[coord.x, coord.y].IsEmpty == false)
                    return false;
            }

            return true;
        }
    }
}