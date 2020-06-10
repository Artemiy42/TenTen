using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class Grid : MonoBehaviour
{
    [SerializeField] private int height = 10;
    [SerializeField] private int width = 10;
    [SerializeField] private float tileSize = 1.5f;
    [SerializeField] private GameObject slot;

    private Transform[,] slots;

    private List<int> lines = new List<int>();
    private List<int> columns = new List<int>();

    public event UnityAction<int> BlockAdded;
    public event UnityAction<int> ClearLines;

    void Start()
    {
        slots = new Transform[height, width];
        CreateGrid();
    }

    private void CreateGrid()
    {
        for (int i = 0; i < height; ++i)
        {
            for (int j = 0; j < width; ++j)
            {
                GameObject tile = Instantiate(slot, gameObject.transform.position, Quaternion.identity);
                tile.transform.parent = gameObject.transform;

                float posX = j * tileSize;
                float posY = i * tileSize;

                tile.transform.position = new Vector2(transform.position.x + posX, transform.position.y + posY);
                slots[i, j] = null;
            }
        }
    }

    public void AddTetrominoToGrid(GameObject tetromino)
    {
        foreach (Transform block in tetromino.transform.GetComponentsInChildren<Transform>().Skip(1))
        {
            int roundedX = GetIndexByCoord(block.position.x);
            int roundedY = GetIndexByCoord(block.position.y);

            Transform childSlot = transform.GetChild(roundedY * 10 + roundedX);
            GameObject newBlock = Instantiate(slot, childSlot.transform.position, Quaternion.identity);

            newBlock.transform.parent= childSlot;
            newBlock.GetComponent<SpriteRenderer>().color = block.gameObject.GetComponent<SpriteRenderer>().color;
            slots[roundedX, roundedY] = newBlock.transform;
        }

        BlockAdded.Invoke(tetromino.transform.childCount);
        Destroy(tetromino); 
        CheckForLinesAndColumns();
    }

    public bool CanAddToGrid(GameObject tetromino)
    {
        foreach (Transform block in tetromino.transform)
        {
            int roundedX = GetIndexByCoord(block.position.x);
            int roundedY = GetIndexByCoord(block.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
            {
                return false;
            }

            if (slots[roundedX, roundedY] != null)
            {
                return false;
            }
        }

        return true;
    }

    private int GetIndexByCoord(float coord)
    {
        int index;

        index = Mathf.FloorToInt(coord / tileSize);

        return index;
    }

    private bool CheckFail(GameObject tetromino)
    {
        return false;
    }

    private void CheckForLinesAndColumns()
    {
        for (int i = 0; i < height; i++)
        {
            if (HasLine(i))
            {
                lines.Add(i);
                Debug.Log("Find line " + i);
            }

            if (HasColumn(i))
            {
                columns.Add(i);
                Debug.Log("Find column " + i);
            }
        }

        foreach (int index in lines)
        {
            DeleteLine(index);
        }

        foreach (int index in columns)
        {
            DeleteColumn(index);
        }

        ClearLines.Invoke(lines.Count + columns.Count);

        lines.Clear();
        columns.Clear();
    }

    private bool HasLine(int j)
    {
        for (int i = 0; i < width; i++)
        {
            if (slots[i, j] == null)
            {
                return false;
            }
        }

        Debug.Log("Find line #" + j);
        return true;
    }

    private bool HasColumn(int i)
    {
        for (int j = 0; j < height; j++)
        {
            if (slots[i, j] == null)
            {
                return false;
            }
        }

        Debug.Log("Find column #" + i);
        return true;
    }

    private void DeleteLine(int j)
    {
        StringBuilder str = new StringBuilder();

        for (int i = 0; i < width; i++)
        {
            if (slots[i, j] != null)
            {
                str.Append("(" + i + "," + j + ") ");
                Destroy(slots[i, j].gameObject);
            }
        }

        Debug.Log("Delete line " + str);
    }

    private void DeleteColumn(int i)
    {
        StringBuilder str = new StringBuilder();

        for (int j = 0; j < height; j++)
        {
            if (slots[i, j] != null)
            {
                str.Append("(" + i + "," + j + ") ");
                Destroy(slots[i, j].gameObject);
            }
        }

        Debug.Log("Delete column " + str);
    }
}
