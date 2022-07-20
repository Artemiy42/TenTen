using System;
using System.Collections.Generic;
using DG.Tweening;
using TenTen.Utilities;
using UnityEngine;

namespace TenTen.Board
{
    public class BoardController : MonoBehaviour
    {
        public const int Height = 10;
        public const int Width = 10;

        public event Action OnGameOver;
        public event Action<int> OnTetrominoAdded;
        public event Action<int> OnClearLines;

        [SerializeField] private BoardView boardView;
        [SerializeField] private Spawner _spawner;

        private Board<Cell> _board;
        public Board<Cell> Board => _board;

        public void Init(Board<Cell> boardData)
        {
            _board = boardData ?? new Board<Cell>(Width, Height);
            // var cellsData = boardData.Cells;
            //
            // for (int i = 0; i < cellsData.GetLength(0); i++)
            // {
            //     for (int j = 0; j < cellsData.GetLength(1); j++)
            //     {
            //         var cellData = cellsData[i, j];
            //         if (!cellData.IsEmpty)
            //         {
            //             _board[i, j].TetrominoType = cellData.TetrominoType;
            //         }
            //     }
            // }
            
            boardView.Init(_board);
            boardView.CreateBackgroundSlots();
        }

        public void StartGame()
        {
            boardView.Show();
            _spawner.CreateTetrominoes();
        }

        public void ResetGame()
        {
            _board.Clear();
            _spawner.RemoveAll();
        }

        private void OnEnable()
        {
            _spawner.OnCreateTetrominoes += SubscribeLiveTetrominoes;
        }

        private void OnDisable()
        {
            _spawner.OnCreateTetrominoes -= SubscribeLiveTetrominoes;
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
                var blockTransform = block.transform;
                var coord = boardView.GetCoordinatesOnGrid(blockTransform.position);
                blockTransform.parent = boardView.transform;
                block.SetSortingLayer(SortingLayerConstants.PieceLayer);
                sequence.Join(blockTransform.DOMove(new Vector3(coord.x, coord.y, blockTransform.position.z), 0.1f));

                var cell = _board[coord.x, coord.y];
                cell.AddBlock(block, tetromino.ColorType);
            }

            sequence.OnComplete(() =>
            {
                ClearFullLinesAndColumns();
                OnTetrominoAdded?.Invoke(tetromino.AmountBlock);
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

        private void SubscribeLiveTetrominoes(IEnumerable<Tetromino> liveTetrominoes)
        {
            foreach (var tetromino in liveTetrominoes)
            {
                tetromino.OnBlockEndMove += BlockEndMoveHandler;
            }
        }

        private void BlockEndMoveHandler(Tetromino tetromino)
        {
            if (CanAddToBoard(tetromino))
            {
                tetromino.OnBlockEndMove -= BlockEndMoveHandler;

                _spawner.Remove(tetromino);
                AddTetrominoToBoard(tetromino);
                _spawner.TryCreateTetrominoes();

                if (!CanAddAtLeastOneLiveTetrominoToBoard())
                    OnGameOver?.Invoke();

                Destroy(tetromino.gameObject);
            }
            else
            {
                tetromino.ReturnToStartState();
            }
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