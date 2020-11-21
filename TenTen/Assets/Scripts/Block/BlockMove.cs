using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class BlockMove : MonoBehaviour
{
    [SerializeField] private static Grid _grid;

    private BlockSimulation _blockSimulation;

    private bool moving;
    private bool finised;

    private float startPosX;
    private float startPosY;

    private bool isBig;

    private Vector3 resetPosition;

    private void Awake()
    {
        _grid = FindObjectOfType<Grid>();
    }

    private void Start()
    {
        resetPosition = this.transform.position;
        ReduceScale();
    }

    private void Update()
    {
        if (moving)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.position = new Vector3(mousePos.x, mousePos.y + 2, this.gameObject.transform.position.z);
        }
    }

    private void OnMouseDown()
    {      
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            IncreaseScale();
            GetComponent<SortingGroup>().sortingLayerName = "Air";
            moving = true;
        }
    }

    private void OnMouseUp()
    {
        if (moving && !finised)
        {
            if (_grid.CanAddToGrid(gameObject.transform.GetChild(0)))
            {
                this.enabled = false;
                gameObject.SetActive(false);
                _grid.AddTetrominoToGrid(gameObject.transform.GetChild(0));
                finised = true;
            }
            else
            {
                this.transform.position = resetPosition;
                GetComponent<SortingGroup>().sortingLayerName = "Piece";
                ReduceScale();
                moving = false;
            }
        }
    }

    private void ReduceScale()
    {
        transform.localScale /= 1.7f;
        isBig = false;
    }

    private void IncreaseScale()
    {
        if (!isBig)
        {
            transform.localScale *= 1.7f;
            isBig = true;
        }
    }
}
