using UnityEngine;

namespace TenTen.Board
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void SetSortingLayer(string sortingLayerName)
        {
            _spriteRenderer.sortingLayerName = sortingLayerName;
        }

        public void ChangeColor(Color color)
        {
            _spriteRenderer.color = color;
        }
    }
}