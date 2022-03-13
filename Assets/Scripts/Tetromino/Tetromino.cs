using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using Utilities;

namespace DefaultNamespace
{
    public class Tetromino : MonoBehaviour
    {
        private const float LocalScaleMultiplayer = 1.7f;
        
        public Action<Tetromino> OnBlockEndMove;

        [SerializeField] private SortingGroup _sortingGroup;
        [SerializeField] private List<Transform> _blocks;
        
        private bool _moving;
        private bool _isBig = true;
        private Vector2 _resetPosition;

        public IEnumerable<Transform> Blocks => _blocks;
        
        public int GetScore()
        {
            return _blocks.Count;
        }

        public void CacheResetPosition()
        {
            _resetPosition = transform.position;
        }

        public void FinishMove()
        {
            enabled = false;
        }

        public void Reset()
        {
            transform.position = _resetPosition;
            _sortingGroup.sortingLayerName = SortingLayerConstants.PieceLayer;
            ReduceScale();
            _moving = false;
        }
        
        public void ReduceScale()
        {
            if (!_isBig)
            {
                Debug.LogError("Try reduce small block!");
                return;
            }

            transform.localScale /= LocalScaleMultiplayer;
            _isBig = false;
        }

        public void IncreaseScale()
        {
            if (_isBig)
            {
                Debug.LogError("Try increase big block!");
                return;
            }
            
            transform.localScale *= LocalScaleMultiplayer;
            _isBig = true;
        }

        public void SetSortingLayerForAllBlocks(string sortingLayer)
        {
            foreach (Transform block in _blocks)
            {
                block.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayer;
            }
        }

        private void OnMouseDown()
        {      
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            IncreaseScale();
            _sortingGroup.sortingLayerName = SortingLayerConstants.AirLayer;
            _moving = true;
        }

        private void OnMouseDrag()
        {   
            if (_moving)
            {
                Vector3 mousePosition = GetMousePosition();
                transform.position = new Vector3(mousePosition.x, mousePosition.y + 2, transform.position.z);
            }
        }

        private void OnMouseUp()
        {
            if (_moving)
            {
                OnBlockEndMove?.Invoke(this);
            }
        }
        
        private static Vector3 GetMousePosition()
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            return mousePos;
        }
    }
}