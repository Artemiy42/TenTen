using System;
using System.Collections.Generic;
using DG.Tweening;
using TenTen.Utilities;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

namespace TenTen.Board
{
    public class Tetromino : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public event Action<Tetromino> OnBlockEndMove;

        [SerializeField] private SortingGroup _sortingGroup;
        [SerializeField] private List<Block> _blocks;
        [SerializeField] private Vector3 _dragOffset;
        [SerializeField] private ColorType colorType;

        private const float LocalScaleMultiplayer = 1.7f;

        private Vector2 _resetPosition;
        private bool _isBig = true;
        private bool _moving;

        public IEnumerable<Block> Blocks => _blocks;
        public int AmountBlock => _blocks.Count;
        public ColorType ColorType => colorType;

        public void OnDrag(PointerEventData eventData)
        {
            if (_moving)
            {
                var mousePosition = GetMousePosition();
                transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z) + _dragOffset;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            IncreaseScale();
            _sortingGroup.sortingLayerName = SortingLayerConstants.AirLayer;
            _moving = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_moving)
            {
                _moving = false;
                OnBlockEndMove?.Invoke(this);
            }
        }

        public void CacheResetPosition()
        {
            _resetPosition = transform.position;
        }

        public void ReturnToStartState()
        {
            transform.DOMove(_resetPosition, 0.25f);
            _sortingGroup.sortingLayerName = SortingLayerConstants.PieceLayer;
            ReduceScale();
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

        public void ChangeColor(Color color)
        {
            foreach (var block in _blocks)
            {
                block.ChangeColor(color);
            }
        }

        private void OnDestroy()
        {
            UnsubscribeAll();
        }

        private static Vector3 GetMousePosition()
        {
            var mousePosition = Input.mousePosition;
            var worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            return worldPosition;
        }

        private void UnsubscribeAll()
        {
            if (OnBlockEndMove == null)
                return;

            foreach (var d in OnBlockEndMove.GetInvocationList())
            {
                OnBlockEndMove -= d as Action<Tetromino>;
            }
        }

        #if UNITY_EDITOR
        [ContextMenu("Find All Blocks")]
        private void FindAllBlocks()
        {
            var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            Block[] blocks;
            if (prefabStage == null)
                blocks = FindObjectsOfType<Block>();
            else
                blocks = prefabStage.FindComponentsOfType<Block>();

            _blocks = new List<Block>(blocks);
            EditorUtility.SetDirty(this);
        }

        #endif
    }
}