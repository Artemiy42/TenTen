using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class BlockMove : MonoBehaviour
{
    [SerializeField] private static Grid grid;

    private bool moving;
    private bool finised;

    private float startPosX;
    private float startPosY;

    private bool isBig;

    private Vector3 resetPosition;

    void Start()
    {
        grid = FindObjectOfType<Grid>();
        resetPosition = this.transform.position;
        ReduceScale();
    }

    void Update()
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
            if (grid.CanAddToGrid(gameObject))
            {
                this.enabled = false;
                grid.AddTetrominoToGrid(gameObject);
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
