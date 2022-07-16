using System;
using System.Collections.Generic;
using DG.Tweening;
using TenTen.UI;
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
        [SerializeField] private HUD _hud;

        private Board _board;

        public void StartGame()
        {
            _board = new Board(new Cell[Height, Width]);
            boardView.Init(_board);
            boardView.CreateBackgroundSlots();
            boardView.Show();
            _spawner.CreateTetrominoes();
            _hud.Show();
        }

        public void ResetGame()
        {
            _board.Clear();
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
                block.SpriteRenderer.sortingLayerName = SortingLayerConstants.PieceLayer;
                sequence.Join(blockTransform.DOMove(new Vector3(coord.x, coord.y, blockTransform.position.z), 0.1f));
                _board[coord.x, coord.y].Block = block.gameObject;
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
                DeleteLine(index);
            }

            foreach (var index in columns)
            {
                DeleteColumn(index);
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

                Destroy(tetromino);
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
            for (var y = 0; y < Board.Height; y++)
            {
                for (var x = 0; x < Board.Width; x++)
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

        private void DeleteLine(int y)
        {
            for (var x = 0; x < Board.Width; x++)
            {
                ClearCellIfEmpty((x, y));
            }
        }

        private void DeleteColumn(int x)
        {
            for (var y = 0; y < Board.Height; y++)
            {
                ClearCellIfEmpty((x, y));
            }
        }

        private void ClearCellIfEmpty((int x, int y) position)
        {
            var cell = _board[position.x, position.y];

            if (cell.IsEmpty == false)
                cell.Clear();
        }
    }
}