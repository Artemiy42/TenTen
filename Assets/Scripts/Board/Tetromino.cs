using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using NaughtyAttributes;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

namespace TenTen
{
    public class Tetromino : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public event Action<Tetromino> OnBlockEndMove;

        [SerializeField] private SortingGroup _sortingGroup;
        [SerializeField] private List<Block> _blocks;
        [SerializeField] private Vector3 _dragOffset;
        [SerializeField] private TetrominoType _tetrominoType;
        [ShowNonSerializedField] private bool _isBigDebug;

        private const float LocalScaleMultiplayer = 1.7f;

        private Vector2 _resetPosition;
        private bool _isBig = true;
        private bool _moving;

        public IEnumerable<Block> Blocks => _blocks.Where(b => b.gameObject.activeSelf);
        public int AmountBlock => _blocks.Count;
        public TetrominoType TetrominoType => _tetrominoType;

        public void Init(Tetromino tetromino)
        {
            _isBig = true;
            _moving = false;
            transform.localScale = Vector3.one;

            var i = 0;
            foreach (var activeBlock in tetromino.Blocks)
            {
                _blocks[i++].Init(activeBlock);
            }

            _tetrominoType = tetromino.TetrominoType;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_moving)
                return;

            IncreaseScale();
            _sortingGroup.sortingLayerName = SortingLayerConstants.AirLayer;
            _moving = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_moving)
                return;

            var mousePosition = GetMousePosition();
            transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z) + _dragOffset;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_moving)
                return;

            _moving = false;
            OnBlockEndMove?.Invoke(this);
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

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            foreach (var block in _blocks)
            {
                block.Deactivate();
            }

            gameObject.SetActive(false);
        }

        public void ReduceScale()
        {
            Debug.Log("Try reduce scale!");

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
            Debug.Log("Try increase scale!");

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

        // TODO Move this code from this to some InputController/InputService
        private static Vector3 GetMousePosition()
        {
            var mousePosition = Input.mousePosition;
            var worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            return worldPosition;
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            _isBigDebug = _isBig;
        }

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