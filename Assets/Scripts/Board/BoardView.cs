using System;
using UnityEngine;

namespace TenTen.Board
{
    public class BoardView : MonoBehaviour
    {
        [SerializeField] private Transform _slotsContainer;
        [SerializeField] private Transform _blocksContainer;
        [SerializeField] private GameObject _slot;
        [SerializeField] private bool _showDebugVisualization;

        private Board _board;

        public void Init(Board board)
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
            for (var y = 0; y < Board.Height; y++)
            {
                for (var x = 0; x < Board.Width; x++)
                {
                    var tile = Instantiate(_slot, _slotsContainer);
                    tile.transform.position = transform.position + new Vector3(x, y);
                    _board[x, y].Background = tile;
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
                for (var y = 0; y < Board.Height; y++)
                {
                    for (var x = 0; x < Board.Width; x++)
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