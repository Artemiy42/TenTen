using UnityEngine;

namespace TenTen.Board
{
    public class Cell : ICell
    {
        public GameObject Background { get; set; }
        public GameObject Block { get; set; }

        public bool IsEmpty => Block == null;

        public void Clear()
        {
            Object.Destroy(Block);
            Block = null;
        }
    }
}