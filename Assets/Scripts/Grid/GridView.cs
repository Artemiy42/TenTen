using UnityEngine;

namespace DefaultNamespace
{
    public class GridView : MonoBehaviour
    {
        [SerializeField] private Transform _slotsContainer;
        [SerializeField] private Transform _blocksContainer;
        [SerializeField] private GameObject _slot;

        private Grid _grid;

        public void Init(Grid grid)
        {
            _grid = grid;
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
            for (int y = 0; y < Grid.Height; y++)
            {
                for (int x = 0; x < Grid.Width; x++)
                {
                    GameObject tile = Instantiate(_slot, _slotsContainer);
                    tile.transform.position = new Vector2(transform.position.x + x, transform.position.y + y);
                    _grid[x, y].Background = tile;
                }
            }
        }

        public void MoveBlockToCell(Transform block, (int x, int y) cellPosition)
        {
            
        }

        public (int x, int y) GetCoordinatesOnGrid(Vector2 worldPosition)
        {
            return GetCoordinatesOnGrid((worldPosition.x, worldPosition.y));
        }

        public (int x, int y) GetCoordinatesOnGrid((float x, float y) worldPosition)
        {
            int roundedX = Mathf.RoundToInt(worldPosition.x);
            int roundedY = Mathf.RoundToInt(worldPosition.y);

            return (roundedX, roundedY);
        }
    }
}
