using UnityEngine;

namespace TenTen.Board
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;
    }
}