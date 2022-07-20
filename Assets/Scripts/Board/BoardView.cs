using System;
using TenTen.Factories;
using TenTen.Utilities;
using UnityEngine;

namespace TenTen.Board
{
    public class BoardView : MonoBehaviour
    {
        [SerializeField] private Transform _slotsContainer;
        [SerializeField] private Transform _blocksContainer;
        [SerializeField] private TetrominoFactory _tetrominoFactory;
        [SerializeField] private bool _showDebugVisualization;

        private Board<Cell> _board;

        public void Init(Board<Cell> board)
        {
            _board = board;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void CreateBackgroundSlots()
        {
            for (var y = 0; y < _board.Height; y++)
            {
                for (var x = 0; x < _board.Width; x++)
                {
                    var gridPosition = new Vector3(x, y);
                    var tile = _tetrominoFactory.GetSlot();
                    tile.transform.parent = _slotsContainer;
                    tile.transform.position = transform.position + gridPosition;
                    
                    var cell = _board[x, y];
                    cell.Background = tile;
                    
                    if (!cell.IsEmpty)
                    {
                        var block = _tetrominoFactory.GetBlock(cell.ColorType);
                        block.transform.parent = _blocksContainer;
                        block.transform.position = transform.position + gridPosition;
                        block.SetSortingLayer(SortingLayerConstants.PieceLayer);
                        cell.Block = block;
                    }
                }
            }
        }

        public Vector2Int GetCoordinatesOnGrid(Vector2 localPosition)
        {
            int roundedX = Utilities.Mathf.RoundToInt(localPosition.x, MidpointRounding.AwayFromZero);
            int roundedY = Utilities.Mathf.RoundToInt(localPosition.y, MidpointRounding.AwayFromZero);

            return new Vector2Int(roundedX, roundedY);
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_showDebugVisualization && _board != null)
            {
                Gizmos.color = Color.magenta;
                for (var y = 0; y < _board.Height; y++)
                {
                    for (var x = 0; x < _board.Width; x++)
                    {
                        var cell = _board[x, y];
                        if (!cell.IsEmpty)
                        {
                            Gizmos.DrawSphere(cell.Block.transform.position, 0.15f);
                        }
                    }
                }
            }
        }
        #endif
    }
}