using Newtonsoft.Json;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TenTen.Board
{
    public class Cell : ICell
    {
        [JsonIgnore] 
        public GameObject Background { get; set; }
        
        [JsonIgnore] 
        public Block Block { get; set; }

        public ColorType ColorType { get; set; } = ColorType.Slot;
        public bool IsEmpty { get; set; } = true;

        public void AddBlock(Block block, ColorType colorType)
        {
            Block = block;
            ColorType = colorType;
            IsEmpty = false;
        }
        
        public void Clear()
        {
            if (Block != null)
            {
                Object.Destroy(Block.gameObject);
                Block = null;
            }
            IsEmpty = true;
        }

        public override string ToString()
        {
            return $"{nameof(ColorType)}: {ColorType}, {nameof(IsEmpty)}: {IsEmpty}";
        }
    }
}