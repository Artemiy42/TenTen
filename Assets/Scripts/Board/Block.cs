using UnityEngine;

namespace TenTen
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void Init(Block block)
        {
            transform.localPosition = block.transform.localPosition;
            Activate();
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }
        
        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
        
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